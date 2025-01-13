import { MapContainer, TileLayer, Marker, Popup, Polyline } from 'react-leaflet'
import 'leaflet/dist/leaflet.css'
import { useEffect, useState } from 'react'
import Routes from './data/Routes'
import VehicleRepository from './repositories/VechicleRepository'
import BusIcon from './components/leaflet-components/BusIcon'
import UserIcon from './components/leaflet-components/UserIcon'
import TripRepository from './repositories/TripRepository'
import ShapeRepository from './repositories/ShapeRepository'

const vehicleRepository = new VehicleRepository()
const shapeReposity = new ShapeRepository()
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
function App() {
  const [selectedRoute, setSelectedRoute] = useState(Routes[15])
  const [tripWayOrRoundWay, setTripWayOrRoundWay] = useState(0)
  const tripId = `${selectedRoute.route_id}_${tripWayOrRoundWay}`
  const trip = tripRepository.GetTripById(tripId)
  const tripsOnRoute = tripRepository.GetTripsByRouteId(selectedRoute.route_id)
  const [shapes, setShapes] = useState([])
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
        const shapes = await shapeReposity.GetShapeById(trip.shape_id)
        setShapes(shapes)
      }

      getData()
    },
    [trip.shape_id]
  )

  function HandleSelectRoute(e) {
    const id = Number(e.target.value)
    const selectedRoute = Routes.find((x) => x.route_id === id)
    setSelectedRoute(selectedRoute)
    setTripWayOrRoundWay(0)
  }

  return (
    <>
      <header>
        <select value={selectedRoute.route_id} onChange={HandleSelectRoute}>
          {Routes.map((r) => (
            <option key={r.route_id} value={r.route_id}>
              {vehicleRepository.GetVehicleType(r.route_type)}{' '}
              {r.route_short_name}: {r.route_long_name}
            </option>
          ))}
        </select>
        <select
          value={tripWayOrRoundWay}
          onChange={(e) => setTripWayOrRoundWay(e.target.value)}
        >
          {tripsOnRoute.map((t) => (
            <option
              key={t.trip_id}
              value={t.direction_id}
              onChange={(e) => setTripWayOrRoundWay(e.target.value)}
            >
              to {t.trip_headsign}
            </option>
          ))}
        </select>
      </header>

      <section>
        <MapContainer
          key={userGeolocation}
          center={userGeolocation}
          zoom={18}
          scrollWheelZoom={false}
          style={{ height: '80vh', width: '100%' }}
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

      <footer style={{ textAlign: 'center' }}>
        &copy; {new Date().getFullYear()} Urban Watch
      </footer>
    </>
  )
}

export default App
