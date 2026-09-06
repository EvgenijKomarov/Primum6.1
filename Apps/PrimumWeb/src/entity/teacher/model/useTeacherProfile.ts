import useSWRImmutable from 'swr/immutable';
import { api } from '@/shared/config/api.ts';
import { getTeacherProfile } from '@/entity/teacher';

export const useTeacherProfile = (enabled: boolean) => {
  const { data, isLoading, mutate } = useSWRImmutable(
    enabled ? [api.teacher.getProfile] : null,
    async () => (await getTeacherProfile()).data,
    { revalidateOnMount: true },
  );

  return { teacherProfile: data, isLoading, mutate };
};
