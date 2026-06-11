import { fetcherInstance } from "@/shared/api/axios";
import { api } from "@/shared/config/api";
import type { PromocodeDto, PromocodeDtoPageResult } from "../model/types";


export const getStudentPromocodes = async (page = 0, pageSize = 20) => {
  return await fetcherInstance<PromocodeDtoPageResult>({
    method: 'GET',
    url: api.promocodes.student,
    params: { page, pageSize },
  });
};

export const getAvailablePromocodes = async (page = 0, pageSize = 20) => {
  return await fetcherInstance<PromocodeDtoPageResult>({
    method: 'GET',
    url: api.promocodes.available,
    params: { page, pageSize },
  });
};

export const buyPromocode = async (promocodeId: number) => {
  return await fetcherInstance<PromocodeDto>({
    method: 'POST',
    url: `${api.promocodes.buy}/${promocodeId}`
  });
};