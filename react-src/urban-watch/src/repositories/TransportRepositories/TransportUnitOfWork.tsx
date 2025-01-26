import RouteRepository from './RouteRepository';
import VehicleRepository from './VehicleRepository';
import TripRepository from './TripRepository';
import ShapeRepository from './ShapeRepository';
import StopsRepository from './StopsRepository';
import StopTimesRepository from './StopTimesRepository';

export default class TransportUnitOfWork {
  //   NOT IMPLEMENTED
  //   public Agency: AgencyRepository;

  public Vehicles: VehicleRepository;

  public Routes: RouteRepository;

  public Trips: TripRepository;

  public Shapes: ShapeRepository;

  public Stops: StopsRepository;

  public StopTimes: StopTimesRepository;

  private constructor(
    vehicles: VehicleRepository,
    routes: RouteRepository,
    trips: TripRepository,
    shapes: ShapeRepository,
    stops: StopsRepository,
    stopTimes: StopTimesRepository
  ) {
    this.Vehicles = vehicles;
    this.Routes = routes;
    this.Trips = trips;
    this.Shapes = shapes;
    this.Stops = stops;
    this.StopTimes = stopTimes;
  }

  public static async create(): Promise<TransportUnitOfWork> {
    const vehicles = new VehicleRepository();
    const routes = new RouteRepository();
    const trips = new TripRepository();
    const shapes = new ShapeRepository();
    const stops = new StopsRepository();
    const stopTimes = new StopTimesRepository();

    await Promise.all([
      vehicles.Initialize(),
      routes.Initialize(),
      trips.Initialize(),
      shapes.Initialize(),
      stops.Initialize(),
      stopTimes.Initialize(),
    ]);
    return new TransportUnitOfWork(
      vehicles,
      routes,
      trips,
      shapes,
      stops,
      stopTimes
    );
  }
}
