import { TripDTO } from '../../dto/TranzyDTOs';
import BaseRepository from './BaseReposiory';

export default class StopsRepository extends BaseRepository<TripDTO> {
  protected apiUrl: string = 'https://api.tranzy.ai/v1/opendata/trips';

  protected data: TripDTO[] | null = null;

  async Initialize(): Promise<void> {
    if (this.data !== null) return;
    this.data = await this.fetchData();
  }

  getAll(): TripDTO[] | null {
    return this.data || null;
  }

  getById(id: string): TripDTO[] | null {
    return this.data?.filter(trip => trip.trip_id === id) || null;
  }
}
