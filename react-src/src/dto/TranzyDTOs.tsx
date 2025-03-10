export interface AgencyDTO {
  agency_id: number;
  agency_name: string;
  agency_timezone: string;
  agency_lang: string;
  agency_url: string;
  agency_urls: string[];
}

export interface VehicleDTO {
  id: string;
  label: string;
  latitude: number;
  longitude: number;
  timestamp: string;
  vehicle_type: number;
  bike_accessible: 'BIKE_ACCESSIBLE' | 'BIKE_INACCESSIBLE';
  wheelchair_accessible: 'ACCESSIBLE' | 'NOT_ACCESSIBLE' | 'NO_VALUE';
  speed: number;
  route_id: number;
  trip_id: string;
}

export enum RouteType {
  Tram = 0, // Tram, Streetcar, Light rail
  Subway = 1, // Subway, Metro
  Rail = 2, // Rail
  Bus = 3, // Bus
  Ferry = 4, // Ferry
  CableTram = 5, // Cable tram
  AerialLift = 6, // Aerial lift
  Funicular = 7, // Funicular
  Trolleybus = 11, // Trolleybus
  Monorail = 12, // Monorail
}

export interface RouteDTO {
  agency_id: string;
  route_id: number;
  route_short_name: string;
  route_long_name: string;
  route_color: string;
  route_type: RouteType;
  route_desc: string;
}

export interface TripDTO {
  direction_id: number;
  route_id: number;
  trip_id: string;
  trip_headsign: string;
  block_id: number;
  shape_id: number;
  wheelchair_accessible: number;
  bikes_allowed: number;
}

export interface ShapeDTO {
  shape_id: string;
  shape_pt_lat: number;
  shape_pt_lon: number;
  shape_pt_sequence: number;
  shape_dist_traveled?: number;
}

export interface StopDTO {
  stop_id: number;
  stop_name: string;
  stop_desc: string;
  stop_lat: number;
  stop_lon: number;
  location_type: number;
  stop_code: string | null;
}

export interface StopTimeDTO {
  trip_id: string;
  arrival_time: string;
  departure_time: string;
  stop_id: number;
  stop_sequence: number;
  stop_headsign: string;
  pickup_type: number;
  drop_off_type: number;
  shape_dist_traveled: number;
  timepoint: number;
}

export enum PickupDropOffType {
  REGULAR = 0,
  NONE = 1,
  CALL_AGENCY = 2,
  COORDINATE_WITH_DRIVER = 3,
}
