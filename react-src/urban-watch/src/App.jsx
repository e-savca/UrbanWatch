import { MapContainer, Marker, Popup } from 'react-leaflet'
import { TileLayer } from 'react-leaflet/TileLayer'
import 'leaflet/dist/leaflet.css'

import Routes from './data/Routes.js'
import { useState } from 'react'
import { GetVehicleType } from './utils/vehicleUtils.js'

function App() {
  const [selectedRoute, setSelectedRoute] = useState(Routes[2])
  return (
    <>
      <select
        value={selectedRoute.route_id}
        onChange={(e) => setSelectedRoute(e.target.value)}
      >
        {Routes.map((r) => (
          <option key={r.route_id} value={r.route_id}>
            {GetVehicleType(r.route_type)} {r.route_short_name} -{' '}
            {r.route_long_name}
          </option>
        ))}
      </select>
      <br />
      <MapContainer
        center={[51.505, -0.09]}
        zoom={13}
        scrollWheelZoom={false}
        style={{ height: '80vh', width: '100%' }} // Reduced height
      >
        <TileLayer
          attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
          url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
        />
        <Marker position={[51.505, -0.09]}>
          <Popup>
            A pretty CSS3 popup. <br /> Easily customizable.
          </Popup>
        </Marker>
      </MapContainer>
    </>
  )
}

export default App
