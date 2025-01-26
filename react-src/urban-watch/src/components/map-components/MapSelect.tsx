import { RouteDTO, TripDTO } from '../../dto/TranzyDTOs';
import { TransportActions, TransportActionTypes } from '../../types/maps';
import TransportUnitOfWork from '../../repositories/TransportRepositories/TransportUnitOfWork';

const transportUOW = await TransportUnitOfWork.create();

function MapSelect({
  route,
  tripDirection,
  tripsOnRoute,
  dispatch,
}: {
  route: RouteDTO;
  tripDirection: number;
  tripsOnRoute: TripDTO[];
  dispatch: (action: TransportActions) => void;
}) {
  return (
    <div className="absolute z-10 flex flex-wrap sm:flex-nowrap flex-col sm:flex-row space-y-4 sm:space-y-0 sm:space-x-4 p-4 bg-gray-100 shadow w-full">
      <select
        className="p-2 rounded border border-gray-300 bg-white w-full flex-grow"
        value={route.route_id}
        onChange={e => {
          dispatch({
            type: TransportActionTypes.SetRoute,
            payload: Number(e.target.value),
          });
        }}
      >
        {transportUOW.Routes.getAll().map(r => (
          <option key={r.route_id} value={r.route_id}>
            {r.route_type} {r.route_short_name}:{r.route_long_name}
          </option>
        ))}
      </select>
      <select
        className="p-2 rounded border border-gray-300 bg-white w-full flex-grow"
        value={tripDirection}
        onChange={e =>
          dispatch({
            type: TransportActionTypes.SetDirection,
            payload: Number(e.target.value),
          })
        }
      >
        {tripsOnRoute.map(t => (
          <option key={t.trip_id} value={t.direction_id}>
            to {t.trip_headsign}
          </option>
        ))}
      </select>
    </div>
  );
}

export default MapSelect;
