import { MapContainer, TileLayer, Marker, Popup, Polyline } from 'react-leaflet'
import 'leaflet/dist/leaflet.css'
import { useEffect, useState } from 'react'
import Routes from './data/Routes.js'
import VehicleRepository from './repositories/VechicleRepository.js'
import BusIcon from './components/leaflet-components/BusIcon.js'
import TripRepository from './repositories/TripRepository.js'
import ShapeRepository from './repositories/ShapeRepository.js'

const vehicleRepository = new VehicleRepository()
const shapeReposity = new ShapeRepository()
const tripRepository = new TripRepository()

function App() {
  const [selectedRoute, setSelectedRoute] = useState(Routes[15])
  const [tripWayOrRoundWay, setTripWayOrRoundWay] = useState(0)
  const tripId = `${selectedRoute.route_id}_${tripWayOrRoundWay}`
  const trip = tripRepository.GetTripById(tripId)
  const [shapes, setShapes] = useState([])
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
  console.log(shapes)
  const vehiclesArray = vehicleRepository.GetVehiclesByTripId(tripId)

  function HandleSelectRoute(e) {
    const id = Number(e.target.value)
    const selectedRoute = Routes.find((x) => x.route_id === id)
    setSelectedRoute(selectedRoute)
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
          <option value={0}>Way</option>
          <option value={1}>Round Way</option>
        </select>
      </header>

      <section>
        <MapContainer
          center={[47.024371640335254, 28.832034417468275]}
          zoom={11}
          // scrollWheelZoom={false}
          style={{ height: '80vh', width: '100%' }} // Reduced height
        >
          <TileLayer
            attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
          />
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
