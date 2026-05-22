import useSWR from 'swr';
import { api } from '@/shared/config/api.ts';
import { getTeacherCourses } from '@/entity/course';

export const useTeacherCourses = () => {
  const { data, isLoading, mutate } = useSWR(
    [api.teacherCourse.getCourses],
    async () => (await getTeacherCourses()).data,
  );

  return { courses: data?.items ?? [], isLoading, mutate };
};
