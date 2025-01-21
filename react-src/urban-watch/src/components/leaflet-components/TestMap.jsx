import { useEffect, useState } from 'react'
import { MapContainer, TileLayer, useMap } from 'react-leaflet'
import 'leaflet/dist/leaflet.css' // IMPORTANT for map to work properly
const MapInfo = () => {
  const map = useMap()
  const [zoom, setZoom] = useState(map.getZoom())
  const [center, setCenter] = useState(map.getCenter())
  const [corners, setCorners] = useState({
    northEast: null,
    southWest: null,
  })

  useEffect(() => {
    const onZoomEnd = () => {
      console.log(map.getZoom())
      setZoom(map.getZoom())
    }

    const onMoveEnd = () => {
      console.log(map.getCenter())
      setCenter(map.getCenter())
    }

    const updateCorners = () => {
      const bounds = map.getBounds()
      const ne = bounds.getNorthEast()
      const sw = bounds.getSouthWest()

      console.log(`ne ${ne} sw ${sw}`)
      onMoveEnd()
      onZoomEnd()
      setCorners({
        northEast: ne,
        southWest: sw,
      })
    }

    map.on('zoomend', updateCorners)
    map.on('moveend', updateCorners)

    // Cleanup listeners on unmount
    return () => {
      map.off('zoomend', updateCorners)
      map.off('moveend', updateCorners)
    }
  }, [map])

  return (
    <div className="map-info">
      <p>Zoom Level: {zoom}</p>
      <p>
        Center: {center.lat.toFixed(4)}, {center.lng.toFixed(4)}
      </p>
    </div>
  )
}

const TestMap = () => (
  <MapContainer
    center={[51.505, -0.09]}
    zoom={13}
    style={{ height: '100vh', width: '100%' }}
  >
    <TileLayer
      attribution='&copy; <a href="https://osm.org/copyright">OpenStreetMap</a>'
      url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
    />
    <MapInfo />
  </MapContainer>
)

export default TestMap
