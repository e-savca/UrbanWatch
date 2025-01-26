import { StopDTO } from '../../dto/TranzyDTOs';
import BaseRepository from './BaseReposiory';

export default class StopsRepository extends BaseRepository<StopDTO> {
  protected apiUrl: string = 'https://api.tranzy.ai/v1/opendata/stops';

  protected data: StopDTO[] = [];

  async Initialize(): Promise<void> {
    if (this.data.length > 0) return;
    const fetchData = await this.fetchData();
    this.data = fetchData ?? [];
  }

  getAll(): StopDTO[] {
    return this.data;
  }

  getById(id: number): StopDTO[] | undefined {
    return this.data.filter(stop => stop.stop_id === id);
  }
}
