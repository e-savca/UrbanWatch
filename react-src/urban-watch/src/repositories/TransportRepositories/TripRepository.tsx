import { TripDTO } from '../../dto/TranzyDTOs';
import BaseRepository from './BaseReposiory';

export default class TripRepository extends BaseRepository<TripDTO> {
  protected apiUrl: string = 'https://api.tranzy.ai/v1/opendata/trips';

  protected data: TripDTO[] = [];

  private async Initialize(): Promise<void> {
    if (this.data.length > 0) return;
    const fetchedData = await this.fetchData();
    this.data = fetchedData ?? [];
  }

  async getAll(): Promise<TripDTO[]> {
    if (this.data.length === 0) await this.Initialize();
    return this.data;
  }

  async getById(id: string): Promise<TripDTO | undefined> {
    if (this.data.length === 0) await this.Initialize();
    return this.data?.find(trip => trip.trip_id === id);
  }

  async getByRouteId(routeId: number): Promise<TripDTO[]> {
    if (this.data.length === 0) await this.Initialize();
    return this.data?.filter(trip => trip.route_id === routeId);
  }

  async getByRouteIdAndDirection(
    routeId: number,
    direction: number
  ): Promise<TripDTO | undefined> {
    if (this.data.length === 0) await this.Initialize();
    return this.data.find(
      trip => trip.route_id === routeId && trip.direction_id === direction
    );
  }
}
