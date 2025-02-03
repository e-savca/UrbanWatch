import { useSearchParams } from 'react-router';
import { useEffect, useState } from 'react';
import { StopDTO, RouteDTO, TripDTO } from '../../dto/TranzyDTOs';
import TransportUnitOfWork from '../../repositories/TransportRepositories/TransportUnitOfWork';

interface BusStopModalProps {
  onClose: () => void;
  stop: StopDTO | null;
  transportUnitOfWork: TransportUnitOfWork;
}

export function BusStopModal({
  onClose,
  stop,
  transportUnitOfWork,
}: BusStopModalProps) {
  const [afiliateRoutes, setAfiliateRoutes] = useState<
    { route: RouteDTO; trip: TripDTO }[]
  >([]);
  const [searchParams, setSearchParams] = useSearchParams();

  useEffect(() => {
    const fetchRoutes = async () => {
      if (!stop) return;

      try {
        const stopTimes = await transportUnitOfWork.StopTimes.getByStopId(
          stop.stop_id
        );

        const tripIds = stopTimes.map(stopTime => stopTime.trip_id);

        const routePairs = await Promise.all(
          tripIds.map(async tripId => {
            const trip = await transportUnitOfWork.Trips.getById(tripId);
            if (!trip) return undefined;
            const route = await transportUnitOfWork.Routes.getById(
              trip.route_id
            );

            return { route, trip };
          })
        );
        if (!routePairs) return;
        setAfiliateRoutes(
          routePairs.filter(
            (pair): pair is { route: RouteDTO; trip: TripDTO } =>
              pair !== undefined
          )
        );
      } catch (error) {
        console.error('Error fetching routes:', error);
      }
    };

    fetchRoutes();
  }, [stop, transportUnitOfWork]);

  const handleClose = (e: React.MouseEvent<HTMLDivElement>) => {
    if ((e.target as HTMLDivElement).id === 'modal-overlay') {
      onClose();
    }
  };

  const onRouteSelected = (routeId: number, directionId: number) => {
    const newParams = new URLSearchParams(searchParams);
    newParams.set('route', routeId.toString());
    newParams.set('direction', directionId.toString());
    setSearchParams(newParams);
    console.log(newParams);
  };

  const renderRoutes = () => {
    if (afiliateRoutes.length === 0) {
      return <p className="text-gray-500 text-center">No routes available.</p>;
    }

    return afiliateRoutes.map(({ route, trip }) => (
      // eslint-disable-next-line jsx-a11y/no-static-element-interactions
      <div
        key={trip.trip_id}
        className="flex items-center p-3 bg-gray-100 rounded-lg shadow-sm hover:bg-gray-200 transition-all"
        onClick={() => onRouteSelected(route.route_id, trip.direction_id)}
        onKeyDown={e => {
          // if is escape close modal
          if (e.key === 'Escape') {
            onClose();
          }
        }}
      >
        <div
          className="h-10 w-10 flex items-center justify-center
                         bg-blue-500 text-white font-bold rounded-full mr-4"
        >
          {route.route_short_name}
        </div>
        <div>
          <p className="text-gray-700 font-medium">{route.route_long_name}</p>
          <p className="text-gray-500 text-sm">
            {trip.trip_headsign || 'No destination info'}
          </p>
        </div>
      </div>
    ));
  };

  return (
    // eslint-disable-next-line jsx-a11y/click-events-have-key-events
    <div
      id="modal-overlay"
      className="fixed inset-0 z-50 flex items-center justify-center bg-black bg-opacity-50"
      onClick={handleClose}
    >
      <div
        className="relative bg-white rounded-lg shadow-2xl p-6
                         w-full max-w-md md:max-w-lg lg:max-w-xl
                         sm:w-11/12 sm:mx-4 md:w-auto animate-fade-in m-3"
      >
        <button
          className="absolute top-4 right-4 text-gray-700 text-2xl font-bold hover:text-gray-900 transition-all"
          onClick={onClose}
          type="button"
          aria-label="Close modal"
        >
          &times;
        </button>
        <h2 className="text-2xl font-semibold text-gray-800 mb-4 text-center">
          {stop?.stop_name || 'Bus Stop'}
        </h2>
        <div className="max-h-80 overflow-y-auto space-y-3">
          {renderRoutes()}
        </div>
      </div>
    </div>
  );
}

export default BusStopModal;
