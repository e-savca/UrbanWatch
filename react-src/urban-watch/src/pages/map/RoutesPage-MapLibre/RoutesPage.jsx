import MapSelect from '../../../components/map-components/MapSelect'
import MapLibreGLMap from '../../../components/mapbox-components/MapLibreGLMap'
import { defaultCenterPositionOnMapLngLat } from '../../../data/AppData'
import RoutesRepository from '../../../repositories/RoutesRepository'
import ShapeRepository from '../../../repositories/ShapeRepository'
import StopTimesRepository from '../../../repositories/StopTimesRepository'
import TripRepository from '../../../repositories/TripRepository'
import VehicleRepository from '../../../repositories/VehicleRepository'
import TranzyUtils from '../../../utils/TranzyUtils'
import RoutesData from '../../../data/Routes'
import { useEffect, useMemo, useReducer, useState } from 'react'

// Repositories and utils
const tranzyUtils = new TranzyUtils()
const shapeRepository = new ShapeRepository()
const tripRepository = new TripRepository()
const vehicleRepository = new VehicleRepository(false)
const stopTimesRepo = new StopTimesRepository()
const routesRepository = new RoutesRepository()

// Initial state
const initialState = {
  route: RoutesData[0],
  tripDirection: 0,
  stops: null,
  mapKey: 0,
  userGeolocation: defaultCenterPositionOnMapLngLat,
  modalIsOpen: false,
  selectedStation: null,
  routesAffiliatedToSelectedStation: [],
}

// Reducer function
function reducer(state, action) {
  switch (action.type) {
    case 'SET_ROUTE':
      return {
        ...state,
        route: RoutesData.find(
          (route) => route.route_id === Number(action.payload)
        ),
        tripDirection: 0,
      }
    case 'SET_DIRECTION':
      return {
        ...state,
        tripDirection: Number(action.payload),
      }
    case 'SET_STOPS':
      return { ...state, stops: action.payload }
    case 'SET_CENTER':
      return { ...state, mapCenter: action.payload }
    case 'INCREASE_MAP_KEY':
      return { ...state, mapKey: state.mapKey + 1 }
    case 'SET_USER_GEOLOCATION':
      return { ...state, userGeolocation: action.payload }
    case 'SET_MODAL_IS_OPEN':
      return { ...state, modalIsOpen: action.payload }
    case 'SET_SELECTED_STATION':
      return { ...state, selectedStation: action.payload }
    case 'SET_ROUTES_AFFILIATED_TO_SELECTED_STATION':
      return { ...state, routesAffiliatedToSelectedStation: action.payload }
    default:
      return state
  }
}

function RoutesPage() {
  const [state, dispatch] = useReducer(reducer, initialState)

  const dispatchHelper = useMemo(
    () => ({
      setRoute: (routeId) => {
        dispatch({ type: 'SET_ROUTE', payload: routeId })
      },
      setDirection: (tripDirection) => {
        dispatch({ type: 'SET_DIRECTION', payload: tripDirection })
      },
      setStops: (stops) => {
        dispatch({ type: 'SET_STOPS', payload: stops })
      },
      setCenter: (center) => {
        dispatch({ type: 'SET_CENTER', payload: center })
      },
      increaseMapKey: () => {
        dispatch({ type: 'INCREASE_MAP_KEY' })
      },
      setUserGeolocation: (userGeolocation) => {
        dispatch({ type: 'SET_USER_GEOLOCATION', payload: userGeolocation })
      },
      setModalIsOpen: (payloadValue) => {
        dispatch({ type: 'SET_MODAL_IS_OPEN', payload: payloadValue })
      },
      setSelectedStation: (station) => {
        dispatch({ type: 'SET_SELECTED_STATION', payload: station })
      },
      setRoutesAffiliatedToSelectedStation: (routes) => {
        dispatch({
          type: 'SET_ROUTES_AFFILIATED_TO_SELECTED_STATION',
          payload: routes,
        })
      },
    }),
    [dispatch]
  )

  const {
    route,
    tripDirection,
    stops,
    mapKey,
    userGeolocation,
    modalIsOpen,
    selectedStation,
    routesAffiliatedToSelectedStation,
  } = state

  const tripsOnRoute = tripRepository.GetTripsByRouteId(route.route_id)
  const tripId = tranzyUtils.getTripIdBaseOnRouteIdAndDirection(
    route.route_id,
    tripDirection
  )

  const [vehicles, setVehicles] = useState([])

  useEffect(() => {
    async function getData() {
      const shapes = await shapeRepository.GetShapeById(tripId)
      dispatchHelper.setStops(shapes)
    }
    getData()
  }, [dispatchHelper, tripId])

  useEffect(() => {
    async function getVehicles() {
      const data = await vehicleRepository.GetVehiclesByTripId(tripId)
      setVehicles(data)
    }
    getVehicles()
  }, [tripId])

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
  )
}

export default RoutesPage
