import 'leaflet/dist/leaflet.css' // IMPORTANT for map to work properly
import { useEffect, useState } from 'react'
import { MapContainer, Marker, Popup, TileLayer, useMap } from 'react-leaflet'
import { defaultCenterPositionOnMap } from '../../data/AppData'
import Stops from '../../data/Stops'
import BusIcon from './BusIcon'

const getNumberMultiplyiedByHundred = (num) => Number(num) * 100
const getKeyBasedOnLatAndLon = (lat, lon) => `${lat}_${lon}`

const preprocessAndIndexStations = (stations) => {
  const index = {}

  stations.forEach((station) => {
    const latShort = Math.floor(getNumberMultiplyiedByHundred(station.stop_lat))
    const lonShort = Math.floor(getNumberMultiplyiedByHundred(station.stop_lon))
    const key = getKeyBasedOnLatAndLon(latShort, lonShort)
    if (!index[key]) {
      index[key] = []
    }

    index[key].push(station)
  })

  return index
}

const getTruncatedBounds = (bounds) => {
  const southWest = bounds.getSouthWest()
  const northEast = bounds.getNorthEast()

  return {
    latMin: Math.floor(getNumberMultiplyiedByHundred(southWest.lat)),
    latMax: Math.floor(getNumberMultiplyiedByHundred(northEast.lat)),
    lonMin: Math.floor(getNumberMultiplyiedByHundred(southWest.lng)),
    lonMax: Math.floor(getNumberMultiplyiedByHundred(northEast.lng)),
  }
}

const filterStations = (index, truncatedBounds, exactBounds) => {
  const { latMin, latMax, lonMin, lonMax } = truncatedBounds
  const visibleStations = []

  for (let lat = latMin; lat <= latMax; lat++) {
    for (let lon = lonMin; lon <= lonMax; lon++) {
      const key = `${lat}_${lon}`
      if (index[key]) {
        index[key].forEach((station) => {
          if (
            station.stop_lat >= exactBounds.getSouthWest().lat &&
            station.stop_lat <= exactBounds.getNorthEast().lat &&
            station.stop_lon >= exactBounds.getSouthWest().lng &&
            station.stop_lon <= exactBounds.getNorthEast().lng
          ) {
            visibleStations.push(station)
          }
        })
      }
    }
  }

  return visibleStations
}

const index = preprocessAndIndexStations(Stops)

const MapInfo = () => {
  const map = useMap()
  const [zoom, setZoom] = useState(map.getZoom())
  const [center, setCenter] = useState(map.getCenter())
  const [corners, setCorners] = useState(null)
  const [stations, setStations] = useState([])

  useEffect(() => {
    if (zoom < 15) {
      setStations([])
    } else {
      const bounds = map.getBounds()
      // console.log(bounds)
      const truncatedBounds = getTruncatedBounds(bounds)
      // console.log(truncatedBounds)

      const filtered = filterStations(index, truncatedBounds, bounds)
      console.log(filtered.length)
      setStations(filtered)
    }
  }, [corners, zoom, center, map])

  useEffect(() => {
    const updateCorners = () => {
      setCenter(map.getCenter())
      setZoom(map.getZoom())
      console.log('zoom ->' + map.getZoom())
      const bounds = map.getBounds()
      const ne = bounds.getNorthEast()
      const sw = bounds.getSouthWest()

      // console.log(`ne ${ne} sw ${sw}`)
      setCorners({
        northEast: ne,
        southWest: sw,
      })
    }

    map.on('moveend', updateCorners)

    return () => {
      map.off('moveend', updateCorners)
    }
  }, [map])

  return (
    <>
      {stations.map((station) => (
        <Marker
          key={station.stop_id}
          position={[station.stop_lat, station.stop_lon]}
          icon={BusIcon}
        >
          <Popup>{station.stop_name}</Popup>
        </Marker>
      ))}
    </>
  )
}

const TestMap = () => (
  <MapContainer
    center={defaultCenterPositionOnMap}
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
