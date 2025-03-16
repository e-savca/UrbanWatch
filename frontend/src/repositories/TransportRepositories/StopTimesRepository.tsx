import { StopTimeDTO } from '../../dto/TranzyDTOs';
import BaseRepository from './BaseReposiory';

export default class StopTimesRepository extends BaseRepository<StopTimeDTO> {
  protected apiUrl: string = 'https://api.tranzy.ai/v1/opendata/stop_times';

  protected data: StopTimeDTO[] = [];

  private async Initialize(): Promise<void> {
    if (this.data.length > 0) return;
    const fetchedData = await this.fetchData();
    this.data = fetchedData ?? [];
  }

  async getByStopId(id: number): Promise<StopTimeDTO[]> {
    if (this.data.length === 0) await this.Initialize();
    return this.data.filter(stopTime => stopTime.stop_id === id);
  }
}
