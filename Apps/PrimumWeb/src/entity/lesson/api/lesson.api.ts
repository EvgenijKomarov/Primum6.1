import { fetcherInstance } from '@/shared/api/axios.ts';
import { api } from '@/shared/config/api.ts';
import type { LessonDtoPageResult, LessonsByDateDtoPageResult } from '@/entity/lesson';

export const getStudentLessons = async (page = 0, pageSize = 20) => {
  return await fetcherInstance<LessonDtoPageResult>({
    method: 'GET',
    url: api.studentLesson.getAll,
    params: { page, pageSize },
  });
};

export const getStudentFutureLessons = async (page = 0, pageSize = 100) => {
  return await fetcherInstance<LessonsByDateDtoPageResult>({
    method: 'GET',
    url: api.studentLesson.getFuture,
    params: { page, pageSize },
  });
};
