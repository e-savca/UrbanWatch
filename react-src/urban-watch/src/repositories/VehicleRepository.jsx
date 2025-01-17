import Vehicles from '../data/Vehicles'
export default class VehicleRepository {
  constructor() {}
  GetVehiclesByTripId(tripId) {
    return Vehicles.filter((v) => v.trip_id === tripId)
  }

  GetVehicleType(id) {
    switch (id) {
      case 0:
        return 'Tram, Streetcar, Light rail'
      case 1:
        return 'Subway, Metro'
      case 2:
        return 'Rail'
      case 3:
        return 'Bus'
      case 4:
        return 'Ferry'
      case 5:
        return 'Cable tram'
      case 6:
        return 'Aerial lift'
      case 7:
        return 'Funicular'
      case 11:
        return 'Trolleybus'
      case 12:
        return 'Monorail'
      default:
        return 'Unknown vehicle type'
    }
  }
}
