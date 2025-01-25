import L from 'leaflet';
import userIcon from '../../../assets/leaflet-icons/current-location.svg';

const UserIcon = L.icon({
  iconUrl: userIcon,
  iconSize: [38, 38],
  iconAnchor: [19, 19],
});

export default UserIcon;
