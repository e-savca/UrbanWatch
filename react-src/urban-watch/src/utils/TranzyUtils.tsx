class TranzyUtils {
  static getTripIdBaseOnRouteIdAndDirection(
    routeId: unknown,
    tripDirection: unknown
  ) {
    return `${routeId}_${tripDirection}`;
  }
}

export default TranzyUtils;
