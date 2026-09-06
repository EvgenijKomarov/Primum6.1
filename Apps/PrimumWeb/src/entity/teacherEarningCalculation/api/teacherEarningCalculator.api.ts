import { fetcherInstance } from "@/shared/api/axios";
import { api } from "@/shared/config/api";
import type { TeacherEarningDto } from "../model/types";


export const getTeacherEarning = async () => {
  return await fetcherInstance<TeacherEarningDto>({
    method: 'GET',
    url: api.teacherEarnings.base,
  });
};