import { fetcherInstance } from '@/shared/api/axios.ts';
import { api } from '@/shared/config/api.ts';
import type { TeacherProfileDto } from '@/entity/teacher';
import type { TeacherScheduleDtoPageResult } from '@/entity/schedule';

export const getTeacherProfile = async () => {
  return await fetcherInstance<TeacherProfileDto>({
    method: 'GET',
    url: api.teacher.getProfile,
  });
};

export const getTeacherProfileById = async (id: number) => {
  return await fetcherInstance<TeacherProfileDto>({
    method: 'GET',
    url: `${api.teacher.getById}/${id}`,
  });
};

export const getPublicTeacherSchedules = async (teacherId: number, page = 0, pageSize = 500) => {
  return await fetcherInstance<TeacherScheduleDtoPageResult>({
    method: 'GET',
    url: `${api.publicTeacher.getSchedules}/${teacherId}/shedules`,
    params: { page, pageSize },
  });
};
