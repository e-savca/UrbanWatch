import { useEffect, useReducer } from 'react';
import MapSelect from '../../../components/map-components/MapSelect';
import MapLibreGLMap from '../../../components/mapbox-components/MapLibreGLMap';
import { defaultCenterPositionOnMapLngLat } from '../../../data/AppData';

import {
  TransportState,
  TransportActions,
  TransportActionTypes,
} from '../../../types/maps';
import TransportUnitOfWork from '../../../repositories/TransportRepositories/TransportUnitOfWork';
import { ShapeDTO } from '../../../dto/TranzyDTOs';

const transportUnitOfWork = await TransportUnitOfWork.create();

function generateInitialState(routeId: number = 3): TransportState {
  const route = transportUnitOfWork.Routes.getById(routeId);

  if (!route) {
    throw new Error(`Route with ID ${routeId} not found.`);
  }

  const tripDirection = 0;
  const trip = transportUnitOfWork.Trips.getByRouteIdAndDirection(
    routeId,
    tripDirection
  );

  if (!trip) {
    throw new Error(`Trip not found.`);
  }

  const routeShapes: ShapeDTO[] =
    trip !== undefined
      ? transportUnitOfWork.Shapes.getByTripId(trip.shape_id.toString()) || []
      : [];

  const mapCenter = defaultCenterPositionOnMapLngLat;

  const vehicles = transportUnitOfWork.Vehicles.getByTripId(trip.trip_id);

  if (!vehicles)
    throw new Error(
      `Vehicles not found by trip ID (tripId = ${trip.trip_id})).`
    );

  return {
    route,
    tripDirection,
    trip,
    routeShapes,
    mapCenter,
    mapKey: 0,
    userGeolocation: null,
    vehicles,
  };
}

// Initial state
const initialState: TransportState = generateInitialState(3);

// Reducer function
function reducer(
  state: TransportState,
  action: TransportActions
): TransportState {
  switch (action.type) {
    case TransportActionTypes.SetRoute:
      return {
        ...state,
        route:
          transportUnitOfWork.Routes.getById(action.payload) || state.route,
        tripDirection: 0,
      };
    case TransportActionTypes.SetDirection:
      return {
        ...state,
        tripDirection: Number(action.payload),
      };
    case TransportActionTypes.SetVehicles:
      return {
        ...state,
        vehicles:
          transportUnitOfWork.Vehicles.getByTripId(action.payload) || [],
      };
    case TransportActionTypes.SetRouteShapes:
      return {
        ...state,
        routeShapes:
          transportUnitOfWork.Shapes.getByTripId(action.payload) || [],
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

  const { route, tripDirection, trip, vehicles } = state;

  const tripsOnRoute = transportUnitOfWork.Trips.getByRouteId(route?.route_id);

  useEffect(() => {
    function getData() {
      dispatch({
        type: TransportActionTypes.SetRouteShapes,
        payload: trip.trip_id,
      });
    }

    function getVehicles() {
      dispatch({
        type: TransportActionTypes.SetVehicles,
        payload: trip.trip_id,
      });
    }
    getData();

    getVehicles();
  }, [trip.trip_id]);

  return (
    <>
      <MapSelect
        route={route}
        tripDirection={tripDirection}
        tripsOnRoute={tripsOnRoute}
        dispatch={dispatch}
      />
      <MapLibreGLMap vehicles={vehicles} />
    </>
  );
}

export default RoutesPage;
