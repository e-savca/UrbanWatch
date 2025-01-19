import PropTypes from 'prop-types'

import VehicleRepository from '../../repositories/VehicleRepository.jsx'
import RoutesData from '../../data/Routes.jsx'

const vehicleRepository = new VehicleRepository()

MapSelect.propTypes = {
  route: PropTypes.object,
  tripDirection: PropTypes.number,
  tripsOnRoute: PropTypes.array,
  dispatch: PropTypes.func,
}

function MapSelect({ route, tripDirection, tripsOnRoute, dispatch }) {
  return (
    <div className="absolute z-10 flex flex-wrap sm:flex-nowrap flex-col sm:flex-row space-y-4 sm:space-y-0 sm:space-x-4 p-4 bg-gray-100 shadow w-full">
      <select
        className="p-2 rounded border border-gray-300 bg-white w-full flex-grow"
        value={route.route_id}
        onChange={(e) =>
          dispatch({ type: 'SET_ROUTE', payload: e.target.value })
        }
      >
        {RoutesData.map((r) => (
          <option key={r.route_id} value={r.route_id}>
            {vehicleRepository.GetVehicleType(r.route_type)}{' '}
            {r.route_short_name}: {r.route_long_name}
          </option>
        ))}
      </select>
      <select
        className="p-2 rounded border border-gray-300 bg-white w-full flex-grow"
        value={tripDirection}
        onChange={(e) =>
          dispatch({ type: 'SET_DIRECTION', payload: e.target.value })
        }
      >
        {tripsOnRoute.map((t) => (
          <option key={t.trip_id} value={t.direction_id}>
            to {t.trip_headsign}
          </option>
        ))}
      </select>
    </div>
  )
}

export default MapSelect
