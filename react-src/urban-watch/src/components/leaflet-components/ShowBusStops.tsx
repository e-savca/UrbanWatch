// import 'leaflet/dist/leaflet.css'; // IMPORTANT for map to work properly
// import { useEffect, useRef, useState } from 'react';
// import { Marker, useMap } from 'react-leaflet';
// import PropTypes from 'prop-types';
// import BusStopIcon from './icons/BusStopIcon';

// const getNumberMultiplyiedByHundred = num => Number(num) * 100;
// const getKeyBasedOnLatAndLon = (lat, lon) => `${lat}_${lon}`;

// const preprocessAndIndexStations = stations => {
//   const index = {};

//   stations.forEach(station => {
//     const latShort = Math.floor(
//       getNumberMultiplyiedByHundred(station.stop_lat)
//     );
//     const lonShort = Math.floor(
//       getNumberMultiplyiedByHundred(station.stop_lon)
//     );
//     const key = getKeyBasedOnLatAndLon(latShort, lonShort);
//     if (!index[key]) {
//       index[key] = [];
//     }

//     index[key].push(station);
//   });

//   return index;
// };

// const getTruncatedBounds = bounds => {
//   const southWest = bounds.getSouthWest();
//   const northEast = bounds.getNorthEast();

//   return {
//     latMin: Math.floor(getNumberMultiplyiedByHundred(southWest.lat)),
//     latMax: Math.floor(getNumberMultiplyiedByHundred(northEast.lat)),
//     lonMin: Math.floor(getNumberMultiplyiedByHundred(southWest.lng)),
//     lonMax: Math.floor(getNumberMultiplyiedByHundred(northEast.lng)),
//   };
// };

// const filterStations = (index, truncatedBounds, exactBounds) => {
//   const { latMin, latMax, lonMin, lonMax } = truncatedBounds;
//   const visibleStations = [];

//   for (let lat = latMin; lat <= latMax; lat++) {
//     for (let lon = lonMin; lon <= lonMax; lon++) {
//       const key = `${lat}_${lon}`;
//       if (index[key]) {
//         index[key].forEach(station => {
//           if (
//             station.stop_lat >= exactBounds.getSouthWest().lat &&
//             station.stop_lat <= exactBounds.getNorthEast().lat &&
//             station.stop_lon >= exactBounds.getSouthWest().lng &&
//             station.stop_lon <= exactBounds.getNorthEast().lng
//           ) {
//             visibleStations.push(station);
//           }
//         });
//       }
//     }
//   }

//   return visibleStations;
// };

// ShowBusStops.propTypes = {
//   busStops: PropTypes.array,
//   onBusStopClick: PropTypes.func,
// };
// function ShowBusStops({ busStops, onBusStopClick }) {
//   const map = useMap();
//   const [zoom, setZoom] = useState(map.getZoom());
//   const [corners, setCorners] = useState(null);
//   const [stations, setStations] = useState([]);
//   const index = useRef(preprocessAndIndexStations(busStops));

//   useEffect(() => {
//     if (zoom < 16) {
//       setStations([]);
//     } else {
//       const bounds = map.getBounds();
//       const truncatedBounds = getTruncatedBounds(bounds);

//       const filtered = filterStations(index.current, truncatedBounds, bounds);
//       setStations(filtered);
//     }
//   }, [corners, zoom, map]);

//   useEffect(() => {
//     const updateCorners = () => {
//       setZoom(map.getZoom());
//       // console.log('zoom ->' + map.getZoom())
//       const bounds = map.getBounds();
//       const ne = bounds.getNorthEast();
//       const sw = bounds.getSouthWest();

//       setCorners({
//         northEast: ne,
//         southWest: sw,
//       });
//     };

//     map.on('moveend', updateCorners);

//     return () => {
//       map.off('moveend', updateCorners);
//     };
//   }, [map]);

//   return (
//     <>
//       {stations.map(station => (
//         <Marker
//           key={station.stop_id}
//           position={[station.stop_lat, station.stop_lon]}
//           icon={BusStopIcon}
//           eventHandlers={{
//             click: () => {
//               onBusStopClick(station);
//             },
//           }}
//         />
//       ))}
//     </>
//   );
// }

// export default ShowBusStops;
