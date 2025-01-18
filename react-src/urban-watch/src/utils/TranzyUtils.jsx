class TranzyUtils {
  getTripIdBaseOnRouteIdAndDirection(routeId, tripDirection) {
    return `${routeId}_${tripDirection}`
  }
}

export default TranzyUtils
