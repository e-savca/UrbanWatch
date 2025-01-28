import maplibregl from 'maplibre-gl';
import 'maplibre-gl/dist/maplibre-gl.css';
import '@maplibre/maplibre-gl-geocoder/dist/maplibre-gl-geocoder.css';
import { useRef, useEffect, useCallback, useMemo, useState } from 'react';
import { mapTiles } from '../data/AppData';
import useMapHashParams from '../custom-hooks/map-hooks/useMapHashParams';
import {
  createAttributionControl,
  createFullscreenControl,
  // createGeocoderApi,
  createGeolocateControl,
  createNavigationControl,
} from '../utils/map-utils/Controls';
import TransportUnitOfWork from '../repositories/TransportRepositories/TransportUnitOfWork';
import { RouteDTO, TripDTO, VehicleDTO } from '../dto/TranzyDTOs';
import TransportUtilities from '../utils/TransportUtilities';
import convertVehiclesToGeoJSON from '../utils/mapping/MapToGeoJson';

function MapPage(): JSX.Element {
  const mapRef = useRef<maplibregl.Map | null>(null);
  const mapContainerRef = useRef<HTMLDivElement | null>(null);

  const transportUnitOfWork = useMemo(() => new TransportUnitOfWork(), []);
  const [{ zoom, center }, setMapParams] = useMapHashParams();
  const [routes, setRoutes] = useState<RouteDTO[]>([]);
  const [selectedRoute, setSelectedRoute] = useState<number>(0);
  const [tripDirection, setTripDirection] = useState<number>(0);
  const tripRef = useRef<TripDTO | null>(null);

  const handleRouteChange = useCallback((e: { target: { value: string } }) => {
    const routeId = Number(e.target.value);
    setSelectedRoute(routeId);
  }, []);

  useEffect(() => {
    async function UpdateTrip() {
      const trips = await transportUnitOfWork.Trips.getByRouteId(selectedRoute);
      tripRef.current = trips?.at(0) || null;
    }
    UpdateTrip();
  }, [selectedRoute, transportUnitOfWork.Trips]);

  useEffect(() => {
    const fetchRoutes = async () => {
      const fetchedRoutes = await transportUnitOfWork.Routes.getAll();
      setRoutes(fetchedRoutes);
    };
    fetchRoutes();
  }, [transportUnitOfWork.Routes]);

  useEffect(() => {
    // Fetch vehicle data and convert it to GeoJSON
    const fetchVehicles = async () => {
      let vehicleData: VehicleDTO[];
      if (tripRef.current?.trip_id) {
        const fetchVehicleData = await transportUnitOfWork.Vehicles.getByTripId(
          tripRef.current?.trip_id
        );
        if (fetchVehicleData) vehicleData = fetchVehicleData;
      }

      if (mapRef.current && vehicleData?.length > 0) {
        const geojson = convertVehiclesToGeoJSON(vehicleData);

        // Add GeoJSON source
        mapRef.current.addSource('vehicles', {
          type: 'geojson',
          data: geojson,
        });

        // Add a circle layer to display vehicle points
        mapRef.current.addLayer({
          id: 'vehicle-points',
          type: 'circle',
          source: 'vehicles',
          paint: {
            'circle-radius': 6,
            'circle-color': '#007cbf',
          },
        });
      }
    };

    fetchVehicles();
  }, [transportUnitOfWork.Vehicles]);

  const handleMoveEnd = useCallback(() => {
    const newCenter = mapRef.current?.getCenter();
    const newZoom = mapRef.current?.getZoom();

    // implemented later. because map.md api provides map just for Moldova territory
    // console.log(newZoom);

    // if (newZoom && newZoom < 10)
    //   mapRef.current?.setStyle(mapTiles.CARTO_Voyager);

    // if (newZoom && newZoom >= 10) mapRef.current?.setStyle(mapTiles.MapMD_2D);

    if (newZoom && newCenter)
      setMapParams(newZoom, newCenter.lat, newCenter.lng);
  }, [setMapParams]);

  // memoize map controls
  const attributionControl = useMemo(() => createAttributionControl(), []);
  const navigationControl = useMemo(() => createNavigationControl(), []);
  const geolocationControl = useMemo(() => createGeolocateControl(), []);
  const fullscreenControl = useMemo(() => createFullscreenControl(), []);
  // const geocoderApi = useMemo(() => createGeocoderApi(), []);

  useEffect(() => {
    mapRef.current = new maplibregl.Map({
      container: mapContainerRef.current || '',
      hash: false,
      style: mapTiles.MapMD_2D,
      center,
      zoom,
      maxZoom: 19,
      minZoom: 1,
      attributionControl: false,
    });

    // Update URL search parameters when map moves
    mapRef.current.on('moveend', handleMoveEnd);

    mapRef.current.addControl(attributionControl);

    mapRef.current.addControl(navigationControl, 'bottom-right');

    mapRef.current.addControl(geolocationControl, 'bottom-right');

    mapRef.current.addControl(fullscreenControl, 'bottom-right');

    // mapRef.current.addControl(geocoderApi, 'top-left');

    return () => {
      if (mapRef.current) mapRef.current.remove();
    };
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <div id="map-root" className="absolute inset-0">
      <div
        id="map-container"
        ref={mapContainerRef}
        className="h-full w-full bg-gray-300"
      />

      <div className="absolute top-4 left-4 z-10 bg-white p-3 rounded-lg shadow-md">
        <select
          value={selectedRoute}
          onChange={handleRouteChange}
          className="p-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
        >
          <option value="0">Select a route: Trolleybus or Bus</option>
          {routes.map(route => (
            <option key={route.route_id} value={route.route_id}>
              {TransportUtilities.getRouteTypeById(route.route_type)}{' '}
              {route.route_short_name}: {route.route_long_name}
            </option>
          ))}
        </select>
      </div>

      <div className="absolute top-4 left-4 z-10 bg-white p-3 rounded-lg shadow-md">
        <select
          value={tripDirection}
          onChange={e => setTripDirection(Number(e.target.value))}
          className="p-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
        >
          <option value={0}>Tur</option>
          <option value={1}>Re-Tur</option>
        </select>
      </div>
    </div>
  );
}

export default MapPage;
