const API_KEY = import.meta.env.VITE_TRANZY_API_KEY;

export default abstract class BaseRepository<T> {
  protected agencyId: string = '4';

  protected abstract apiUrl: string;

  private abortController: AbortController | null = null;

  protected async fetchData(): Promise<T[]> {
    this.abortPreviousRequest();

    this.abortController = new AbortController();
    const { signal } = this.abortController;

    const options = {
      method: 'GET',
      headers: {
        'X-Agency-Id': this.agencyId,
        Accept: 'application/json',
        'X-API-KEY': API_KEY,
      },
      signal,
    };

    try {
      const response = await fetch(this.apiUrl, options);
      if (!response.ok) {
        throw new Error(
          `Failed to fetch data: ${response.status} ${response.statusText}`
        );
      }

      const data: T[] = await response.json();

      if (!Array.isArray(data)) {
        throw new Error('Invalid response format, expected an array.');
      }

      return data;
    } catch (error) {
      if ((error as Error).name === 'AbortError') {
        return [];
      }
      throw new Error(`Error fetching data: ${(error as Error).message}`);
    }
  }

  // Cancel ongoing request
  public abortPreviousRequest() {
    if (this.abortController) {
      this.abortController.abort();
      this.abortController = null;
    }
  }
}
