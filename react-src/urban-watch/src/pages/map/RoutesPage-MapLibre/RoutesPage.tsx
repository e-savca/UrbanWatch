import { useEffect, useReducer, useState } from 'react';
import MapSelect from '../../../components/map-components/MapSelect';
import MapLibreGLMap from '../../../components/mapbox-components/MapLibreGLMap';
import { defaultCenterPositionOnMapLngLat } from '../../../data/AppData';

import {
  TransportState,
  TransportActions,
  TransportActionTypes,
} from '../../../types/maps';
import TransportUnitOfWork from '../../../repositories/TransportRepositories/TransportUnitOfWork';
import tranzyUtils from '../../../utils/TranzyUtils';
import { RouteDTO } from '../../../dto/TranzyDTOs';

const transportUnitOfWork = await TransportUnitOfWork.create();

function generateInitialState(routeId: number): TransportState {
  const route = transportUnitOfWork.Routes.getById(routeId);
  const tripDirection = 0;
  const trip = transportUnitOfWork.Trips.getByRouteIdAndDirection(
    route?.route_id,
    tripDirection
  );
  const routeShapes = transportUnitOfWork.Shapes.getById(trip?.shape_id);

  return { route, tripDirection, trip };
}

// Initial state
const initialState: TransportState = {
  route: transportUnitOfWork.Routes.getAll()?.at(0) || null,
  tripDirection: 0,
  routeShapes: null,
  mapCenter: defaultCenterPositionOnMapLngLat,
  mapKey: 0,
  userGeolocation: null,
  Vehicles: null,
};

// Reducer function
function reducer(
  state: TransportState,
  action: TransportActions
): TransportState {
  switch (action.type) {
    case TransportActionTypes.SetRoute:
      return {
        ...state,
        route: transportUnitOfWork.Routes.getById(action.payload),
        tripDirection: 0,
      };
    case TransportActionTypes.SetDirection:
      return {
        ...state,
        tripDirection: Number(action.payload),
      };
    case TransportActionTypes.SetRouteShapes:
      return {
        ...state,
        routeShapes: transportUnitOfWork.Shapes.getById(action.payload),
      };
    case TransportActionTypes.SetMapCenter:
      return { ...state, mapCenter: action.payload };
    case TransportActionTypes.IncreaseMapKey:
      return { ...state, mapKey: state.mapKey + 1 };
    case TransportActionTypes.SetUserGeolocation:
      return { ...state, userGeolocation: action.payload };
    default:
      return state;
  }
}

function RoutesPage() {
  const [state, dispatch] = useReducer(reducer, initialState);
  const [vehicles, setVehicles] = useState(Array<RouteDTO>);

  const { route, tripDirection } = state;

  const tripsOnRoute = transportUnitOfWork.Trips.getByRouteId(route?.route_id);
  const tripId: string = tranzyUtils.getTripIdBaseOnRouteIdAndDirection(
    route.route_id,
    tripDirection
  );

  useEffect(() => {
    async function getData() {
      const shapes = await shapeRepository.GetShapeById(tripId);
      dispatch({ type: TransportActionTypes.SetRouteShapes, payload: shapes });
    }
    getData();
  }, [dispatchHelper, tripId]);

  useEffect(() => {
    async function getVehicles() {
      const data = await vehicleRepository.GetVehiclesByTripId(tripId);
      setVehicles(data);
    }
    getVehicles();
  }, [tripId]);

  return (
    <>
      <MapSelect
        route={route}
        tripDirection={tripDirection}
        tripsOnRoute={tripsOnRoute}
        dispatchHelper={dispatch}
      />
      <MapLibreGLMap vehicles={vehicles} />
    </>
  );
}

export default RoutesPage;
