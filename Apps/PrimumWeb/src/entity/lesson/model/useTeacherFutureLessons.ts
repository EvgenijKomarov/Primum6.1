import useSWR from 'swr';
import { api } from '@/shared/config/api.ts';
import { getTeacherFutureLessons } from '../api/lesson.api';

export const useTeacherFutureLessons = () => {
  const { data, isLoading, mutate } = useSWR(
    [api.teacherLesson.getFuture],
    async () => (await getTeacherFutureLessons()).data,
  );

  return { groups: data?.items ?? [], isLoading, mutate };
};
