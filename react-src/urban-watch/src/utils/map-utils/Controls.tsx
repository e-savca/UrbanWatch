import maplibregl from 'maplibre-gl';
import MaplibreGeocoder, {
  GeocoderOptions,
} from '@maplibre/maplibre-gl-geocoder';

export const createAttributionControl = (): maplibregl.AttributionControl =>
  new maplibregl.AttributionControl({
    customAttribution: '&copy; <a href="/" target="_blank">UrbanWatch</a>',
    compact: true,
  });

export const createNavigationControl = (): maplibregl.NavigationControl =>
  new maplibregl.NavigationControl({ visualizePitch: true });

export const createGeolocateControl = (): maplibregl.GeolocateControl =>
  new maplibregl.GeolocateControl({
    positionOptions: {
      enableHighAccuracy: true,
      timeout: 10000,
      maximumAge: 30000,
    },
  });

export const createFullscreenControl = (): maplibregl.FullscreenControl =>
  new maplibregl.FullscreenControl();

// interface GeocodeFeature {
//   type: 'Feature';
//   geometry: {
//     type: 'Point';
//     coordinates: [number, number];
//   };
//   place_name: string;
//   properties: Record<string, any>;
//   text: string;
//   place_type: string[];
//   center: [number, number];
// }

// interface ForwardGeocodeResult {
//   features: GeocodeFeature[];
// }

// const geocoderApi = (): GeocoderOptions['localGeocoder'] => ({
//   forwardGeocode: async (config: {
//     query: string;
//   }): Promise<ForwardGeocodeResult> => {
//     const features: GeocodeFeature[] = [];
//     try {
//       const apiKey = import.meta.env.VITE_MAP_MD_API_KEY;
//       const location = 'Chisinau'; // hard-coded for the moment. As there isn't implemented the logic which parse the city
//       const requestUrl = `https://map.md/api/companies/webmap/search_street?location=${encodeURIComponent(
//         location
//       )}&q=${encodeURIComponent(config.query)}`;

//       const response = await fetch(requestUrl, {
//         headers: {
//           Authorization: `Basic ${btoa(`${apiKey}:`)}`,
//           'Content-Type': 'application/json',
//         },
//       });

//       if (!response.ok) {
//         throw new Error(`HTTP error! Status: ${response.status}`);
//       }

//       const data = await response.json();

//       for (const item of data) {
//         const center: [number, number] = [item.centroid.lon, item.centroid.lat];

//         features.push({
//           type: 'Feature',
//           geometry: {
//             type: 'Point',
//             coordinates: center,
//           },
//           place_name: item.name,
//           properties: item,
//           text: item.name,
//           place_type: ['street'],
//           center,
//         });
//       }
//     } catch (e) {
//       console.error(`Failed to forwardGeocode. Error: ${(e as Error).message}`);
//     }

//     return { features };
//   },
// });

// // Function to create and return the geocoder instance
// export const createGeocoderApi = () =>
//   new MaplibreGeocoder(geocoderApi(), {
//     maplibregl,
//     placeholder: 'Search for a street...',
//     limit: 5,
//   });
