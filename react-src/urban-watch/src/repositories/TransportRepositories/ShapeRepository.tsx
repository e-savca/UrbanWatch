import { ShapeDTO } from '../../dto/TranzyDTOs';
import BaseRepository from './BaseReposiory';

export default class ShapeRepository extends BaseRepository<ShapeDTO> {
  protected apiUrl: string = 'https://api.tranzy.ai/v1/opendata/shapes';

  protected data: ShapeDTO[] | null = null;

  async Initialize(): Promise<void> {
    if (this.data !== null) return;
    this.data = await this.fetchData();
  }

  getAll(): ShapeDTO[] | null {
    return this.data || null;
  }

  getById(id: string): ShapeDTO[] | null {
    return this.data?.filter(shape => shape.shape_id === id) || null;
  }
}
