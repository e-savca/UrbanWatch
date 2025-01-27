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
      enableHighAccuracy: false,
      timeout: 10000,
      maximumAge: 30000,
    },
  });

export const createFullscreenControl = (): maplibregl.FullscreenControl =>
  new maplibregl.FullscreenControl();

interface GeocodeFeature {
  type: 'Feature';
  geometry: {
    type: 'Point';
    coordinates: [number, number];
  };
  place_name: string;
  properties: Record<string, any>;
  text: string;
  place_type: string[];
  center: [number, number];
}

interface ForwardGeocodeResult {
  features: GeocodeFeature[];
}

// Function to create geocoder API
const geocoderApi = (): GeocoderOptions['localGeocoder'] => ({
  forwardGeocode: async (config: {
    query: string;
  }): Promise<ForwardGeocodeResult> => {
    const features: GeocodeFeature[] = [];
    try {
      const requestUrl = `https://nominatim.openstreetmap.org/search?q=${encodeURIComponent(
        config.query
      )}&format=geojson&polygon_geojson=1&addressdetails=1`;

      const response = await fetch(requestUrl, {
        headers: {
          'User-Agent': 'YourAppName/1.0 (your-email@example.com)',
        },
      });

      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }

      const geojson = await response.json();

      for (const feature of geojson.features) {
        const [minLng, minLat, maxLng, maxLat] = feature.bbox;
        const center: [number, number] = [
          minLng + (maxLng - minLng) / 2,
          minLat + (maxLat - minLat) / 2,
        ];

        features.push({
          type: 'Feature',
          geometry: {
            type: 'Point',
            coordinates: center,
          },
          place_name: feature.properties.display_name,
          properties: feature.properties,
          text: feature.properties.display_name,
          place_type: ['place'],
          center,
        });
      }
    } catch (e) {
      console.error(`Failed to forwardGeocode. Error: ${(e as Error).message}`);
    }

    return { features };
  },
});

// Function to create and return the geocoder instance
export const createGeocoderApi = () =>
  new MaplibreGeocoder(geocoderApi(), {
    maplibregl,
    placeholder: 'Search for a location...',
    limit: 5, // Limit results to avoid flooding responses
  });
