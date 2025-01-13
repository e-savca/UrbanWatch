import L from 'leaflet'
import userIcon from '../../assets/leaflet-icons/user-location.svg'

const UserIcon = L.icon({
  iconUrl: userIcon,
  iconSize: [38, 38],
  iconAnchor: [19, 38],
})

export default UserIcon
