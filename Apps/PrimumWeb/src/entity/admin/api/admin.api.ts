import { api } from "@/shared/config/api";
import type { AdminProfileDto, AdminProfileDtoPageResult } from "../model/types";
import { fetcherInstance } from "@/shared/api/axios";


export const getSelfAdminProfile = async () => {
  return await fetcherInstance<AdminProfileDto>({
    method: 'GET',
    url: api.adminProfile.self,
  });
};

export const getAdminProfile = async (userId: number) => {
  return await fetcherInstance<AdminProfileDto>({
    method: 'GET',
    url: `${api.adminProfile.other}/${userId}`,
  });
};

export const getAdminProfiles = async (displayName: string, page = 0, pageSize = 20) => {
  return await fetcherInstance<AdminProfileDtoPageResult>({
    method: 'GET',
    url: api.adminProfile.other,
    params: { displayName, page, pageSize },
  });
};

export const deleteAdminProfile = async (userId: number) => {
  return await fetcherInstance({
    method: 'DELETE',
    url: `${api.adminProfile.other}/${userId}`,
  });
};

export const changeAdminPermissions = async (userId: number, data: Record<string, boolean>) => {
  return await fetcherInstance({
    method: 'PATCH',
    url: `${api.adminProfile.other}/${userId}/permissions`,
    headers: {
      'Content-Type': 'application/json',
    },
    data
  });
};