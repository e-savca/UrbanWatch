import Vehicles from '../data/Vehicles';

const API_KEY = import.meta.env.VITE_TRANZY_API_KEY;

export default class VehicleRepository {
  constructor(fakeDate) {
    this.fakeDate = fakeDate;
  }

  async GetVehiclesByTripId(tripId) {
    if (this.fakeDate) return Vehicles.filter((v) => v.trip_id === tripId);

    const url = 'https://api.tranzy.ai/v1/opendata/vehicles';
    const options = {
      method: 'GET',
      headers: {
        'X-Agency-Id': '4',
        Accept: 'application/json',
        'X-API-KEY': `${API_KEY}`,
      },
    };

    try {
      const response = await fetch(url, options);
      const data = await response.json();
      const filteredData = data.filter((item) => item.trip_id === tripId);
      // const filteredData = data.filter((item) =>
      //   Object.values(item).every((value) => value !== null)
      // )

      return filteredData;
    } catch (error) {
      console.error(error);
    }
    return '';
  }

  GetVehicleType(id) {
    switch (id) {
      case 0:
        return 'Tram, Streetcar, Light rail';
      case 1:
        return 'Subway, Metro';
      case 2:
        return 'Rail';
      case 3:
        return 'Bus';
      case 4:
        return 'Ferry';
      case 5:
        return 'Cable tram';
      case 6:
        return 'Aerial lift';
      case 7:
        return 'Funicular';
      case 11:
        return 'Trolleybus';
      case 12:
        return 'Monorail';
      default:
        return 'Unknown vehicle type';
    }
  }
}
