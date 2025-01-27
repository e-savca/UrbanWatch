import maplibregl from 'maplibre-gl';
import { useSearchParams } from 'react-router';

type MapParams = {
  zoom: number;
  center: maplibregl.LngLat;
};
export const useMapHashParams = (): [
  MapParams,
  (zoom: number, lat: number, lng: number) => void,
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

  const updateMapParams = (newZoom: number, newLat: number, newLng: number) => {
    setSearchParams({
      zoom: newZoom.toFixed(2),
      lat: newLat.toFixed(6),
      lng: newLng.toFixed(6),
    });
  };

  return [{ zoom, center }, updateMapParams];
};

export default useMapHashParams;
