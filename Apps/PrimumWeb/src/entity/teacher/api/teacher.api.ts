import { fetcherInstance } from '@/shared/api/axios.ts';
import { api } from '@/shared/config/api.ts';
import type { TeacherProfileDto } from '@/entity/teacher';

export const getTeacherProfile = async () => {
  return await fetcherInstance<TeacherProfileDto>({
    method: 'GET',
    url: api.teacher.getProfile,
  });
};
