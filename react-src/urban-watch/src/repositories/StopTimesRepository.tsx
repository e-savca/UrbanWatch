import StopTimes from '../data/StopTimes';

export default class StopTimesRepository {
  GetStopTimesByTripId(tripId) {
    return StopTimes.filter((x) => x.trip_id === tripId);
  }

  GetStopId(tripId) {
    return StopTimes.find((stop) => stop.trip_id === tripId).stop_id;
  }

  GetStopTimesByStopId(stopId) {
    return StopTimes.filter((stop) => stop.stop_id === stopId);
  }
}
