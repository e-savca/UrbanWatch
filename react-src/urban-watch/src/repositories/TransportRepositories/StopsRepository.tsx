import { StopDTO } from '../../dto/TranzyDTOs';
import BaseRepository from './BaseReposiory';

export default class StopsRepository extends BaseRepository<StopDTO> {
  protected apiUrl: string = 'https://api.tranzy.ai/v1/opendata/stops';

  protected data: StopDTO[] = [];

  private async Initialize(): Promise<void> {
    if (this.data.length > 0) return;
    const fetchData = await this.fetchData();
    this.data = fetchData ?? [];
  }

  async getAll(): Promise<StopDTO[]> {
    if (this.data.length === 0) await this.Initialize();
    return this.data;
  }

  async getById(id: number): Promise<StopDTO[] | undefined> {
    if (this.data.length === 0) await this.Initialize();
    return this.data.filter(stop => stop.stop_id === id);
  }
}
