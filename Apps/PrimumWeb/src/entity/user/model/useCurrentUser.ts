import useSWRImmutable from 'swr/immutable';
import { userApiConfig } from '@/entity/user/config';
import { getUserInfo } from '@/entity/user';

export const useCurrentUser = () => {
  const {
    data: user,
    isLoading,
    mutate,
  } = useSWRImmutable(
    [userApiConfig.getUserInfo],
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