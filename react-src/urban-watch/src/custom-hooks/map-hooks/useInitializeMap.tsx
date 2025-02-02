import maplibregl from 'maplibre-gl';
import { useEffect, useMemo, useRef } from 'react';

import { mapTiles } from '../../data/AppData';
import {
  createAttributionControl,
  createFullscreenControl,
  // createGeocoderApi,
  createGeolocateControl,
  createNavigationControl,
} from '../../utils/map-utils/Controls';

export default function useInitializeMap(
  mapContainerRef: React.RefObject<HTMLDivElement>,
  zoom: number,
  center: maplibregl.LngLat
) {
  const mapRef = useRef<maplibregl.Map | null>(null);
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

  return mapRef;
}
