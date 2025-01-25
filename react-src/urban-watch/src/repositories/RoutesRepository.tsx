import { RouteDTO } from '../dto/TranzyDTOs';

const API_KEY = import.meta.env.VITE_TRANZY_API_KEY;
const API_URL = 'https://api.tranzy.ai/v1/opendata/routes';

async function fetchRoutes(): Promise<RouteDTO[]> {
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
        `Failed to fetch routes: ${response.status} ${response.statusText}`,
      );
    }

    const data: RouteDTO[] = await response.json();

    // Validate response data (basic check)
    if (!Array.isArray(data)) {
      throw new Error('Invalid response format, expected an array of routes.');
    }

    return data;
  } catch (error) {
    throw new Error(`Error fetching routes: ${(error as Error).message}`);
  }
}

export default class RoutesRepository {
  GetRouteById(routeId: number) {
    return Routes.find((route) => route.route_id === routeId);
  }

  GetAllRoutes() {
    return Routes;
  }
}
