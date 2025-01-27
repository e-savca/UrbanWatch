import { LngLat, StyleSpecification } from 'maplibre-gl';

const lng = 28.832034417468275;
const lat = 47.024371640335254;

export const defaultCenterPositionOnMap = [lat, lng];

export const defaultCenterPositionOnMapLngLat = new LngLat(lng, lat);

export const mapTiles: { [key: string]: string | StyleSpecification } = {
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
        minzoom: 1,
        maxzoom: 19,
      },
    ],
  } as StyleSpecification, // Correcting the syntax
};
