import StopTimes from '../data/StopTimes'

export default class StopTimesRepository {
  GetStopTimesByTripId(tripId) {
    return StopTimes.filter((x) => x.trip_id === tripId)
  }
}
