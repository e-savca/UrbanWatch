import { RouteType } from '../dto/TranzyDTOs';

export default class TransportUtilities {
  static getRouteTypeById(transportTypeId: RouteType): string {
    switch (transportTypeId) {
      case RouteType.Tram:
        return 'Tram';
      case RouteType.Subway:
        return 'Subway';
      case RouteType.Rail:
        return 'Rail';
      case RouteType.Bus:
        return 'Bus';
      case RouteType.Ferry:
        return 'Ferry';
      case RouteType.CableTram:
        return 'Cable tram';
      case RouteType.AerialLift:
        return 'Aerial lift';
      case RouteType.Funicular:
        return 'Funicular';
      case RouteType.Trolleybus:
        return 'Trolleybus';
      case RouteType.Monorail:
        return 'Monorail';
      default:
        return 'Unknown';
    }
  }
}
