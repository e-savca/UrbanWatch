import React, { useEffect, useReducer } from 'react';
import MapSelect from '../../../components/map-components/MapSelect';
import MapLibreGLMap from '../../../components/mapbox-components/MapLibreGLMap';
import { defaultCenterPositionOnMapLngLat } from '../../../data/AppData';

import {
  TransportState,
  TransportActions,
  TransportActionTypes,
} from '../../../types/maps';
import TransportUnitOfWork from '../../../repositories/TransportRepositories/TransportUnitOfWork';

const transportUnitOfWork = new TransportUnitOfWork();

async function generateInitialState(routeId = 8): Promise<TransportState> {
  const tripDirection = 0;
  const [route, tripsOnRoute] = await Promise.all([
    transportUnitOfWork.Routes.getById(routeId),
    transportUnitOfWork.Trips.getByRouteId(routeId),
  ]);

  if (!route) throw new Error(`Route with ID ${routeId} not found.`);
  if (!tripsOnRoute.length) throw new Error(`No trips found for route.`);

  const trip = tripsOnRoute.find(t => t.direction_id === tripDirection);
  if (!trip) throw new Error(`Trip not found.`);

  const [routeShapes, vehicles] = await Promise.all([
    transportUnitOfWork.Shapes.getByTripId(trip.shape_id.toString()),
    transportUnitOfWork.Vehicles.getByTripId(trip.trip_id),
  ]);

  return {
    route,
    tripDirection,
    trip,
    tripsOnRoute,
    routeShapes: routeShapes || [],
    mapCenter: defaultCenterPositionOnMapLngLat,
    mapKey: 0,
    vehicles: vehicles || [],
  };
}

const initialState: TransportState = {
  mapCenter: defaultCenterPositionOnMapLngLat,
  mapKey: 0,
  route: undefined,
  trip: undefined,
  tripsOnRoute: [],
  tripDirection: 0,
  routeShapes: [],
  vehicles: [],
};

// Reducer function
function reducer(
  state: TransportState,
  action: TransportActions
): TransportState {
  switch (action.type) {
    case TransportActionTypes.InitializeState:
      return action.payload;
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

  const [isLoading, setIsLoading] = React.useState(true);

  useEffect(() => {
    async function initialize() {
      try {
        setIsLoading(true); // Set loading state before fetch
        const generatedState = await generateInitialState(8);
        dispatch({
          type: TransportActionTypes.InitializeState,
          payload: generatedState,
        });
      } catch (error) {
        console.error('Initialization failed:', error);
      } finally {
        setIsLoading(false); // Disable loading after fetch
      }
    }
    initialize();
  }, []);

  const MemoizedMapSelect = React.memo(MapSelect);
  const MemoizedMapLibreGLMap = React.memo(MapLibreGLMap);

  const { route, tripDirection, trip, vehicles, tripsOnRoute } = state;

  useEffect(() => {
    async function setVehicles() {
      if (trip?.trip_id) {
        const vehiclesData = await transportUnitOfWork.Vehicles.getByTripId(
          trip.trip_id
        );
        dispatch({
          type: TransportActionTypes.SetVehicles,
          payload: vehiclesData,
        });
      }
    }

    async function setRouteShapes() {
      if (trip?.trip_id) {
        const shapes = await transportUnitOfWork.Shapes.getByTripId(
          trip.trip_id
        );
        dispatch({
          type: TransportActionTypes.SetRouteShapes,
          payload: shapes || [],
        });
      }
    }

    async function updateTripsOnRoute() {
      if (route?.route_id) {
        const trips = await transportUnitOfWork.Trips.getByRouteId(
          route.route_id
        );
        dispatch({
          type: TransportActionTypes.SetTripsOnRoute,
          payload: trips,
        });
      }
    }

    setVehicles();

    setRouteShapes();

    updateTripsOnRoute();
  }, [route?.route_id, trip?.trip_id]);
  return (
    <>
      {isLoading && (
        <p className="text-gray-500 text-center">Loading data...</p>
      )}

      {!isLoading && route && tripsOnRoute?.length ? (
        <MemoizedMapSelect
          route={route}
          tripDirection={tripDirection ?? 0}
          tripsOnRoute={tripsOnRoute}
          dispatch={dispatch}
        />
      ) : (
        !isLoading && (
          <p className="text-red-500 text-center">Failed to load route data.</p>
        )
      )}

      {isLoading && <p className="text-gray-500 text-center">Loading map...</p>}

      {!isLoading && vehicles && trip ? (
        <MemoizedMapLibreGLMap vehicles={vehicles} />
      ) : (
        !isLoading && (
          <p className="text-red-500 text-center">Map data not available.</p>
        )
      )}
    </>
  );
}

export default RoutesPage;
