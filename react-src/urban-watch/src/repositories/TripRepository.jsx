import Trips from '../data/Trips'

export default class TripRepository {
  GetTripById(tripId) {
    return Trips.find((x) => x.trip_id === tripId)
  }
}
