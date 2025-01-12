import L from 'leaflet'
import busIcon from '../../assets/leaflet-icons/bus.png'

const BusIcon = L.icon({
  iconUrl: busIcon,
  iconSize: [38, 38], // Dimension of the icon
  iconAnchor: [19, 38], // Anchor point of the icon (center bottom)
})

export default BusIcon
