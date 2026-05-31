import { fetcherInstance } from '@/shared/api/axios.ts';
import { api } from '@/shared/config/api.ts';
import type { CourseRankDtoPageResult } from '../model/types';

export const getCourseRanks = async (page = 0, pageSize = 20) => {
  return await fetcherInstance<CourseRankDtoPageResult>({
    method: 'GET',
    url: api.ranks.course,
    params: { page, pageSize },
  });
};