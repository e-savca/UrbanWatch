import { RouteDTO } from '../../dto/TranzyDTOs';
import BaseRepository from './BaseReposiory';

function sortRoutesByShortName(a: RouteDTO, b: RouteDTO): number {
  const regex = /^(\d+)?([a-zA-Z]*)$/;

  const parseShortName = (
    shortName: string
  ): { number: number | null; text: string } => {
    const match = shortName.match(regex);
    return {
      number: match && match[1] ? parseInt(match[1], 10) : null,
      text: match && match[2] ? match[2] : '',
    };
  };

  if (a.route_type !== b.route_type) {
    return b.route_type - a.route_type;
  }

  const parsedA = parseShortName(a.route_short_name);
  const parsedB = parseShortName(b.route_short_name);

  if (parsedA.number !== null && parsedB.number !== null) {
    if (parsedA.number !== parsedB.number) {
      return parsedA.number - parsedB.number;
    }
  } else if (parsedA.number !== null) {
    return -1;
  } else if (parsedB.number !== null) {
    return 1;
  }

  return parsedA.text.localeCompare(parsedB.text);
}

export default class RouteRepository extends BaseRepository<RouteDTO> {
  protected apiUrl: string = 'https://api.tranzy.ai/v1/opendata/routes';

  protected data: RouteDTO[] = [];

  private async Initialize(): Promise<void> {
    if (this.data.length > 0) return;
    const fetchedData = await this.fetchData();

    this.data = fetchedData.sort(sortRoutesByShortName) ?? [];
  }

  async getAll(): Promise<RouteDTO[]> {
    if (this.data.length === 0) await this.Initialize();
    return this.data;
  }

  async getById(id: number): Promise<RouteDTO | undefined> {
    if (this.data.length === 0) await this.Initialize();
    return this.data?.find(route => route.route_id === id);
  }
}
