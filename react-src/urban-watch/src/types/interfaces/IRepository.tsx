export interface IRepository<T> {
  getAll(): Promise<T[]>;
  getById(id: string): Promise<T>;
}
