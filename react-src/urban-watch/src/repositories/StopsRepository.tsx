import Stops from '../data/Stops';

export default class StopsRepository {
  GetStopByStopId(stopId) {
    return Stops.find((x) => x.stop_id === stopId);
  }

  GetAllStops() {
    return Stops;
  }
}
