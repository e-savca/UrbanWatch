import StopTimes from '../data/StopTimes'

export default class StopTimesRepository {
  GetStopTimesByTripId(tripId) {
    return StopTimes.filter((x) => x.trip_id === tripId)
  }

  GetStopId(tripId) {
    return StopTimes.find((stop) => stop.trip_id === tripId).stop_id
  }

  GetTripId(stopId) {
    return StopTimes.find((stop) => stop.stop_id === stopId).trip_id
  }
}
