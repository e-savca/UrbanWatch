class TranzyUtils {
  getTripIdBaseOnRouteIdAndDirection(routeId: any, tripDirection: any) {
    return `${routeId}_${tripDirection}`;
  }
}

export default TranzyUtils;
