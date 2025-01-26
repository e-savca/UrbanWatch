import { useEffect, useReducer } from 'react';
import MapSelect from '../../../components/map-components/MapSelect';
import MapLibreGLMap from '../../../components/mapbox-components/MapLibreGLMap';
import { defaultCenterPositionOnMapLngLat } from '../../../data/AppData';
import { ShapeDTO } from '../../../dto/TranzyDTOs';

import {
  TransportState,
  TransportActions,
  TransportActionTypes,
} from '../../../types/maps';
import TransportUnitOfWork from '../../../repositories/TransportRepositories/TransportUnitOfWork';

const transportUnitOfWork = new TransportUnitOfWork();

async function generateInitialState(
  routeId: number = 8
): Promise<TransportState> {
  const route = await transportUnitOfWork.Routes.getById(routeId);

  if (!route) {
    throw new Error(`Route with ID ${routeId} not found.`);
  }

  const tripDirection = 0;
  const trip = await transportUnitOfWork.Trips.getByRouteIdAndDirection(
    routeId,
    tripDirection
  );

  const tripsOnRoute = await transportUnitOfWork.Trips.getByRouteId(routeId);

  if (!trip) {
    throw new Error(`Trip not found.`);
  }

  const routeShapes: ShapeDTO[] =
    (await transportUnitOfWork.Shapes.getByTripId(trip.shape_id.toString())) ||
    [];

  const mapCenter = defaultCenterPositionOnMapLngLat;

  const vehicles = await transportUnitOfWork.Vehicles.getByTripId(trip.trip_id);

  if (!vehicles)
    throw new Error(
      `Vehicles not found by trip ID (tripId = ${trip.trip_id})).`
    );

  return {
    route,
    tripDirection,
    trip,
    tripsOnRoute,
    routeShapes,
    mapCenter,
    mapKey: 0,
    userGeolocation: null,
    vehicles,
  };
}

// Initial state
const initialState: TransportState = await generateInitialState(8);

// Reducer function
function reducer(
  state: TransportState,
  action: TransportActions
): TransportState {
  switch (action.type) {
    case TransportActionTypes.SetRoute:
      return {
        ...state,
        route: action.payload || state.route,
        tripDirection: 0,
      };
    case TransportActionTypes.SetDirection:
      return {
        ...state,
        tripDirection: Number(action.payload),
      };
    case TransportActionTypes.SetTripsOnRoute:
      return { ...state, tripsOnRoute: action.payload || [] };
    case TransportActionTypes.SetVehicles:
      return {
        ...state,
        vehicles: action.payload || [],
      };
    case TransportActionTypes.SetRouteShapes:
      return {
        ...state,
        routeShapes: action.payload || [],
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

  const { route, tripDirection, trip, vehicles, tripsOnRoute } = state;

  useEffect(() => {
    async function setVehicles() {
      const vehiclesData = await transportUnitOfWork.Vehicles.getByTripId(
        trip.trip_id
      );
      dispatch({
        type: TransportActionTypes.SetVehicles,
        payload: vehiclesData,
      });
    }

    async function setRouteShapes() {
      const shapes = await transportUnitOfWork.Shapes.getByTripId(trip.trip_id);
      dispatch({
        type: TransportActionTypes.SetRouteShapes,
        payload: shapes || [],
      });
    }
    async function updateTripsOnRoute() {
      const trips = await transportUnitOfWork.Trips.getByRouteId(
        route.route_id
      );
      dispatch({ type: TransportActionTypes.SetTripsOnRoute, payload: trips });
    }

    setVehicles();

    setRouteShapes();

    updateTripsOnRoute();
  }, [route.route_id, trip.trip_id]);

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
