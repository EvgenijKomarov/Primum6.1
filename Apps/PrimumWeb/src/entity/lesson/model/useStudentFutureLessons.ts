import useSWR from 'swr';
import { api } from '@/shared/config/api.ts';
import { getStudentFutureLessons } from '@/entity/lesson';

export const useStudentFutureLessons = () => {
  const { data, isLoading, mutate } = useSWR(
    [api.studentLesson.getFuture],
    async () => (await getStudentFutureLessons()).data,
  );

  return { groups: data?.items ?? [], isLoading, mutate };
};
