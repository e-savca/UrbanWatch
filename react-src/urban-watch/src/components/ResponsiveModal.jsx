import PropTypes from 'prop-types'

ResponsiveModal.propTypes = {
  isOpen: PropTypes.bool,
  onClose: PropTypes.func,
  station: PropTypes.object,
  afiliateRoutes: PropTypes.array,
}
function ResponsiveModal({ isOpen, onClose, station, afiliateRoutes }) {
  console.log(afiliateRoutes)
  return (
    <>
      {isOpen && (
        <div className="fixed bottom-0 left-0 right-0 md:inset-0 z-50 flex items-center justify-center pointer-events-none">
          <div
            className="relative bg-white rounded-lg shadow-lg p-6 
                        w-full max-w-md md:max-w-lg lg:max-w-xl
                        sm:w-11/12 sm:mx-4 md:w-auto pointer-events-auto"
          >
            <button
              className="absolute top-2 right-2 text-gray-700 text-lg font-bold"
              onClick={onClose}
            >
              &times;
            </button>
            <h2 className="text-xl font-bold mb-4">{station.stop_name}</h2>
            {afiliateRoutes.map((route) => (
              <p key={route.id} className="text-gray-600">
                <h4>Bus Number {route.route_id}</h4>
              </p>
            ))}
          </div>
        </div>
      )}
    </>
  )
}

export default ResponsiveModal
