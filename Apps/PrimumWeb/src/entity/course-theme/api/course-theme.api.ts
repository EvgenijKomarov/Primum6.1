import { fetcherInstance } from '@/shared/api/axios.ts';
import { api } from '@/shared/config/api.ts';
import type { CourseThemeDtoPageResult } from '@/entity/course-theme';

export const getPublicThemes = async (page = 0, pageSize = 100) => {
  return await fetcherInstance<CourseThemeDtoPageResult>({
    method: 'GET',
    url: api.publicTheme.getThemes,
    params: { page, pageSize },
  });
};
