import useSWRImmutable from 'swr/immutable';
import { api } from '@/shared/config/api.ts';
import { getStudentBalance, getStudentProfile } from '@/entity/student';

export const useStudentProfile = (enabled: boolean) => {
  const { data, isLoading, mutate } = useSWRImmutable(
    enabled ? [api.student.getProfile] : null,
    async () => (await getStudentProfile()).data,
    { revalidateOnMount: true },
  );

  return { studentProfile: data, isLoading, mutate };
};

export const useStudentBalance = () => {
  const { data, isLoading, mutate } = useSWRImmutable(
    [api.studentBalance.get],
    async () => (await getStudentBalance()).data
  );

  return { studentBalance: data ? +data : null, isLoading, mutate };
};