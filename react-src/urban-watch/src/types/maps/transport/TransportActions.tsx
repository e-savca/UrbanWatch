import { LngLat } from 'maplibre-gl';
import {
  RouteDTO,
  ShapeDTO,
  TripDTO,
  VehicleDTO,
} from '../../../dto/TranzyDTOs';
import { TransportState } from './TransportState';

export enum TransportActionTypes {
  InitializeState,
  SetRoute,
  SetDirection,
  SetTripsOnRoute,
  SetRouteShapes,
  SetVehicles,
  SetMapCenter,
  IncreaseMapKey,
  SetUserGeolocation,
  SetModalIsOpen,
  SetSelectedStation,
  SetRoutesAffiliatedToSelectedStation,
}

export type TransportActions =
  | { type: TransportActionTypes.InitializeState; payload: TransportState }
  | { type: TransportActionTypes.SetRoute; payload: RouteDTO | undefined }
  | { type: TransportActionTypes.SetDirection; payload: number }
  | {
      type: TransportActionTypes.SetTripsOnRoute;
      payload: TripDTO[] | undefined;
    }
  | {
      type: TransportActionTypes.SetVehicles;
      payload: VehicleDTO[] | undefined;
    }
  | {
      type: TransportActionTypes.SetRouteShapes;
      payload: ShapeDTO[] | undefined;
    }
  | { type: TransportActionTypes.SetMapCenter; payload: LngLat }
  | { type: TransportActionTypes.IncreaseMapKey }
  | {
      type: TransportActionTypes.SetUserGeolocation;
      payload: Array<number>;
    };
