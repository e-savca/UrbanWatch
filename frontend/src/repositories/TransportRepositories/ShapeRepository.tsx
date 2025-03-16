import { ShapeDTO } from '../../dto/TranzyDTOs';
import BaseRepository from './BaseReposiory';

export default class ShapeRepository extends BaseRepository<ShapeDTO> {
  private baseUrl: string =
    'https://api.tranzy.ai/v1/opendata/shapes?shape_id=';

  protected apiUrl: string = '';

  private setApiUrl(id: string) {
    this.apiUrl = this.baseUrl + id;
  }

  async getByTripId(id: string): Promise<ShapeDTO[] | undefined> {
    this.setApiUrl(id);

    const shapes = await this.fetchData();

    return shapes;
  }
}
