import { StopDTO, VehicleDTO } from '../../dto/TranzyDTOs';

export const convertVehiclesToGeoJSON = (
  vehicles: VehicleDTO[]
): GeoJSON.FeatureCollection => ({
  type: 'FeatureCollection',
  features: vehicles.map(vehicle => ({
    type: 'Feature',
    geometry: {
      type: 'Point',
      coordinates: [vehicle.longitude, vehicle.latitude],
    },
    properties: {
      id: vehicle.id,
      label: vehicle.label,
      timestamp: vehicle.timestamp,
      vehicle_type: vehicle.vehicle_type,
      bike_accessible: vehicle.bike_accessible,
      wheelchair_accessible: vehicle.wheelchair_accessible,
      speed: vehicle.speed,
      route_id: vehicle.route_id,
      trip_id: vehicle.trip_id,
    },
  })),
});

export const convertBusStopsToGeoJSON = (
  stops: StopDTO[]
): GeoJSON.FeatureCollection => ({
  type: 'FeatureCollection',
  features: stops.map(stop => ({
    type: 'Feature',
    geometry: {
      type: 'Point',
      coordinates: [stop.stop_lon, stop.stop_lat],
    },
    properties: {
      stop_id: stop.stop_id,
      stop_name: stop.stop_name,
      stop_desc: stop.stop_desc,
      stop_code: stop.stop_code,
      location_type: stop.location_type,
    },
  })),
});
