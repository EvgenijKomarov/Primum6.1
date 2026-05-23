import { fetcherInstance } from '@/shared/api/axios.ts';
import { api } from '@/shared/config/api.ts';
import type { StudentProfileDto } from '@/entity/student';

export const getStudentProfile = async () => {
  return await fetcherInstance<StudentProfileDto>({
    method: 'GET',
    url: api.student.getProfile,
  });
};

export const subscribeToCourse = async (courseId: number, teacherScheduleId: number) => {
  return await fetcherInstance<number>({
    method: 'POST',
    url: `${api.student.subscribe}/${courseId}/${teacherScheduleId}`,
  });
};
