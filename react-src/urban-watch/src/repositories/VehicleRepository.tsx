import { VehicleDTO } from '../dto/TranzyDTOs';

const API_KEY = import.meta.env.VITE_TRANZY_API_KEY;
const API_URL = 'https://api.tranzy.ai/v1/opendata/vehicles';

async function fetchVehicles(): Promise<VehicleDTO[]> {
  const options = {
    method: 'GET',
    headers: {
      'X-Agency-Id': '4',
      Accept: 'application/json',
      'X-API-KEY': API_KEY,
    },
  };

  try {
    const response = await fetch(API_URL, options);

    // Check if the response is successful
    if (!response.ok) {
      throw new Error(
        `Failed to fetch vehicles: ${response.status} ${response.statusText}`,
      );
    }

    const data: VehicleDTO[] = await response.json();

    // Validate response data (basic check)
    if (!Array.isArray(data)) {
      throw new Error(
        'Invalid response format, expected an array of vehicles.',
      );
    }

    return data;
  } catch (error) {
    throw new Error(`Error fetching vehicles: ${(error as Error).message}`);
  }
}

export default class VehicleRepository {
  static async GetVehiclesByTripId(tripId: string) {
    const vehicles = await fetchVehicles();

    const filteredData = vehicles.filter(
      (item: { trip_id: string | null }) => item.trip_id === tripId,
    );
    return filteredData;
  }

  static GetVehicleType(id: number) {
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
