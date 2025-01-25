import Routes from '../data/Routes';

export default class RoutesRepository {
  GetRouteById(routeId: number) {
    return Routes.find((route) => route.route_id === routeId);
  }

  GetAllRoutes() {
    return Routes;
  }
}
