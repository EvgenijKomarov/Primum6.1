import { fetcherInstance } from '@/shared/api/axios.ts';
import { api } from '@/shared/config/api.ts';
import type { GradingInputDto, LessonDtoPageResult, LessonReportStatus, LessonsByDateDtoPageResult } from '@/entity/lesson';

export const getStudentLessons = async (page = 0, pageSize = 20) => {
  return await fetcherInstance<LessonDtoPageResult>({
    method: 'GET',
    url: api.studentLesson.getLast,
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

export const getTeacherLessons = async (page = 0, pageSize = 20) => {
  return await fetcherInstance<LessonDtoPageResult>({
    method: 'GET',
    url: api.teacherLesson.getLast,
    params: { page, pageSize },
  });
};

export const getTeacherFutureLessons = async (page = 0, pageSize = 100) => {
  return await fetcherInstance<LessonsByDateDtoPageResult>({
    method: 'GET',
    url: api.teacherLesson.getFuture,
    params: { page, pageSize },
  });
};

export const gradeLesson = async (lessonId: number, data: GradingInputDto) => {
  return await fetcherInstance<number>({
    method: 'POST',
    url: `${api.teacherLesson.base}/${lessonId}/grade`,
    headers: { 'Content-Type': 'application/json' },
    data,
  });
}

export const cancelLesson = async (lessonId: number) => {
  return await fetcherInstance<number>({
    method: 'PATCH',
    url: `${api.studentLesson.base}/${lessonId}/cancel`,
  });
}

export const studentReportLesson = async (lessonId: number, data: LessonReportStatus) => {
  return await fetcherInstance<number>({
    method: 'PATCH',
    url: `${api.studentLesson.base}/${lessonId}/report`,
    headers: { 'Content-Type': 'application/json' },
    data,
  });
}

export const teacherReportLesson = async (lessonId: number, data: LessonReportStatus) => {
  return await fetcherInstance<number>({
    method: 'PATCH',
    url: `${api.teacherLesson.base}/${lessonId}/report`,
    headers: { 'Content-Type': 'application/json' },
    data,
  });
}