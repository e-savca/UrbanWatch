import {
  RouteDTO,
  ShapeDTO,
  TripDTO,
  VehicleDTO,
} from '../../../dto/TranzyDTOs';

export interface TransportState {
  route: RouteDTO;
  tripDirection: number;
  trip: TripDTO;
  routeShapes: Array<ShapeDTO>;
  mapCenter: Array<number>;
  mapKey: number;
  userGeolocation: Array<number> | null;
  vehicles: VehicleDTO[];
}
