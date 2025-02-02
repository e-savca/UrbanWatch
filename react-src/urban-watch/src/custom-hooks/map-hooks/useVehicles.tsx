import { useEffect } from 'react';
import { VehicleDTO } from '../../dto/TranzyDTOs';
import TransportUnitOfWork from '../../repositories/TransportRepositories/TransportUnitOfWork';
import convertVehiclesToGeoJSON from '../../utils/mapping/MapToGeoJson';

export default function useVehicles(
  mapRef: React.MutableRefObject<maplibregl.Map | null>,
  selectedTrip: { trip_id?: string } | null,
  transportUnitOfWork: TransportUnitOfWork
) {
  useEffect(() => {
    // Fetch vehicle data and convert it to GeoJSON
    const fetchVehicles = async () => {
      let vehicleData: VehicleDTO[] = [];
      if (selectedTrip?.trip_id) {
        const fetchVehicleData = await transportUnitOfWork.Vehicles.getByTripId(
          selectedTrip?.trip_id
        );
        if (fetchVehicleData) vehicleData = fetchVehicleData;
      }

      if (mapRef.current && vehicleData?.length > 0) {
        const geojson = convertVehiclesToGeoJSON(vehicleData);

        // Add GeoJSON source
        mapRef.current.addSource('vehicles', {
          type: 'geojson',
          data: geojson,
        });

        // Add a circle layer to display vehicle points
        mapRef.current.addLayer({
          id: 'vehicle-points',
          type: 'circle',
          source: 'vehicles',
          paint: {
            'circle-radius': 6,
            'circle-color': '#007cbf',
          },
        });
      }
    };

    fetchVehicles();
  }, [mapRef, selectedTrip?.trip_id, transportUnitOfWork.Vehicles]);
}
