import { RouteDTO, ShapeDTO, VehicleDTO } from '../../../dto/TranzyDTOs';

export interface TransportState {
  route: RouteDTO;
  tripDirection: number;
  routeShapes: Array<ShapeDTO>;
  mapCenter: Array<number>;
  mapKey: number;
  userGeolocation: Array<number> | null;
  vehicles: VehicleDTO[];
}
