import { fetcherInstance } from '@/shared/api/axios.ts';
import { api } from '@/shared/config/api.ts';
import type { CourseDtoPageResult, CourseInputDto } from '@/entity/course';

export const getTeacherCourses = async (page = 0, pageSize = 50) => {
  return await fetcherInstance<CourseDtoPageResult>({
    method: 'GET',
    url: api.teacherCourse.getCourses,
    params: { page, pageSize },
  });
};

export const createCourse = async (data: CourseInputDto) => {
  return await fetcherInstance<number>({
    method: 'POST',
    url: api.teacherCourse.getCourses,
    data,
  });
};
