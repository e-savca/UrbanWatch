import { useCallback, useEffect, useState } from 'react';
import { RouteDTO, TripDTO } from '../../dto/TranzyDTOs';
import TransportUnitOfWork from '../../repositories/TransportRepositories/TransportUnitOfWork';

const useRoutesAndTrips = (transportUnitOfWork: TransportUnitOfWork) => {
  const [routes, setRoutes] = useState<RouteDTO[]>([]);
  const [selectedRoute, setSelectedRoute] = useState<number>(0);
  const [tripsOnSelectedRoute, setTripsOnSelectedRoute] = useState<
    TripDTO[] | null
  >([]);
  const [tripDirection, setTripDirection] = useState<number>(0);
  const [selectedTrip, setSelectedTrip] = useState<TripDTO | null>(null);

  // Fetch routes on mount
  useEffect(() => {
    const fetchRoutes = async () => {
      const fetchedRoutes = await transportUnitOfWork.Routes.getAll();
      setRoutes(fetchedRoutes);
    };

    fetchRoutes();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  // Fetch trips on setSelectedRoute
  useEffect(() => {
    async function UpdateTripsOnSelectedRoute() {
      const trips = await transportUnitOfWork.Trips.getByRouteId(selectedRoute);
      setTripsOnSelectedRoute(trips || []);
    }
    UpdateTripsOnSelectedRoute();
  }, [selectedRoute, transportUnitOfWork.Trips]);

  useEffect(() => {
    const getCurrentTrip = tripsOnSelectedRoute?.find(
      trip => trip.direction_id === tripDirection
    );

    setSelectedTrip(getCurrentTrip || null);
  }, [tripDirection, tripsOnSelectedRoute]);

  const handleRouteChange = useCallback((e: { target: { value: string } }) => {
    const routeId = Number(e.target.value);
    setSelectedRoute(routeId);
  }, []);

  return {
    routes,
    selectedRoute,
    tripsOnSelectedRoute,
    tripDirection,
    setTripDirection,
    selectedTrip,
    handleRouteChange,
  };
};

export default useRoutesAndTrips;
