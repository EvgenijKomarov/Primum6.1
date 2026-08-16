import { fetcherInstance } from "@/shared/api/axios";
import type { UserDtoPageResult } from "../model/types";
import { api } from "@/shared/config/api";


export const getUserInfo = async (displayName: string, page = 0, pageSize = 20) => {
  return await fetcherInstance<UserDtoPageResult>({
    method: 'GET',
    url: api.user.byAdmin,
    params: { displayName, page, pageSize },
  });
};

export const changeBanStatus =  async (userId: number, data: boolean) => {
  return await fetcherInstance({
    method: 'PATCH',
    url: `${api.user.byAdmin}/${userId}/ban-status`,
    headers: { 'Content-Type': 'application/json' },
    data
  });
}

export const createAdminProfile =  async (userId: number, status: string) => {
  return await fetcherInstance({
    method: 'POST',
    url: `${api.user.byAdmin}/${userId}/admin-profile`,
    params: { status },
  });
}