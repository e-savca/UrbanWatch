import { useEffect, useReducer, useState } from 'react'
import Map from '../../components/leaflet-components/Map.jsx'
import RoutesData from '../../data/Routes.jsx'
import TripRepository from '../../repositories/TripRepository.jsx'
import MapSelect from './MapSelect.jsx'
import VehicleRepository from '../../repositories/VehicleRepository.jsx'
import { Marker, Polyline, Popup } from 'react-leaflet'
import BusIcon from '../../components/leaflet-components/BusIcon.jsx'

// Utils
import TranzyUtils from '../../utils/TranzyUtils.jsx'
import ShapeRepository from '../../repositories/ShapeRepository.jsx'

var tranzyUtils = new TranzyUtils()

// Repo's
const shapeRepository = new ShapeRepository()

const tripRepository = new TripRepository()
const vehicleRepository = new VehicleRepository(false)

const initialState = {
  route: RoutesData[0],
  tripDirection: 0,
  stops: null,
}

function reducer(state, action) {
  switch (action.type) {
    case 'SET_ROUTE':
      return {
        ...state,
        route: RoutesData.find(
          (route) => route.route_id === Number(action.payload)
        ),
      }
    case 'SET_DIRECTION':
      return { ...state, tripDirection: Number(action.payload) }
    case 'SET_STOPS':
      return { ...state, stops: action.payload }
    default:
      return state
  }
}

function RoutesPage() {
  const [state, dispatch] = useReducer(reducer, initialState)

  const { route, tripDirection, stops } = state

  const tripsOnRoute = tripRepository.GetTripsByRouteId(route.route_id)
  const tripId = tranzyUtils.getTripIdBaseOnRouteIdAndDirection(
    route.route_id,
    tripDirection
  )
  const [vehicles, setVehicles] = useState([])
  useEffect(
    function () {
      async function getData() {
        const shapes = await shapeRepository.GetShapeById(tripId)
        dispatch({ type: 'SET_STOPS', payload: shapes })
      }

      getData()
    },
    [tripId]
  )

  useEffect(
    function () {
      async function getVehicles() {
        const data = await vehicleRepository.GetVehiclesByTripId(tripId)
        setVehicles(data)
      }
      getVehicles()
    },
    [setVehicles, tripId]
  )
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
