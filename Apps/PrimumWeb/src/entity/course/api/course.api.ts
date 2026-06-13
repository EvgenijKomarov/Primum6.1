import { fetcherInstance } from '@/shared/api/axios.ts';
import { api } from '@/shared/config/api.ts';
import type { CourseDto, CourseDtoPageResult, CourseInputDto } from '@/entity/course';

export const getTeacherCourses = async (page = 0, pageSize = 50) => {
  return await fetcherInstance<CourseDtoPageResult>({
    method: 'GET',
    url: api.teacherCourse.base,
    params: { page, pageSize },
  });
};

export const createCourse = async (data: CourseInputDto) => {
  return await fetcherInstance<number>({
    method: 'POST',
    url: api.teacherCourse.base,
    data,
  });
};

export const editCourse = async (courseId: number, data: CourseInputDto) => {
  return await fetcherInstance<number>({
    method: 'PUT',
    url: `${api.teacherCourse.base}/${courseId}`,
    data,
  });
};

export const changeActivityCourse = async (courseId: number, data: boolean) => {
  return await fetcherInstance<number>({
    method: 'PATCH',
    url: `${api.teacherCourse.base}/${courseId}/activity`,
    headers: { 'Content-Type': 'application/json' },
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
