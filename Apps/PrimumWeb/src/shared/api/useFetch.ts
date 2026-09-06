import { useState } from 'react';

type SafeArg = unknown;
type AsyncFunction<TArgs extends SafeArg[], TReturn> = (...args: TArgs) => Promise<TReturn>;

export const useFetch = <TArgs extends SafeArg[], TReturn>(
  callback: AsyncFunction<TArgs, TReturn>,
): {
  fetch: AsyncFunction<TArgs, TReturn>;
  isLoading: boolean;
} => {
  const [isLoading, setIsLoading] = useState(false);

  const fetch = async (...data: TArgs): Promise<TReturn> => {
    setIsLoading(true);
    return callback(...data).finally(() => setIsLoading(false));
  };

  return { fetch, isLoading };
};
