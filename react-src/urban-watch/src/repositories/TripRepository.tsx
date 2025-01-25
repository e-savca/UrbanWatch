import { TripDTO } from '../dto/TranzyDTOs';

const API_KEY = import.meta.env.VITE_TRANZY_API_KEY;
const API_URL = 'https://api.tranzy.ai/v1/opendata/trips';

async function fetchTrips(): Promise<TripDTO[]> {
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
        `Failed to fetch trips: ${response.status} ${response.statusText}`,
      );
    }

    const data: TripDTO[] = await response.json();

    // Validate response data (basic check)
    if (!Array.isArray(data)) {
      throw new Error('Invalid response format, expected an array of trips.');
    }

    return data;
  } catch (error) {
    throw new Error(`Error fetching trips: ${(error as Error).message}`);
  }
}

const trips: TripDTO[] = await fetchTrips();

export default class TripRepository {
  trips: TripDTO[];

  constructor() {
    this.trips = trips;
  }

  GetTripById(tripId: string) {
    return this.trips.find((x) => x.trip_id === tripId);
  }

  GetTripsByRouteId(routeId: number) {
    return this.trips.filter((x) => x.route_id === routeId);
  }

  GetRouteIdByTripId(tripId: string) {
    return this.trips.find((trip) => trip.trip_id === tripId)?.route_id || null;
  }
}
