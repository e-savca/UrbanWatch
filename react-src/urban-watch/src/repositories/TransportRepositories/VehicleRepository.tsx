import { VehicleDTO } from '../../dto/TranzyDTOs';
import BaseRepository from './BaseReposiory';

export default class VehicleRepository extends BaseRepository<VehicleDTO> {
  protected apiUrl: string = 'https://api.tranzy.ai/v1/opendata/vehicles';

  protected data: VehicleDTO[] | null = null;

  async Initialize(): Promise<void> {
    if (this.data !== null) return;
    this.data = await this.fetchData();
  }

  getById(id: string): VehicleDTO[] | null {
    return this.data?.filter(vehicle => vehicle.id === id) || null;
  }
}
