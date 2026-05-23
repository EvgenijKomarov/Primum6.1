import useSWR from 'swr';
import { api } from '@/shared/config/api.ts';
import { getStudentLessons } from '@/entity/lesson';
import type { LessonDto } from '@/entity/lesson';

export const useStudentLessons = () => {
  const { data, isLoading } = useSWR(
    [api.studentLesson.getAll],
    async () => (await getStudentLessons(0, 500)).data,
  );

  const sorted: LessonDto[] = [...(data?.items ?? [])].sort(
    (a, b) => new Date(b.dateTime).getTime() - new Date(a.dateTime).getTime(),
  );

  return { lessons: sorted, isLoading };
};
