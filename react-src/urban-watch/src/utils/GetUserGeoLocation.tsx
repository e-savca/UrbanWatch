import { defaultCenterPositionOnMap } from '../data/AppData';

export async function GetUserGeoLocation() {
  if ('geolocation' in navigator) {
    return new Promise((resolve) => {
      navigator.geolocation.getCurrentPosition(
        (p) => {
          resolve([p.coords.latitude, p.coords.longitude]);
        },
        () => {
          resolve(defaultCenterPositionOnMap);
        },
      );
    });
  }
  return Promise.resolve(defaultCenterPositionOnMap);
}

export async function GetUserPositionOnMap() {
  if ('geolocation' in navigator) {
    return new Promise((resolve) => {
      navigator.geolocation.getCurrentPosition((p) => {
        resolve([p.coords.longitude, p.coords.latitude]);
      });
    });
  }
}
