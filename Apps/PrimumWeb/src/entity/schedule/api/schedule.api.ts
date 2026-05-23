import { fetcherInstance } from '@/shared/api/axios.ts';
import { api } from '@/shared/config/api.ts';
import type { TeacherScheduleDtoPageResult, TeacherScheduleInputDto } from '@/entity/schedule';

export const getTeacherSchedules = async (page = 0, pageSize = 500) => {
  return await fetcherInstance<TeacherScheduleDtoPageResult>({
    method: 'GET',
    url: api.teacherSchedule.base,
    params: { page, pageSize },
  });
};

export const createTeacherSchedule = async (data: TeacherScheduleInputDto) => {
  return await fetcherInstance<number>({
    method: 'POST',
    url: api.teacherSchedule.base,
    data,
  });
};

export const deleteTeacherSchedule = async (scheduleId: number) => {
  return await fetcherInstance<number>({
    method: 'DELETE',
    url: `${api.teacherSchedule.base}/${scheduleId}`,
  });
};
