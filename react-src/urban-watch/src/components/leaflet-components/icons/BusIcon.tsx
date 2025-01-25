import L from 'leaflet';

const BusStopIcon = L.divIcon({
  className: 'bus-stop-icon',
  html: `
    <div class="flex items-center justify-center w-12 h-12 bg-blue-600 text-white rounded-full shadow-lg border-2 border-white">
      <span class="text-2xl">🚌</span>
    </div>
  `,
  iconSize: [24, 24],
  iconAnchor: [12, 12],
  popupAnchor: [7, -20],
});

export default BusStopIcon;
