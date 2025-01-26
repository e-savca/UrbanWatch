import { useEffect, useState } from 'react';

import { RouteDTO, TripDTO } from '../../dto/TranzyDTOs';
import { TransportActions, TransportActionTypes } from '../../types/maps';
import TransportUnitOfWork from '../../repositories/TransportRepositories/TransportUnitOfWork';
import TransportUtilities from '../../utils/TransportUtilities';

const transportUOW = new TransportUnitOfWork();

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
  const [selectedRoute, setSelectedRoute] = useState(route.route_id);
  const [allRoutes, setAllRoutes] = useState<RouteDTO[]>([]);

  useEffect(() => {
    async function fetchAllRoutes() {
      const routes = await transportUOW.Routes.getAll();
      setAllRoutes(routes);
    }
    fetchAllRoutes();

    return () => {
      transportUOW.Routes.abortPreviousRequest();
    };
  }, []);

  return (
    <div className="absolute z-10 flex flex-wrap sm:flex-nowrap flex-col sm:flex-row space-y-4 sm:space-y-0 sm:space-x-4 p-4 bg-gray-100 shadow w-full">
      <select
        className="p-2 rounded border border-gray-300 bg-white w-full flex-grow"
        value={selectedRoute}
        onChange={e => {
          setSelectedRoute(Number(e.target.value));
          dispatch({
            type: TransportActionTypes.SetRoute,
            payload: allRoutes.find(r => r.route_id === Number(e.target.value)),
          });
        }}
      >
        {allRoutes.map(r => (
          <option key={r.route_id} value={r.route_id}>
            {TransportUtilities.getRouteTypeById(r.route_type)}{' '}
            {r.route_short_name}: {r.route_long_name}
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
