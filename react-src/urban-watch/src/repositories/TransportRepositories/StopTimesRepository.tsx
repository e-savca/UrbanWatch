// Not implemented at the moment
// import { StopTimeDTO } from '../../dto/TranzyDTOs';
// import BaseRepository from './BaseReposiory';

// export default class StopTimesRepository extends BaseRepository<StopTimeDTO> {
//   protected apiUrl: string = 'https://api.tranzy.ai/v1/opendata/stop_times';

//   protected data: StopTimeDTO[] | null = null;

//   async Initialize(): Promise<void> {
//     if (this.data !== null) return;
//     this.data = await this.fetchData();
//   }

//   getAll(): StopTimeDTO[] | null {
//     return this.data || null;
//   }

//   getById(id: number): StopTimeDTO[] | null {
//     return this.data?.filter((stopTime) => stopTime.stop_id === id) || null;
//   }
// }
