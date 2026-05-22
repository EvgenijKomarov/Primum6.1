import useSWRImmutable from 'swr/immutable';
import { api } from '@/shared/config/api.ts';
import { getStudentProfile } from '@/entity/student';

export const useStudentProfile = (enabled: boolean) => {
  const { data, isLoading, mutate } = useSWRImmutable(
    enabled ? [api.student.getProfile] : null,
    async () => (await getStudentProfile()).data,
    { revalidateOnMount: true },
  );

  return { studentProfile: data, isLoading, mutate };
};
