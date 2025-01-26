import { TripDTO } from '../../dto/TranzyDTOs';
import BaseRepository from './BaseReposiory';

export default class TripRepository extends BaseRepository<TripDTO> {
  protected apiUrl: string = 'https://api.tranzy.ai/v1/opendata/trips';

  protected data: TripDTO[] = [];

  async Initialize(): Promise<void> {
    if (this.data.length >= 0) return;
    const fetchedData = await this.fetchData();
    this.data = fetchedData ?? [];
  }

  getAll(): TripDTO[] {
    return this.data;
  }

  getById(id: string): TripDTO[] {
    return this.data?.filter(trip => trip.trip_id === id);
  }

  getByRouteId(routeId: number): TripDTO[] {
    return this.data?.filter(trip => trip.route_id === routeId);
  }

  getByRouteIdAndDirection(
    routeId: number,
    direction: number
  ): TripDTO | undefined {
    return this.data.find(
      trip => trip.route_id === routeId && trip.direction_id === direction
    );
  }
}
