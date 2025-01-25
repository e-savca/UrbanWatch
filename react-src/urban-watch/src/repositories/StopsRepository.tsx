import { StopDTO } from '../dto/TranzyDTOs';

const API_KEY = import.meta.env.VITE_TRANZY_API_KEY;
const API_URL = 'https://api.tranzy.ai/v1/opendata/stops';

async function fetchStops(): Promise<StopDTO[]> {
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
        `Failed to fetch stops: ${response.status} ${response.statusText}`,
      );
    }

    const data: StopDTO[] = await response.json();

    // Validate response data (basic check)
    if (!Array.isArray(data)) {
      throw new Error('Invalid response format, expected an array of stops.');
    }

    return data;
  } catch (error) {
    throw new Error(`Error fetching stops: ${(error as Error).message}`);
  }
}

const stops: StopDTO[] = await fetchStops();

export default class StopsRepository {
  stops: StopDTO[];

  constructor() {
    this.stops = stops;
  }

  GetStopByStopId(stopId: number) {
    return this.stops.find((x) => x.stop_id === stopId);
  }

  GetAllStops() {
    return this.stops;
  }
}
