class TranzyUtils {
  static getTripIdBaseOnRouteIdAndDirection (
    routeId: number,
    tripDirection: number
  ): string {
    return `${routeId}_${tripDirection}`;
  }
}

export default TranzyUtils;
