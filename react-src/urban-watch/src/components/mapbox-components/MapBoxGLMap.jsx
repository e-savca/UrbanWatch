import maplibregl from 'maplibre-gl'
import 'maplibre-gl/dist/maplibre-gl.css'
import { useRef, useState } from 'react'
import { useEffect } from 'react'
import { defaultCenterPositionOnMapLngLat } from '../../data/AppData'
import { GetUserPositionOnMap } from '../../utils/GetUserGeoLocation'

const tileStyles = {
  MapMD_2D:
    'https://map.md/api/tiles/styles/map/style.json?v=2018-12-28T00:00:00.000Z',
  MapMD_3D:
    'https://map.md/api/tiles/styles/satelite/style.json?v=2018-12-28T00:00:00.000Z',
  CARTO_Voyager: {
    version: 8,
    sources: {
      osm: {
        type: 'raster',
        tiles: [
          'https://basemaps.cartocdn.com/rastertiles/voyager/{z}/{x}/{y}{r}.png',
        ],
        attribution:
          '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors &copy; <a href="https://carto.com/attributions">CARTO</a>',
        tileSize: 256,
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
}

function MapLibreGLMap() {
  const mapRef = useRef(null)
  const mapContainerRef = useRef(null)
  const markerRef = useRef(null)
  const [mapCenter, setMapCenter] = useState(defaultCenterPositionOnMapLngLat)

  useEffect(() => {
    const getLocation = async () => {
      try {
        const result = await GetUserPositionOnMap()
        if (mapCenter[0] !== result[0] || mapCenter[1] !== result[1]) {
          setMapCenter(result)
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
      style: tileStyles.MapMD_2D,
      center: mapCenter,
      zoom: 14,
      attributionControl: false,
    })

    mapRef.current.addControl(
      new maplibregl.AttributionControl({
        customAttribution: '&copy; <a href="/" target="_blank">UrbanWatch</a>',
        compact: true,
      })
    )

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
        color: '#ff0000',
        draggable: false,
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
