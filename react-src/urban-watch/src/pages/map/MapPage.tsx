import maplibregl from 'maplibre-gl';
import 'maplibre-gl/dist/maplibre-gl.css';
import { useRef, useEffect } from 'react';
import { mapTiles } from '../../data/AppData';
import useMapHashParams from '../../custom-hooks/map-hooks/useMapHashParams';

function MapPage(): JSX.Element {
  const mapRef = useRef<maplibregl.Map | null>(null);
  const mapContainerRef = useRef<HTMLDivElement | null>(null);
  const [{ zoom, center }, setMapParams] = useMapHashParams();

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
    mapRef.current.on('moveend', () => {
      const newCenter = mapRef.current?.getCenter();
      const newZoom = mapRef.current?.getZoom();
      if (newZoom && newCenter)
        setMapParams(newZoom, newCenter.lat, newCenter.lng);
    });

    mapRef.current.addControl(
      new maplibregl.AttributionControl({
        customAttribution: '&copy; <a href="/" target="_blank">UrbanWatch</a>',
        compact: true,
      })
    );

    mapRef.current.addControl(
      new maplibregl.NavigationControl({ visualizePitch: true }),
      'bottom-right'
    );

    mapRef.current.addControl(
      new maplibregl.GeolocateControl({
        positionOptions: {
          enableHighAccuracy: false,
          timeout: 10000,
          maximumAge: 30000,
        },
      }),
      'bottom-right'
    );

    mapRef.current.addControl(
      new maplibregl.FullscreenControl(),
      'bottom-right'
    );

    return () => {
      if (mapRef.current) mapRef.current.remove();
    };
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
