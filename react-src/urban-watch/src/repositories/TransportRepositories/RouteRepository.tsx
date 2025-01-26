import { RouteDTO } from '../../dto/TranzyDTOs';
import BaseRepository from './BaseReposiory';

export default class RouteRepository extends BaseRepository<RouteDTO> {
  protected apiUrl: string = 'https://api.tranzy.ai/v1/opendata/routes';

  protected data: RouteDTO[] = [];

  async Initialize(): Promise<void> {
    if (this.data.length >= 0) return;
    const fetchedData = await this.fetchData();

    this.data = fetchedData ?? [];
  }

  getAll(): RouteDTO[] {
    return this.data;
  }

  getById(id: number): RouteDTO | undefined {
    return this.data?.find(route => route.route_id === id);
  }
}
