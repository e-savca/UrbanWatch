import { useCallback, useEffect } from 'react';
import { ShapeDTO, VehicleDTO } from '../../dto/TranzyDTOs';
import TransportUnitOfWork from '../../repositories/TransportRepositories/TransportUnitOfWork';
import convertVehiclesToGeoJSON from '../../utils/mapping/MapToGeoJson';

export default function useVehicles(
  mapRef: React.MutableRefObject<maplibregl.Map | null>,
  selectedTrip: { trip_id?: string } | null,
  transportUnitOfWork: TransportUnitOfWork
) {
  const fetchVehicles = useCallback(async () => {
    const map = mapRef.current;
    let vehicleData: VehicleDTO[] = [];
    if (selectedTrip?.trip_id) {
      const fetchVehicleData = await transportUnitOfWork.Vehicles.getByTripId(
        selectedTrip?.trip_id
      );
      if (fetchVehicleData) vehicleData = fetchVehicleData;
    }

    if (map) {
      if (map.getLayer('vehicle-points')) {
        map.removeLayer('vehicle-points');
      }
      if (map.getSource('vehicles')) {
        map.removeSource('vehicles');
      }

      if (vehicleData?.length > 0) {
        const geojson = convertVehiclesToGeoJSON(vehicleData);

        map.addSource('vehicles', {
          type: 'geojson',
          data: geojson,
        });

        map.addLayer({
          id: 'vehicle-points',
          type: 'circle',
          source: 'vehicles',
          paint: {
            'circle-radius': 6,
            'circle-color': '#007cbf',
          },
        });
      }
    }
  }, [mapRef, selectedTrip?.trip_id, transportUnitOfWork.Vehicles]);

  const fetchTripLines = useCallback(async () => {
    const map = mapRef.current;
    let tripLine: ShapeDTO[] = [];
    if (selectedTrip?.trip_id) {
      const fetchedTripLine = await transportUnitOfWork.Shapes.getByTripId(
        selectedTrip?.trip_id
      );

      if (fetchedTripLine) tripLine = fetchedTripLine;
    }
    if (map) {
      if (map.getLayer('route-line')) map.removeLayer('route-line');
      if (map.getSource('route')) map.removeSource('route');

      if (tripLine?.length > 0) {
        const routeGeoJSON = {
          type: 'Feature',
          geometry: {
            type: 'LineString',
            coordinates: tripLine.map(point => [
              point.shape_pt_lon,
              point.shape_pt_lat,
            ]),
          },
        };

        map.addSource('route', { type: 'geojson', data: routeGeoJSON });

        let tripLineColor: string = '#ff0000';

        if (selectedTrip?.trip_id) {
          const currentTripObj = await transportUnitOfWork.Trips.getById(
            selectedTrip?.trip_id
          );
          if (currentTripObj) {
            const routeObj = await transportUnitOfWork.Routes.getById(
              currentTripObj.route_id
            );
            tripLineColor = routeObj?.route_color ?? tripLineColor;
          }
        }
        map.addLayer({
          id: 'route-line',
          type: 'line',
          source: 'route',
          layout: {
            'line-join': 'round',
            'line-cap': 'round',
          },
          paint: {
            'line-color': tripLineColor,
            'line-width': 3,
          },
        });
      }
    }
  }, [
    mapRef,
    selectedTrip?.trip_id,
    transportUnitOfWork.Routes,
    transportUnitOfWork.Shapes,
    transportUnitOfWork.Trips,
  ]);

  useEffect(() => {
    fetchVehicles();
    fetchTripLines();
  }, [fetchTripLines, fetchVehicles]);
}
