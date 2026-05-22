import useSWR from 'swr';
import { api } from '@/shared/config/api.ts';
import { getPublicCourses, getPublicCoursesByTheme } from '@/entity/course';

export const usePublicCourses = (themeId: number | null) => {
  const key = themeId
    ? [api.publicCourse.getByTheme, themeId]
    : [api.publicCourse.getAll];

  const { data, isLoading } = useSWR(key, async () => {
    const res = themeId
      ? await getPublicCoursesByTheme(themeId)
      : await getPublicCourses();
    return res.data;
  });

  return { courses: data?.items ?? [], isLoading };
};
