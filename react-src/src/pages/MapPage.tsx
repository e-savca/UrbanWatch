import 'maplibre-gl/dist/maplibre-gl.css';
import '@maplibre/maplibre-gl-geocoder/dist/maplibre-gl-geocoder.css';
import { useRef, useMemo, useEffect, useCallback, useState } from 'react';
import useMapHashParams from '../custom-hooks/map-hooks/useMapHashParams';

import TransportUnitOfWork from '../repositories/TransportRepositories/TransportUnitOfWork';
import TransportUtilities from '../utils/TransportUtilities';
import useMapState from '../custom-hooks/map-hooks/useMapState';
import useInitializeMap from '../custom-hooks/map-hooks/useInitializeMap';
import useVehicles from '../custom-hooks/map-hooks/useVehicles';
import useBusStops from '../custom-hooks/map-hooks/useBusStops';
import { StopDTO } from '../dto/TranzyDTOs';
import BusStopModal from '../components/map-components/BusStopModal';

function MapPage(): JSX.Element {
  const mapContainerRef = useRef<HTMLDivElement | null>(null);

  const transportUnitOfWork = useMemo(() => new TransportUnitOfWork(), []);
  const [zoom, center, onMoveEnd] = useMapHashParams();
  const [selectedStop, setSelectedStop] = useState<StopDTO | null>(null);
  const {
    routes,
    selectedRoute,
    tripsOnSelectedRoute,
    tripDirection,
    setTripDirection,
    selectedTrip,
    handleRouteChange,
    handleRouteAndDirectionChange,
  } = useMapState(transportUnitOfWork);

  const mapRef = useInitializeMap(mapContainerRef, zoom, center);
  const handleMoveEnd = useCallback(() => {
    onMoveEnd(mapRef.current);
  }, [mapRef, onMoveEnd]);

  // subscribe events on map
  useEffect(() => {
    const map = mapRef.current;

    map?.on('moveend', handleMoveEnd);

    return () => {
      if (map) {
        map?.off('moveend', handleMoveEnd);
      }
    };
  }, [handleMoveEnd, mapRef]);

  // Use custom hook to fetch bus stops and update the map
  useBusStops(mapRef, transportUnitOfWork, setSelectedStop);
  // Use custom hook to fetch vehicles and update map
  useVehicles(mapRef, selectedTrip, transportUnitOfWork);

  return (
    <>
      {selectedStop && (
        <BusStopModal
          stop={selectedStop}
          onClose={() => setSelectedStop(null)}
          onRouteChange={handleRouteAndDirectionChange}
          transportUnitOfWork={transportUnitOfWork}
        />
      )}
      <div id="map-root" className="absolute inset-0">
        <div
          id="map-container"
          ref={mapContainerRef}
          className="h-full w-full bg-gray-300"
        />

        <div className="absolute top-2 z-10 bg-white p-2 m-2 rounded-lg shadow-md w-fit sm:max-w-sm md:max-w-md lg:max-w-lg">
          <select
            value={selectedRoute}
            onChange={handleRouteChange}
            className="p-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 w-full"
          >
            <option value="0">Select a route: Trolleybus or Bus</option>
            {routes.map(route => (
              <option key={route.route_id} value={route.route_id}>
                {TransportUtilities.getRouteTypeById(route.route_type)}{' '}
                {route.route_short_name}: {route.route_long_name}
              </option>
            ))}
          </select>

          {selectedRoute !== 0 && (
            <select
              value={tripDirection}
              onChange={e => setTripDirection(Number(e.target.value))}
              className="p-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 w-full mt-2"
            >
              {tripsOnSelectedRoute?.map(trip => (
                <option key={trip.trip_id} value={trip.direction_id}>
                  {trip.trip_headsign}
                </option>
              ))}
            </select>
          )}
        </div>
      </div>
    </>
  );
}

export default MapPage;
