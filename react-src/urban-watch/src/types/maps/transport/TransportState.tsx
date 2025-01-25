import { RouteDTO, ShapeDTO } from '../../../dto/TranzyDTOs';

export interface TransportState {
  route: RouteDTO;
  tripDirection: number;
  routeShapes: Array<ShapeDTO> | null;
  mapKey: number;
  userGeolocation: Array<number>;
}
