import { VehicleDTO } from '../../dto/TranzyDTOs';
import BaseRepository from './BaseReposiory';

export default class VehicleRepository extends BaseRepository<VehicleDTO> {
  protected apiUrl: string = 'https://api.tranzy.ai/v1/opendata/vehicles';

  protected data: VehicleDTO[] = [];

  private async Initialize(): Promise<void> {
    if (this.data.length > 0) return;
    const fetchedData = await this.fetchData();
    this.data = fetchedData ?? [];
  }

  async getById(id: string): Promise<VehicleDTO[] | undefined> {
    if (this.data.length === 0) await this.Initialize();
    return this.data?.filter(vehicle => vehicle.id === id);
  }

  async getByTripId(id: string): Promise<VehicleDTO[] | undefined> {
    if (this.data.length === 0) await this.Initialize();
    return this.data?.filter(vehicle => vehicle.trip_id === id);
  }
}
