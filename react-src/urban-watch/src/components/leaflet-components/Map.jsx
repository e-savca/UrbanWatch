import 'leaflet/dist/leaflet.css' // IMPORTANT for map to work properly

import { MapContainer, TileLayer } from 'react-leaflet'
import PropTypes from 'prop-types'

const defaultCenterPositionOnMap = [47.024371640335254, 28.832034417468275]

Map.propTypes = {
  children: PropTypes.any,
  centerPosition: PropTypes.array,
  zoom: PropTypes.number,
  scrollWheelZoom: PropTypes.bool,
}

function Map({
  children = null,
  centerPosition = defaultCenterPositionOnMap,
  zoom = 18,
  scrollWheelZoom = false,
}) {
  return (
    <div className="flex flex-col space-y-4 z-0">
      <section className="flex-grow">
        <MapContainer
          center={centerPosition}
          zoom={zoom}
          scrollWheelZoom={scrollWheelZoom}
          className="z-10 h-[calc(65vh)] w-full"
        >
          <TileLayer
            attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
          />

          {children}
        </MapContainer>
      </section>
    </div>
  )
}

export default Map
