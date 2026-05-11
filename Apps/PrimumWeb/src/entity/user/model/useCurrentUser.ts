import useSWRImmutable from 'swr/immutable';
import { getUserInfo } from '@/entity/user';
import { api } from '@/shared/config/api.ts';

export const useCurrentUser = () => {
  const {
    data: user,
    isLoading,
    mutate,
  } = useSWRImmutable(
    [api.user.getUserInfo],
    async () => {
      const response = await getUserInfo();
      return response.data;
    },
    { revalidateOnMount: true }
  );

  return {
    user,
    isLoading,
    mutate,
  };
}