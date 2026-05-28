export class FetchError<T = unknown> extends Error {
  constructor(
    message: string,
    public readonly data: T,
  ) {
    super(message);
    this.name = 'FetchError';
  }
}