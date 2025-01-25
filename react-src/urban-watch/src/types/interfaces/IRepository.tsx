export interface IRepository<T> {
  getAll(): T[] | null;
  getById(id: string | number): T | null;
}
