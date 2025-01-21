import L from 'leaflet'
import busStopIcon from '../../..//assets/leaflet-icons/bus.png'

const BusStopIcon = L.icon({
  iconUrl: busStopIcon,
  iconSize: [32, 32],
  iconAnchor: [24, 32],
  popupAnchor: [-8, -30],
})

export default BusStopIcon
