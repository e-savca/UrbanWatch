import { LngLat } from 'maplibre-gl';
import {
  RouteDTO,
  ShapeDTO,
  TripDTO,
  VehicleDTO,
} from '../../../dto/TranzyDTOs';

export interface TransportState {
  route?: RouteDTO;
  tripDirection?: number;
  trip?: TripDTO;
  tripsOnRoute?: TripDTO[];
  routeShapes?: Array<ShapeDTO>;
  mapCenter: LngLat;
  mapKey: number;
  userGeolocation?: Array<number>;
  vehicles?: VehicleDTO[];
}
