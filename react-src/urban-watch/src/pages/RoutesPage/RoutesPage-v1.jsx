import { MapContainer, TileLayer, Marker, Popup, Polyline } from 'react-leaflet'
import BusIcon from '../../components/leaflet-components/BusIcon.jsx'
import UserIcon from '../../components/leaflet-components/UserIcon.jsx'
import RoutesData from '../../data/Routes.jsx'
import VehicleRepository from '../../repositories/VehicleRepository.jsx'
import TripRepository from '../../repositories/TripRepository.jsx'
import ShapeRepository from '../../repositories/ShapeRepository.jsx'

import 'leaflet/dist/leaflet.css'
import { useEffect, useState } from 'react'

const vehicleRepository = new VehicleRepository()
const shapeRepository = new ShapeRepository()
const tripRepository = new TripRepository()

const defaultCenterPositionOnMap = [47.024371640335254, 28.832034417468275]

async function GetUserGeoLocation() {
  if ('geolocation' in navigator) {
    return new Promise((resolve) => {
      navigator.geolocation.getCurrentPosition(
        (p) => {
          resolve([p.coords.latitude, p.coords.longitude])
        },
        () => {
          resolve(defaultCenterPositionOnMap)
        }
      )
    })
  } else {
    return defaultCenterPositionOnMap
  }
}

function RoutesPage() {
  const [selectedRoute, setSelectedRoute] = useState(RoutesData[15])
  const [tripWayOrRoundWay, setTripWayOrRoundWay] = useState(0)
  const tripId = `${selectedRoute.route_id}_${tripWayOrRoundWay}`

  const tripsOnRoute = tripRepository.GetTripsByRouteId(selectedRoute.route_id)
  const [shapes, setShapes] = useState([])

  const trip = tripRepository.GetTripById(tripId)
  const vehiclesArray = vehicleRepository.GetVehiclesByTripId(tripId)
  const [userGeolocation, setUserGeolocation] = useState(
    defaultCenterPositionOnMap
  )

  useEffect(function () {
    async function getLocation() {
      const result = await GetUserGeoLocation()
      setUserGeolocation((ug) => (ug !== result ? result : ug))
    }
    getLocation()
  }, [])

  useEffect(
    function () {
      async function getData() {
        const shapes = await shapeRepository.GetShapeById(trip.shape_id)
        setShapes(shapes)
      }

      getData()
    },
    [trip.shape_id]
  )

  function HandleSelectRoute(e) {
    const id = Number(e.target.value)
    const selectedRoute = RoutesData.find((x) => x.route_id === id)
    setSelectedRoute(selectedRoute)
    setTripWayOrRoundWay(0)
  }
  return (
    <div className="flex flex-col space-y-4 z-0">
      <header className="flex flex-wrap sm:flex-nowrap flex-col sm:flex-row space-y-4 sm:space-y-0 sm:space-x-4 p-4 bg-gray-100 shadow">
        <select
          className="p-2 rounded border border-gray-300 bg-white"
          value={selectedRoute.route_id}
          onChange={HandleSelectRoute}
        >
          {RoutesData.map((r) => (
            <option key={r.route_id} value={r.route_id}>
              {vehicleRepository.GetVehicleType(r.route_type)}{' '}
              {r.route_short_name}: {r.route_long_name}
            </option>
          ))}
        </select>
        <select
          className="p-2 rounded border border-gray-300 bg-white"
          value={tripWayOrRoundWay}
          onChange={(e) => setTripWayOrRoundWay(e.target.value)}
        >
          {tripsOnRoute.map((t) => (
            <option key={t.trip_id} value={t.direction_id}>
              to {t.trip_headsign}
            </option>
          ))}
        </select>
      </header>

      <section className="flex-grow">
        <MapContainer
          key={userGeolocation}
          center={userGeolocation}
          zoom={18}
          scrollWheelZoom={false}
          className="absolute top-0 left-0 z-10 h-[calc(100vh-4rem)] w-full"
        >
          <TileLayer
            attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
          />

          <Marker
            key={userGeolocation}
            position={userGeolocation}
            icon={UserIcon}
          >
            <Popup key={userGeolocation}>
              <div>
                <p>UserLocation</p>
              </div>
            </Popup>
          </Marker>
          {vehiclesArray.map((vehicle) => (
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
          <Polyline
            positions={shapes.map((s) => [s.shape_pt_lat, s.shape_pt_lon])}
          />
        </MapContainer>
      </section>
    </div>
  )
}

export default RoutesPage
