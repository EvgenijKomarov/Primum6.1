import useSWR from 'swr';
import { api } from '@/shared/config/api.ts';
import { getTeacherSchedules } from '@/entity/schedule';

export const useTeacherSchedules = () => {
  const { data, isLoading, mutate } = useSWR(
    [api.teacherSchedule.base],
    async () => (await getTeacherSchedules()).data,
  );

  return { schedules: data?.items ?? [], isLoading, mutate };
};
