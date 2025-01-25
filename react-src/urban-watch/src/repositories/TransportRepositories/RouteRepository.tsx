import { RouteDTO } from '../../dto/TranzyDTOs';
import BaseRepository from './BaseReposiory';

export default class RouteRepository extends BaseRepository<RouteDTO> {
  protected apiUrl: string = 'https://api.tranzy.ai/v1/opendata/routes';

  protected data: RouteDTO[] | null = null;

  async Initialize(): Promise<void> {
    if (this.data !== null) return;
    this.data = await this.fetchData();
  }

  getAll(): RouteDTO[] | null {
    return this.data || null;
  }

  getById(id: number): RouteDTO | null {
    return this.data?.find(route => route.route_id === id) || null;
  }
}
