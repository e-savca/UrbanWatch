import { useMap } from 'react-leaflet'
import PropTypes from 'prop-types'
import { useEffect, useState } from 'react'

MapTools.propTypes = {
  dispatch: PropTypes.func,
}
function MapTools({ dispatch }) {
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
      //   console.log(`type ${typeof center} value ${center}`)

      dispatch({ type: 'SET_CENTER', payload: center })
    }

    handleMapCenter()
  }, [center, dispatch])

  return <></>
}

export default MapTools
