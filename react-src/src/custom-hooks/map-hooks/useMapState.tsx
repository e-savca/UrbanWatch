import { useSearchParams } from 'react-router';
import { useCallback, useEffect, useState } from 'react';
import { RouteDTO, TripDTO } from '../../dto/TranzyDTOs';
import TransportUnitOfWork from '../../repositories/TransportRepositories/TransportUnitOfWork';

const useMapState = (transportUnitOfWork: TransportUnitOfWork) => {
  const [searchParams, setSearchParams] = useSearchParams();

  const routeParam = Number(searchParams.get('route') ?? '0');
  const directionParam = Number(searchParams.get('direction') ?? '0');

  const [routes, setRoutes] = useState<RouteDTO[]>([]);
  const [selectedRoute, setSelectedRoute] = useState<number>(routeParam);
  const [tripsOnSelectedRoute, setTripsOnSelectedRoute] = useState<
    TripDTO[] | null
  >([]);
  const [tripDirection, setTripDirection] = useState<number>(directionParam);
  const [selectedTrip, setSelectedTrip] = useState<TripDTO | null>(null);

  // Update URL params on selectedRoute & tripDirection change
  useEffect(() => {
    const newParams = new URLSearchParams(searchParams);
    newParams.set('route', selectedRoute.toString());
    newParams.set('direction', tripDirection.toString());
    setSearchParams(newParams);
  }, [selectedRoute, tripDirection, setSearchParams, searchParams]);

  // Fetch routes on mount
  useEffect(() => {
    const fetchRoutes = async () => {
      const fetchedRoutes = await transportUnitOfWork.Routes.getAll();
      setRoutes(fetchedRoutes);
    };

    fetchRoutes();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  // Fetch trips on selectedRoute change
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
    setTripDirection(0);
  }, []);

  const handleRouteAndDirectionChange = useCallback(
    (routeId: number, directionId: number) => {
      setSelectedRoute(routeId);
      setTripDirection(directionId);
    },
    []
  );

  return {
    routes,
    selectedRoute,
    tripsOnSelectedRoute,
    tripDirection,
    setTripDirection,
    selectedTrip,
    handleRouteChange,
    handleRouteAndDirectionChange,
  };
};

export default useMapState;
