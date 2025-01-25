import { useEffect, useMemo, useReducer, useState } from 'react';
import MapSelect from '../../../components/map-components/MapSelect';
import MapLibreGLMap from '../../../components/mapbox-components/MapLibreGLMap';
import { defaultCenterPositionOnMapLngLat } from '../../../data/AppData';
import RoutesRepository from '../../../repositories/RoutesRepository';
import ShapeRepository from '../../../repositories/ShapeRepository';
import StopTimesRepository from '../../../repositories/StopTimesRepository';
import TripRepository from '../../../repositories/TripRepository';
import VehicleRepository from '../../../repositories/VehicleRepository';
import TranzyUtils from '../../../utils/TranzyUtils';
import RoutesData from '../../../data/Routes';
import { RouteDTO, ShapeDTO } from '../../../dto/TranzyDTOs';
import {
  TransportState,
  TransportActions,
  TransportActionTypes,
} from '../../../types/maps';

// Repositories and utils
const tranzyUtils = new TranzyUtils();
const shapeRepository = new ShapeRepository();
const tripRepository = new TripRepository();
const vehicleRepository = new VehicleRepository(false);
const stopTimesRepo = new StopTimesRepository();
const routesRepository = new RoutesRepository();

// Initial state
const initialState: TransportState = {
  route: RoutesData[0],
  tripDirection: 0,
  routeShapes: null,
  mapKey: 0,
  userGeolocation: defaultCenterPositionOnMapLngLat,
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
        route:
          RoutesData.find(route => route.route_id === action.payload) ||
          state.route,
        tripDirection: 0,
      };
    case TransportActionTypes.SetDirection:
      return {
        ...state,
        tripDirection: Number(action.payload),
      };
    case TransportActionTypes.SetRouteShapes:
      return { ...state, stops: action.payload };
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

  const dispatchHelper = useMemo(
    () => ({
      setRoute: routeId => {
        dispatch({ type: 'SET_ROUTE', payload: routeId });
      },
      setDirection: tripDirection => {
        dispatch({ type: 'SET_DIRECTION', payload: tripDirection });
      },
      setStops: stops => {
        dispatch({ type: 'SET_STOPS', payload: stops });
      },
      setCenter: center => {
        dispatch({ type: 'SET_CENTER', payload: center });
      },
      increaseMapKey: () => {
        dispatch({ type: 'INCREASE_MAP_KEY' });
      },
      setUserGeolocation: userGeolocation => {
        dispatch({ type: 'SET_USER_GEOLOCATION', payload: userGeolocation });
      },
      setModalIsOpen: payloadValue => {
        dispatch({ type: 'SET_MODAL_IS_OPEN', payload: payloadValue });
      },
      setSelectedStation: station => {
        dispatch({ type: 'SET_SELECTED_STATION', payload: station });
      },
      setRoutesAffiliatedToSelectedStation: routes => {
        dispatch({
          type: 'SET_ROUTES_AFFILIATED_TO_SELECTED_STATION',
          payload: routes,
        });
      },
    }),
    [dispatch]
  );

  const {
    route,
    tripDirection,
    stops,
    mapKey,
    userGeolocation,
    modalIsOpen,
    selectedStation,
    routesAffiliatedToSelectedStation,
  } = state;

  const tripsOnRoute = tripRepository.GetTripsByRouteId(route.route_id);
  const tripId = tranzyUtils.getTripIdBaseOnRouteIdAndDirection(
    route.route_id,
    tripDirection
  );

  const [vehicles, setVehicles] = useState([]);

  useEffect(() => {
    async function getData() {
      const shapes = await shapeRepository.GetShapeById(tripId);
      dispatchHelper.setStops(shapes);
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
        dispatchHelper={dispatchHelper}
      />
      <MapLibreGLMap vehicles={vehicles} />
    </>
  );
}

export default RoutesPage;
