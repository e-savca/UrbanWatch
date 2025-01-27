import maplibregl from 'maplibre-gl';
import 'maplibre-gl/dist/maplibre-gl.css';
import '@maplibre/maplibre-gl-geocoder/dist/maplibre-gl-geocoder.css';
import { useRef, useEffect, useCallback, useMemo } from 'react';
import { mapTiles } from '../data/AppData';
import useMapHashParams from '../custom-hooks/map-hooks/useMapHashParams';
import {
  createAttributionControl,
  createFullscreenControl,
  createGeocoderApi,
  createGeolocateControl,
  createNavigationControl,
} from '../utils/map-utils/Controls';

function MapPage(): JSX.Element {
  const mapRef = useRef<maplibregl.Map | null>(null);
  const mapContainerRef = useRef<HTMLDivElement | null>(null);
  const [{ zoom, center }, setMapParams] = useMapHashParams();

  const handleMoveEnd = useCallback(() => {
    const newCenter = mapRef.current?.getCenter();
    const newZoom = mapRef.current?.getZoom();
    if (newZoom && newCenter)
      setMapParams(newZoom, newCenter.lat, newCenter.lng);
  }, [setMapParams]);

  // memoize map controls
  const attributionControl = useMemo(() => createAttributionControl(), []);
  const navigationControl = useMemo(() => createNavigationControl(), []);
  const geolocationControl = useMemo(() => createGeolocateControl(), []);
  const fullscreenControl = useMemo(() => createFullscreenControl(), []);
  const geocoderApi = useMemo(() => createGeocoderApi(), []);

  useEffect(() => {
    mapRef.current = new maplibregl.Map({
      container: mapContainerRef.current || '',
      hash: false,
      style: mapTiles.MapMD_2D,
      center,
      zoom,
      maxZoom: 19,
      minZoom: 10,
      attributionControl: false,
    });

    // Update URL search parameters when map moves
    mapRef.current.on('moveend', handleMoveEnd);

    mapRef.current.addControl(attributionControl);

    mapRef.current.addControl(navigationControl, 'bottom-right');

    mapRef.current.addControl(geolocationControl, 'bottom-right');

    mapRef.current.addControl(fullscreenControl, 'bottom-right');

    mapRef.current.addControl(geocoderApi, 'top-left');

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
    </div>
  );
}

export default MapPage;
