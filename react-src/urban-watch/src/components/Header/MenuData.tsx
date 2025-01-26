import {
  MapPinIcon,
  ArrowsRightLeftIcon,
  MapIcon,
} from '@heroicons/react/24/outline';

export const MapTypes = [
  {
    name: 'Bus Stop Explorer Leaflet',
    description:
      'Easily locate a bus stop and discover all the live routes and buses passing through it in real-time.',
    href: '/map/routes/leaflet',
    icon: MapPinIcon,
  },
  {
    name: 'Bus Stop Explorer MapLibre',
    description:
      'Easily locate a bus stop and discover all the live routes and buses passing through it in real-time.',
    href: '/map/routes/maplibre',
    icon: MapPinIcon,
  },
  {
    name: 'Route Tracker',
    description:
      'Select a specific route to see detailed information about all stops and track buses live along the way.',
    href: '/map',
    icon: MapIcon,
  },
  {
    name: 'Route Navigator',
    description:
      'Plan your trip by choosing starting and ending points. Discover intersecting routes and optimize your journey with ease.',
    href: '/map',
    icon: ArrowsRightLeftIcon,
  },
];
export const MenuItems = [
  {
    id: 0,
    name: 'About Us',
    href: '#',
  },
  {
    id: 1,
    name: 'Contact',
    href: '#',
  },
];
