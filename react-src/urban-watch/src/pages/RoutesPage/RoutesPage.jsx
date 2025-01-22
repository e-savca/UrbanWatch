import { useEffect, useReducer, useState } from 'react'
import { useSearchParams } from 'react-router'
import Map from '../../components/leaflet-components/Map.jsx'
import RoutesData from '../../data/Routes.jsx'
import TripRepository from '../../repositories/TripRepository.jsx'
import MapSelect from './MapSelect.jsx'
import VehicleRepository from '../../repositories/VehicleRepository.jsx'
import { Marker, Polyline, Popup } from 'react-leaflet'
import TranzyUtils from '../../utils/TranzyUtils.jsx'
import ShapeRepository from '../../repositories/ShapeRepository.jsx'
import { defaultCenterPositionOnMap } from '../../data/AppData.jsx'
import { GetUserGeoLocation } from '../../utils/GetUserGeoLocation'
import UserIcon from '../../components/leaflet-components/icons/UserIcon.jsx'
import BusIcon from '../../components/leaflet-components/icons/BusIcon.jsx'
import Stops from '../../data/Stops'
import ShowBusStops from '../../components/leaflet-components/ShowBusStops'
import MapTools from '../../components/leaflet-components/MapTools.jsx'
import BusStopModal from '../../components/BusStopModal.jsx'
import StopTimesRepository from '../../repositories/StopTimesRepository'
import RoutesRepository from '../../repositories/RoutesRepository'

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
  userGeolocation: defaultCenterPositionOnMap,
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
    default:
      return state
  }
}

function RoutesPage() {
  const [state, dispatch] = useReducer(reducer, initialState)

  const { route, tripDirection, stops, mapKey, userGeolocation } = state

  const [searchParams, setSearchParams] = useSearchParams()

  useEffect(() => {
    const getLocation = async () => {
      try {
        const result = await GetUserGeoLocation()
        if (
          userGeolocation[0] !== result[0] ||
          userGeolocation[1] !== result[1]
        ) {
          console.log('increaseMapKey')
          dispatch({ type: 'SET_USER_GEOLOCATION', payload: result })
          dispatch({ type: 'INCREASE_MAP_KEY' })
        }
      } catch (error) {
        console.error('Error fetching geolocation:', error)
      }
    }

    getLocation()
  }, [])

  useEffect(() => {
    const routeId = searchParams.get('route')
    const direction = searchParams.get('direction')

    if (routeId) {
      dispatch({ type: 'SET_ROUTE', payload: routeId })
    }
    if (direction) {
      dispatch({ type: 'SET_DIRECTION', payload: direction })
    }
  }, [searchParams])

  useEffect(() => {
    const params = {}
    params.route = route.route_id
    params.direction = tripDirection
    setSearchParams(params, { replace: true })
  }, [route, tripDirection, setSearchParams])

  const tripsOnRoute = tripRepository.GetTripsByRouteId(route.route_id)
  const tripId = tranzyUtils.getTripIdBaseOnRouteIdAndDirection(
    route.route_id,
    tripDirection
  )
  const [vehicles, setVehicles] = useState([])

  useEffect(() => {
    async function getData() {
      const shapes = await shapeRepository.GetShapeById(tripId)
      dispatch({ type: 'SET_STOPS', payload: shapes })
    }
    getData()
  }, [tripId])

  useEffect(() => {
    async function getVehicles() {
      const data = await vehicleRepository.GetVehiclesByTripId(tripId)
      setVehicles(data)
    }
    getVehicles()
  }, [tripId])

  const [modalIsOpen, setModalIsOpen] = useState(false)
  const [selectedStation, setSelectedStation] = useState(null)
  const [
    routesAffiliatedToSelectedStation,
    setRoutesAffiliatedToSelectedStation,
  ] = useState([])
  function handleBusStopClick(station) {
    if (selectedStation === station) {
      setModalIsOpen((prevValue) => (prevValue !== true ? true : false))
      return
    }
    setModalIsOpen(true)
    setSelectedStation(station)
    const trips = stopTimesRepo
      .GetStopTimesByStopId(station.stop_id)
      .map(({ trip_id }) => tripRepository.GetTripById(trip_id))
    console.log(trips)
    const routeTripMapping = trips.map((trip) => ({
      trip,
      route: routesRepository.GetRouteById(trip.route_id),
    }))

    setRoutesAffiliatedToSelectedStation(routeTripMapping)
  }
  return (
    <>
      <MapSelect
        route={route}
        tripsOnRoute={tripsOnRoute}
        tripDirection={tripDirection}
        dispatch={dispatch}
      />
      <Map zoom={16} centerPosition={userGeolocation} key={mapKey}>
        {userGeolocation !== defaultCenterPositionOnMap ? (
          <Marker
            key={userGeolocation[0] + userGeolocation[1]}
            position={userGeolocation}
            icon={UserIcon}
          />
        ) : (
          ''
        )}

        {vehicles.map((vehicle) => (
          <Marker
            key={vehicle.id}
            position={[vehicle.latitude, vehicle.longitude]}
            icon={BusIcon}
          >
            <Popup>
              <strong>Speed: </strong>
              {vehicle.speed}
            </Popup>
          </Marker>
        ))}

        {stops !== null ? (
          <Polyline
            positions={stops.map((stop) => [
              stop.shape_pt_lat,
              stop.shape_pt_lon,
            ])}
          />
        ) : null}

        <ShowBusStops busStops={Stops} onBusStopClick={handleBusStopClick} />
        <MapTools dispatch={dispatch} />
      </Map>
      <BusStopModal
        isOpen={modalIsOpen}
        station={selectedStation}
        afiliateRoutes={routesAffiliatedToSelectedStation}
        onClose={() => {
          setModalIsOpen(false)
          setSelectedStation(null)
        }}
      />
    </>
  )
}

export default RoutesPage
