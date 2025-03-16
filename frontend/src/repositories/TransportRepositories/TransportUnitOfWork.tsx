import RouteRepository from './RouteRepository';
import VehicleRepository from './VehicleRepository';
import TripRepository from './TripRepository';
import ShapeRepository from './ShapeRepository';
import StopsRepository from './StopsRepository';
import StopTimesRepository from './StopTimesRepository';

export default class TransportUnitOfWork {
  //   NOT IMPLEMENTED
  //   public Agency: AgencyRepository;

  public Vehicles: VehicleRepository = new VehicleRepository();

  public Routes: RouteRepository = new RouteRepository();

  public Trips: TripRepository = new TripRepository();

  public Shapes: ShapeRepository = new ShapeRepository();

  public Stops: StopsRepository = new StopsRepository();

  public StopTimes: StopTimesRepository = new StopTimesRepository();
}
