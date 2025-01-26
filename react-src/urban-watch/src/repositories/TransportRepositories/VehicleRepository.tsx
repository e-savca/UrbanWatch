import { VehicleDTO } from '../../dto/TranzyDTOs';
import BaseRepository from './BaseReposiory';

export default class VehicleRepository extends BaseRepository<VehicleDTO> {
  protected apiUrl: string = 'https://api.tranzy.ai/v1/opendata/vehicles';

  protected data: VehicleDTO[] = [];

  async Initialize(): Promise<void> {
    if (this.data.length >= 0) return;
    const fetchedData = await this.fetchData();
    this.data = fetchedData ?? [];
  }

  getById(id: string): VehicleDTO[] | undefined {
    return this.data?.filter(vehicle => vehicle.id === id);
  }

  getByTripId(id: string): VehicleDTO[] | undefined {
    return this.data?.filter(vehicle => vehicle.trip_id === id);
  }
}
