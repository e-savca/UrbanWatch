export enum TransportActionTypes {
  SetRoute,
  SetDirection,
  SetRouteShapes,
  SetMapCenter,
  IncreaseMapKey,
  SetUserGeolocation,
  SetModalIsOpen,
  SetSelectedStation,
  SetRoutesAffiliatedToSelectedStation,
}

export type TransportActions =
  | { type: TransportActionTypes.SetRoute; payload: number }
  | { type: TransportActionTypes.SetDirection; payload: number }
  | { type: TransportActionTypes.SetRouteShapes; payload: Array<any> }
  | { type: TransportActionTypes.SetMapCenter; payload: Array<number> }
  | { type: TransportActionTypes.IncreaseMapKey }
  | {
      type: TransportActionTypes.SetUserGeolocation;
      payload: Array<number>;
    };
