import { fetcherInstance } from "@/shared/api/axios";
import { api } from "@/shared/config/api";
import type { TeacherRankDtoPageResult } from "../model/types";

export const getTeacherRanks = async (page = 0, pageSize = 20) => {
  return await fetcherInstance<TeacherRankDtoPageResult>({
    method: 'GET',
    url: api.ranks.teacher,
    params: { page, pageSize },
  });
};