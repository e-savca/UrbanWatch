import maplibregl from 'maplibre-gl';
import { useCallback } from 'react';
import { useSearchParams } from 'react-router';

export const useMapHashParams = (): [
  zoom: number,
  center: maplibregl.LngLat,
  (map: maplibregl.Map | null) => void,
] => {
  const [searchParams, setSearchParams] = useSearchParams();

  const defaultZoom = 14;
  const defaultLng = 28.832034417468275;
  const defaultLat = 47.024371640335254;

  const zoomParam = searchParams.get('zoom');
  const latParam = searchParams.get('lat');
  const lngParam = searchParams.get('lng');

  const zoom = zoomParam ? parseFloat(zoomParam) : defaultZoom;
  const lat = latParam ? parseFloat(latParam) : defaultLat;
  const lng = lngParam ? parseFloat(lngParam) : defaultLng;

  const center = new maplibregl.LngLat(lng, lat);

  const updateParams = useCallback(
    (newZoom: number, newCenter: { lat: number; lng: number }) => {
      const newParams = new URLSearchParams(searchParams);
      newParams.set('zoom', newZoom.toFixed(2));
      newParams.set('lat', newCenter.lat.toFixed(6));
      newParams.set('lng', newCenter.lng.toFixed(6));
      setSearchParams(newParams);
    },
    [searchParams, setSearchParams]
  );

  const onMoveEnd = useCallback(
    (map: maplibregl.Map | null) => {
      if (!map) return;
      const newCenter = map?.getCenter();
      const newZoom = map?.getZoom();

      if (newZoom && newCenter) updateParams(newZoom, newCenter);
    },
    [updateParams]
  );

  return [zoom, center, onMoveEnd];
};

export default useMapHashParams;
