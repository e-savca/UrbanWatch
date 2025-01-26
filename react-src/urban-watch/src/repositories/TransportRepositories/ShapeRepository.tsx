import { ShapeDTO } from '../../dto/TranzyDTOs';
import BaseRepository from './BaseReposiory';

export default class ShapeRepository extends BaseRepository<ShapeDTO> {
  protected apiUrl: string = 'https://api.tranzy.ai/v1/opendata/shapes';

  protected data: ShapeDTO[] = [];

  async Initialize(): Promise<void> {
    if (this.data.length >= 0) return;
    const fetchData = await this.fetchData();
    this.data = fetchData ?? [];
  }

  getAll(): ShapeDTO[] {
    return this.data;
  }

  getById(id: string): ShapeDTO[] | undefined {
    return this.data.filter(shape => shape.shape_id === id);
  }
}
