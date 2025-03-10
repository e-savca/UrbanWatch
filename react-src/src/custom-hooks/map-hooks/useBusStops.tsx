import maplibregl from 'maplibre-gl';
import { useCallback, useEffect, useState } from 'react';
import TransportUnitOfWork from '../../repositories/TransportRepositories/TransportUnitOfWork';
import { StopDTO } from '../../dto/TranzyDTOs';
import { convertBusStopsToGeoJSON } from '../../utils/mapping/MapToGeoJson';
import {
  filterStationsByBounds,
  getNumberMultipliedByHundred,
  preprocessAndIndexStations,
} from './BusStopsUtils';

export default function useBusStops(
  mapRef: React.MutableRefObject<maplibregl.Map | null>,
  transportUnitOfWork: TransportUnitOfWork,
  setSelectedStop: (stop: StopDTO) => void
) {
  const [indexedStops, setIndexedStops] = useState<{
    [key: string]: StopDTO[];
  }>({});

  const [stops, setStops] = useState<StopDTO[]>([]);
  const [zoom, setZoom] = useState(0);
  const [bounds, setBounds] = useState<{
    bounds: {
      west: number;
      south: number;
      east: number;
      north: number;
    };
    truncatedBounds: {
      south: number;
      north: number;
      west: number;
      east: number;
    };
  } | null>(null);

  useEffect(() => {
    if (zoom < 14) setStops([]);
    else if (bounds) {
      const filteredStops = filterStationsByBounds(indexedStops, bounds);
      setStops(filteredStops);
    }
  }, [bounds, indexedStops, zoom]);

  useEffect(() => {
    const fetchStopsAndProcess = async () => {
      const fetchBusStopsData = await transportUnitOfWork.Stops.getAll();
      if (fetchBusStopsData?.length > 0) {
        const indexedStations = preprocessAndIndexStations(fetchBusStopsData);
        setIndexedStops(indexedStations);
      }
    };
    fetchStopsAndProcess();
  }, [transportUnitOfWork.Stops]);

  const getBounds = useCallback(() => {
    const map = mapRef.current;
    if (!map) return null;

    const boundsObj = map.getBounds();
    const exactBounds = {
      west: boundsObj.getWest(),
      south: boundsObj.getSouth(),
      east: boundsObj.getEast(),
      north: boundsObj.getNorth(),
    };
    const multipliedBounds = {
      south: getNumberMultipliedByHundred(exactBounds?.south),
      north: getNumberMultipliedByHundred(exactBounds?.north),
      west: getNumberMultipliedByHundred(exactBounds?.west),
      east: getNumberMultipliedByHundred(exactBounds?.east),
    };

    const truncatedBounds = {
      south: Math.floor(multipliedBounds.south),
      north: Math.floor(multipliedBounds.north),
      west: Math.floor(multipliedBounds.west),
      east: Math.floor(multipliedBounds.east),
    };

    return {
      bounds: exactBounds,
      truncatedBounds,
    };
  }, [mapRef]);

  const handleMoveEnd = useCallback(() => {
    const map = mapRef.current;
    if (!map) return;
    const newZoom = map.getZoom();
    const newBounds = getBounds();

    setZoom(newZoom);
    setBounds(newBounds);
  }, [getBounds, mapRef]);

  // subscribe events on map
  useEffect(() => {
    const map = mapRef.current;

    map?.on('moveend', handleMoveEnd);

    return () => {
      if (map) {
        map?.off('moveend', handleMoveEnd);
      }
    };
  }, [handleMoveEnd, mapRef]);

  const fetchBusStops = useCallback(async () => {
    const map = mapRef.current;
    if (map) {
      if (map.getLayer('bus-stop-points')) map.removeLayer('bus-stop-points');
      if (map.getSource('bus-stops')) map.removeSource('bus-stops');

      if (stops?.length > 0) {
        const geojson = convertBusStopsToGeoJSON(stops);
        map.addSource('bus-stops', {
          type: 'geojson',
          data: geojson,
        });

        map.addLayer({
          id: 'bus-stop-points',
          type: 'circle',
          source: 'bus-stops',
          paint: {
            'circle-radius': 8,
            'circle-color': '#1E90FF',
            'circle-opacity': 0.8,
            'circle-stroke-width': 2,
            'circle-stroke-color': '#ffffff',
          },
        });
      }
    }
  }, [mapRef, stops]);

  useEffect(() => {
    const fetchData = async () => {
      await fetchBusStops();
    };
    fetchData();
  }, [fetchBusStops]);

  useEffect(() => {
    if (!mapRef.current) return undefined;

    const map = mapRef.current;

    map.on('click', 'bus-stop-points', e => {
      if (e.features && e.features.length > 0) {
        const stop = e.features[0].properties as StopDTO;

        setSelectedStop(stop);
      }
    });

    map.on('mouseenter', 'bus-stop-points', () => {
      map.getCanvas().style.cursor = 'pointer';
    });

    map.on('mouseleave', 'bus-stop-points', () => {
      map.getCanvas().style.cursor = '';
    });

    // Cleanup listener
    return () => {
      if (map) {
        map.off('click', 'bus-stop-points', () => {});
      }
    };
  }, [mapRef, setSelectedStop]);
}
