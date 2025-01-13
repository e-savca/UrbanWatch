import Trips from '../data/Trips'

export default class TripRepository {
  GetTripById(tripId) {
    return Trips.find((x) => x.trip_id === tripId)
  }
  GetTripsByRouteId(routeId) {
    return Trips.filter((x) => x.route_id === routeId)
  }
}
