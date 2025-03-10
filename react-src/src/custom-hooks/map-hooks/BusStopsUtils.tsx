import { StopDTO } from '../../dto/TranzyDTOs';

export const getNumberMultipliedByHundred = (num: number) => num * 100;

export const getKeyBasedOnLatAndLon = (lon: number, lat: number) =>
  `${lon}_${lat}`;

export function preprocessAndIndexStations(stations: StopDTO[]) {
  const index: { [key: string]: StopDTO[] } = {};

  stations.forEach(station => {
    const lonShort = Math.floor(getNumberMultipliedByHundred(station.stop_lon));
    const latShort = Math.floor(getNumberMultipliedByHundred(station.stop_lat));
    const key = getKeyBasedOnLatAndLon(lonShort, latShort);
    if (!index[key]) {
      index[key] = [];
    }

    index[key].push(station);
  });

  return index;
}
interface MapBounds {
  south: number;
  north: number;
  west: number;
  east: number;
}

interface BoundsInput {
  truncatedBounds: MapBounds;
  bounds: MapBounds;
}

export function filterStationsByBounds(
  index: { [key: string]: StopDTO[] },
  bounds: BoundsInput
): StopDTO[] {
  if (!index || !bounds) {
    return [];
  }

  const { south, north, west, east } = bounds.truncatedBounds;
  const visibleStations: StopDTO[] = [];
  if (south > north || west > east) {
    return [];
  }

  for (let lon = west; lon <= east; lon += 1) {
    for (let lat = south; lat <= north; lat += 1) {
      const key = getKeyBasedOnLatAndLon(lon, lat);
      const stations = index[key];

      if (stations) {
        const filteredStations = stations.filter(
          station =>
            station.stop_lon >= bounds.bounds.west &&
            station.stop_lon <= bounds.bounds.east &&
            station.stop_lat >= bounds.bounds.south &&
            station.stop_lat <= bounds.bounds.north
        );

        visibleStations.push(...filteredStations);
      }
    }
  }
  return visibleStations;
}
