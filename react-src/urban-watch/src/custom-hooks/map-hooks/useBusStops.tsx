import { useCallback, useEffect } from 'react';
import TransportUnitOfWork from '../../repositories/TransportRepositories/TransportUnitOfWork';
import { StopDTO } from '../../dto/TranzyDTOs';
import { convertBusStopsToGeoJSON } from '../../utils/mapping/MapToGeoJson';

export default function useBusStops(
  mapRef: React.MutableRefObject<maplibregl.Map | null>,
  transportUnitOfWork: TransportUnitOfWork
) {
  const fetchBusStops = useCallback(async () => {
    let busStopsData: StopDTO[] = [];
    const fetchBusStopsData = await transportUnitOfWork.Stops.getAll();
    if (fetchBusStopsData?.length > 0) busStopsData = fetchBusStopsData;

    const map = mapRef.current;
    if (map) {
      if (map.getLayer('bus-stop-points')) map.removeLayer('bus-stop-points');
      if (map.getSource('bus-stops')) map.removeSource('bus-stops');

      if (busStopsData?.length > 0) {
        const geojson = convertBusStopsToGeoJSON(busStopsData);
        map.addSource('bus-stops', {
          type: 'geojson',
          data: geojson,
        });

        map.addLayer({
          id: 'bus-stop-points',
          type: 'circle',
          source: 'bus-stops',
          paint: {
            'circle-radius': 6,
            'circle-color': '#1E90FF',
            'circle-opacity': 0.8,
            'circle-stroke-width': 2,
            'circle-stroke-color': '#ffffff',
          },
        });
      }
    }
  }, [mapRef, transportUnitOfWork.Stops]);

  useEffect(() => {
    const fetchData = async () => {
      await fetchBusStops();
    };
    fetchData();
  }, [fetchBusStops]);
}
