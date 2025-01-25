import { StopDTO } from '../../dto/TranzyDTOs';
import BaseRepository from './BaseReposiory';

export default class StopsRepository extends BaseRepository<StopDTO> {
  protected apiUrl: string = 'https://api.tranzy.ai/v1/opendata/stops';

  protected data: StopDTO[] | null = null;

  async Initialize(): Promise<void> {
    if (this.data !== null) return;
    this.data = await this.fetchData();
  }

  getAll(): StopDTO[] | null {
    return this.data || null;
  }

  getById(id: number): StopDTO[] | null {
    return this.data?.filter(stop => stop.stop_id === id) || null;
  }
}
