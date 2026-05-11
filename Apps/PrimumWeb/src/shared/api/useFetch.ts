import { useState } from 'react';

type SafeArg = string | number | boolean | null | undefined | object;
type AsyncFunction<TArgs extends SafeArg[], TReturn> = (...args: TArgs) => Promise<TReturn>;

export const useFetch = <TArgs extends SafeArg[], TReturn>(
  callback: AsyncFunction<TArgs, TReturn>,
): {
  fetch: AsyncFunction<TArgs, TReturn>;
  isLoading: boolean;
} => {
  const [isLoading, setIsLoading] = useState(false);

  const fetch = async (...data: TArgs): Promise<TReturn> => {
    try {
      setIsLoading(true);

      return await callback(...data);
    } catch (error) {
      if (error instanceof Error) {
        throw error;
      }

      throw new Error(String(error));
    } finally {
      setIsLoading(false);
    }
  };

  return { fetch, isLoading };
};
