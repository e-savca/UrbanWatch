import PropTypes from 'prop-types'

MapLayout.propTypes = {
  children: PropTypes.element,
}
function MapLayout({ children }) {
  return <>{children}</>
}

export default MapLayout
