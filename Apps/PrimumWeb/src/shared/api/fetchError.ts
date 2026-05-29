export class FetchError<T = unknown> extends Error {
  constructor(
    message: string,
    public readonly data: T,
    public readonly status?: number
  ) {
    super(message);
    this.name = 'FetchError';
  }
}