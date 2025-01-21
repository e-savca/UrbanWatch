import Stops from '../data/Stops'

function haversineDistance(lat1Deg, lon1Deg, lat2Deg, lon2Deg) {
  function toRad(degree) {
    return (degree * Math.PI) / 180
  }

  const lat1 = toRad(lat1Deg)
  const lon1 = toRad(lon1Deg)
  const lat2 = toRad(lat2Deg)
  const lon2 = toRad(lon2Deg)

  const { sin, cos, sqrt, atan2 } = Math

  const R = 6371 // earth radius in km
  const dLat = lat2 - lat1
  const dLon = lon2 - lon1
  const a =
    sin(dLat / 2) * sin(dLat / 2) +
    cos(lat1) * cos(lat2) * sin(dLon / 2) * sin(dLon / 2)
  const c = 2 * atan2(sqrt(a), sqrt(1 - a))
  const d = R * c
  return d * 1000 // distance in m
}

export default class StopsRepository {
  GetStopByStopId(stopId) {
    return Stops.find((x) => x.stop_id === stopId)
  }
  FindNearbyStops(lat, lon, radius = 500) {
    return Stops.filter((stop) => {
      const distance = haversineDistance(lat, lon, stop.stop_lat, stop.stop_lon)
      return distance < radius
    })
  }
}
