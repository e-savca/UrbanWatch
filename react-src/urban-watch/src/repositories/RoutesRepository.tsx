import Routes from '../data/Routes'

export default class RoutesRepository {
  GetRouteById(routeId) {
    return Routes.find((route) => route.route_id === routeId)
  }

  GetAllRoutes() {
    return Routes
  }
}
