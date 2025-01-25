import currentLocation from '../../../assets/leaflet-icons/current-location.svg';

function UserPositionMarker() {
  return (
    <div style={{ width: '40px', height: '40px' }}>
      <img
        src={currentLocation}
        alt="Current Location"
        width="100%"
        height="100%"
      />
    </div>
  );
}

export default UserPositionMarker;
