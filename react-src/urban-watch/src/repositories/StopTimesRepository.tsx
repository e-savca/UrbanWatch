import { StopTimeDTO } from '../dto/TranzyDTOs';

const API_KEY = import.meta.env.VITE_TRANZY_API_KEY;
const API_URL = 'https://api.tranzy.ai/v1/opendata/stop_times';

async function fetchStopTimes(): Promise<StopTimeDTO[]> {
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
        `Failed to fetch stop times: ${response.status} ${response.statusText}`,
      );
    }

    const data: StopTimeDTO[] = await response.json();

    // Validate response data (basic check)
    if (!Array.isArray(data)) {
      throw new Error(
        'Invalid response format, expected an array of stop times.',
      );
    }

    return data;
  } catch (error) {
    throw new Error(`Error fetching stop times: ${(error as Error).message}`);
  }
}

const stopTimes: StopTimeDTO[] = await fetchStopTimes();

export default class StopTimesRepository {
  stopTimes: StopTimeDTO[];

  constructor() {
    this.stopTimes = stopTimes;
  }

  GetStopTimesByTripId(tripId: string) {
    return this.stopTimes.filter((x) => x.trip_id === tripId);
  }

  GetStopId(tripId: string) {
    return (
      this.stopTimes.find((stop) => stop.trip_id === tripId)?.stop_id || null
    );
  }

  GetStopTimesByStopId(stopId: number) {
    return this.stopTimes.filter((stop) => stop.stop_id === stopId);
  }
}
