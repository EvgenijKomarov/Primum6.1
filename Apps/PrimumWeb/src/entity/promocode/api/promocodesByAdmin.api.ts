import { fetcherInstance } from "@/shared/api/axios";
import type { PromocodeDtoPageResult, PromocodeInputDto } from "../model/types";
import { api } from "@/shared/config/api";



export const getPromocodes = async (searchQuery?: string, page = 0, pageSize = 20) => {
  return await fetcherInstance<PromocodeDtoPageResult>({
    method: 'GET',
    url: api.promocodes.byAdmin,
    params: { searchQuery, page, pageSize },
  });
};

export const deletePromocode = async (promocodeId?: number) => {
  return await fetcherInstance({
    method: 'DELETE',
    url: `api.promocodes.byAdmin/${promocodeId}`
  });
};

export const addPromocode = async (data: PromocodeInputDto) => {
  return await fetcherInstance({
    method: 'POST',
    url: api.promocodes.byAdmin,
    headers: { 'Content-Type': 'application/json' },
    data
  });
};