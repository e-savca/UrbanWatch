import { useMap } from 'react-leaflet'
import PropTypes from 'prop-types'
import { useEffect, useState } from 'react'

MapTools.propTypes = {
  dispatchHelper: PropTypes.object,
}
function MapTools({ dispatchHelper }) {
  const map = useMap()
  const [center, setCenter] = useState(() => {
    const gps = map.getCenter()
    return [gps.lat, gps.lng]
  })

  useEffect(() => {
    const handleMoveend = () => {
      const c = map.getCenter()
      setCenter([c.lat, c.lng])
    }
    map.on('moveend', handleMoveend)

    return () => {
      map.off('moveend', handleMoveend)
    }
  }, [map])

  useEffect(() => {
    function handleMapCenter() {
      dispatchHelper.setCenter(center)
    }

    handleMapCenter()
  }, [center, dispatchHelper])

  return <></>
}

export default MapTools
