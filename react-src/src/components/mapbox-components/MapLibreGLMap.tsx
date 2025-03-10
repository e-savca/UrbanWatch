import maplibregl from 'maplibre-gl';
import 'maplibre-gl/dist/maplibre-gl.css';
import { useRef, useEffect } from 'react';
import { defaultCenterPositionOnMapLngLat } from '../../data/AppData';

const tileStyles = {
  MapMD_2D:
    'https://map.md/api/tiles/styles/map/style.json?v=2018-12-28T00:00:00.000Z',
  MapMD_3D:
    'https://map.md/api/tiles/styles/satelite/style.json?v=2018-12-28T00:00:00.000Z',
  CARTO_Voyager: {
    version: 8,
    sources: {
      osm: {
        type: 'raster',
        tiles: [
          'https://basemaps.cartocdn.com/rastertiles/voyager/{z}/{x}/{y}{r}.png',
        ],
        attribution:
          '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors &copy; <a href="https://carto.com/attributions">CARTO</a>',
        tileSize: 256,
      },
    },
    layers: [
      {
        id: 'osm-tiles',
        type: 'raster',
        source: 'osm',
        minzoom: 0,
        maxzoom: 19,
      },
    ],
  },
};

function MapLibreGLMap(): JSX.Element {
  const mapRef = useRef<maplibregl.Map | null>(null);
  const mapContainerRef = useRef<HTMLDivElement | null>(null);

  useEffect(() => {
    mapRef.current = new maplibregl.Map({
      container: mapContainerRef.current || '',
      style: tileStyles.MapMD_2D,
      center: defaultCenterPositionOnMapLngLat,
      zoom: 14,
      maxZoom: 19,
      minZoom: 10,
      attributionControl: false,
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

export default MapLibreGLMap;
