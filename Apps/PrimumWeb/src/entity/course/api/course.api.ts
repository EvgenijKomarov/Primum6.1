import { fetcherInstance } from '@/shared/api/axios.ts';
import { api } from '@/shared/config/api.ts';
import type { CourseDto, CourseDtoPageResult, CourseInputDto } from '@/entity/course';

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

export const getPublicCourses = async (page = 0, pageSize = 20) => {
  return await fetcherInstance<CourseDtoPageResult>({
    method: 'GET',
    url: api.publicCourse.getAll,
    params: { page, pageSize },
  });
};

export const getPublicCourse = async (courseId: number) => {
  return await fetcherInstance<CourseDto>({
    method: 'GET',
    url: `${api.publicCourse.getAll}/${courseId}`
  });
};

export const getPublicCoursesByTheme = async (themeId: number, page = 0, pageSize = 20) => {
  return await fetcherInstance<CourseDtoPageResult>({
    method: 'GET',
    url: `${api.publicCourse.getByTheme}/${themeId}`,
    params: { page, pageSize },
  });
};
