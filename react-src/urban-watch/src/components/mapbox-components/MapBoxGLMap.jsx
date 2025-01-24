import maplibregl from 'maplibre-gl'
import 'maplibre-gl/dist/maplibre-gl.css'
import { useRef, useState } from 'react'
import { useEffect } from 'react'
import { defaultCenterPositionOnMapLngLat } from '../../data/AppData'
import { GetUserPositionOnMap } from '../../utils/GetUserGeoLocation'

// Import an icon (local or online URL)
const userIcon = 'https://cdn-icons-png.flaticon.com/32/684/684908.png'

// source: https://leaflet-extras.github.io/leaflet-providers/preview/
const tileLayers = {
  default: 'https://a.tile.openstreetmap.org/{z}/{x}/{y}.png',
  voyager:
    'https://a.basemaps.cartocdn.com/rastertiles/voyager/{z}/{x}/{y}{r}.png',
}

function MapLibreGLMap() {
  const mapRef = useRef(null)
  const mapContainerRef = useRef(null)
  const markerRef = useRef(null) // Reference for marker instance
  const [mapCenter, setMapCenter] = useState(defaultCenterPositionOnMapLngLat)

  useEffect(() => {
    const getLocation = async () => {
      try {
        const result = await GetUserPositionOnMap()
        if (mapCenter[0] !== result[0] || mapCenter[1] !== result[1]) {
          setMapCenter(result)
          console.log(result)
        }
      } catch (error) {
        console.error('Error fetching geolocation:', error)
      }
    }

    getLocation()
  }, [])

  useEffect(() => {
    mapRef.current = new maplibregl.Map({
      container: mapContainerRef.current,
      style: {
        version: 8,
        sources: {
          osm: {
            type: 'raster',
            tiles: [tileLayers.voyager],
            tileSize: 256,
            attribution:
              '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> | &copy; <a href="/">UrbanWatch</a> ',
          },
        },
        layers: [
          {
            id: 'osm-tiles',
            type: 'raster',
            source: 'osm',
            minzoom: 0,
            maxzoom: 19,
          },
        ],
      },
      center: mapCenter,
      zoom: 14,
    })

    return () => {
      if (mapRef.current) mapRef.current.remove()
    }
  }, [])

  useEffect(() => {
    if (mapRef.current) {
      mapRef.current.setCenter(mapCenter)

      if (markerRef.current) {
        markerRef.current.remove()
      }

      markerRef.current = new maplibregl.Marker({
        color: '#ff0000', // Optional color customization
        draggable: false, // Prevent moving the marker
      })
        .setLngLat(mapCenter)
        .setPopup(new maplibregl.Popup().setHTML('<h3>You are here</h3>'))
        .addTo(mapRef.current)
    }
  }, [mapCenter])

  return (
    <div id="map-root" className="absolute inset-0">
      <div
        id="map-container"
        ref={mapContainerRef}
        className="h-full w-full bg-gray-300"
      />
    </div>
  )
}

export default MapLibreGLMap
