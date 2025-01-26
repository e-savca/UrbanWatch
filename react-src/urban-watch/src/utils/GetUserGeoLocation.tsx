import { LngLat } from 'maplibre-gl';
import { defaultCenterPositionOnMap } from '../data/AppData';

export async function GetUserGeoLocation() {
  if ('geolocation' in navigator) {
    return new Promise(resolve => {
      navigator.geolocation.getCurrentPosition(
        p => {
          resolve([p.coords.latitude, p.coords.longitude]);
        },
        () => {
          resolve(defaultCenterPositionOnMap);
        }
      );
    });
  }
  return Promise.resolve(defaultCenterPositionOnMap);
}

export async function GetUserPositionOnMap(): Promise<LngLat | undefined> {
  if ('geolocation' in navigator) {
    return new Promise(resolve => {
      navigator.geolocation.getCurrentPosition(p => {
        resolve(new LngLat(p.coords.longitude, p.coords.latitude));
      });
    });
  }
  return Promise.resolve(undefined);
}
