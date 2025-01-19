import { useEffect, useReducer, useState } from 'react'
import { useLocation, useNavigate } from 'react-router'
import Map from '../../components/leaflet-components/Map.jsx'
import RoutesData from '../../data/Routes.jsx'
import TripRepository from '../../repositories/TripRepository.jsx'
import MapSelect from './MapSelect.jsx'
import VehicleRepository from '../../repositories/VehicleRepository.jsx'
import { Marker, Polyline, Popup } from 'react-leaflet'
import BusIcon from '../../components/leaflet-components/BusIcon.jsx'
import TranzyUtils from '../../utils/TranzyUtils.jsx'
import ShapeRepository from '../../repositories/ShapeRepository.jsx'

// Repositories and utils
const tranzyUtils = new TranzyUtils()
const shapeRepository = new ShapeRepository()
const tripRepository = new TripRepository()
const vehicleRepository = new VehicleRepository(true)

// Initial state
const initialState = {
  route: RoutesData[0],
  tripDirection: 0,
  stops: null,
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
    default:
      return state
  }
}

function RoutesPage() {
  const [state, dispatch] = useReducer(reducer, initialState)
  const { route, tripDirection, stops } = state

  const location = useLocation()
  const navigate = useNavigate()

  useEffect(() => {
    const searchParams = new URLSearchParams(location.search)
    const routeId = searchParams.get('route')
    const direction = searchParams.get('direction')

    if (routeId) {
      dispatch({ type: 'SET_ROUTE', payload: routeId })
    }
    if (direction) {
      dispatch({ type: 'SET_DIRECTION', payload: direction })
    }
  }, [location.search])

  useEffect(() => {
    const searchParams = new URLSearchParams()
    searchParams.set('route', route.route_id)
    searchParams.set('direction', tripDirection)
    navigate(`?${searchParams.toString()}`, { replace: true })
  }, [route, tripDirection, navigate])

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

  return (
    <>
      <MapSelect
        route={route}
        tripsOnRoute={tripsOnRoute}
        tripDirection={tripDirection}
        dispatch={dispatch}
      />
      <Map zoom={13}>
        {vehicles.map((vehicle) => (
          <Marker
            key={vehicle.id}
            position={[vehicle.latitude, vehicle.longitude]}
            icon={BusIcon}
          >
            <Popup>
              <div>
                <strong>Route: </strong> {vehicle.label}
                <br />
                <strong>Speed: </strong> {vehicle.speed}
              </div>
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
      </Map>
    </>
  )
}

export default RoutesPage
