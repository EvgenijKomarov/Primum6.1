import useSWR from 'swr';
import { api } from '@/shared/config/api.ts';
import { getTeacherLessons, type LessonDto } from '@/entity/lesson';

export const useTeacherLessons = () => {
  const { data, isLoading, mutate } = useSWR(
    [api.teacherLesson.getLast],
    async () => (await getTeacherLessons(0, 500)).data,
  );

  const sorted: LessonDto[] = [...(data?.items ?? [])].sort(
    (a, b) => new Date(b.dateTime).getTime() - new Date(a.dateTime).getTime(),
  );

  return { lessons: sorted, isLoading, mutate };
};