import useSWR from 'swr';
import { api } from '@/shared/config/api.ts';
import { getStudentAbonementsAsync, GetStudentAbonementSchedulesAsync } from '../api/abonement.api';

export const useStudentAbonements = () => {
    const { data, isLoading, mutate } = useSWR(
        [api.studentAbonement.base],
        async () => (await getStudentAbonementsAsync()).data,
    );

  return { abonements: data?.items ?? [], isLoading, mutate };
};

export const useAbonementSchedules = (id: number) => {
    const { data, isLoading, mutate } = useSWR(
        [`${api.studentAbonement.base}/${id}/shedules`],
        async () => (await GetStudentAbonementSchedulesAsync(id)).data,
    );

  return { schedules: data?.items ?? [], isLoading, mutate };
};