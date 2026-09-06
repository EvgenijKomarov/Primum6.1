import { fetcherInstance } from '@/shared/api/axios.ts';
import { api } from '@/shared/config/api.ts';
import type { StudentRankDtoPageResult } from '../model/types';

export const getStudentRanks = async (page = 0, pageSize = 20) => {
  return await fetcherInstance<StudentRankDtoPageResult>({
    method: 'GET',
    url: api.ranks.student,
    params: { page, pageSize },
  });
};