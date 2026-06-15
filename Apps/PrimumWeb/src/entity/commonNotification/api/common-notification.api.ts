import { fetcherInstance } from "@/shared/api/axios";
import { api } from "@/shared/config/api";
import type { CommonNotification } from "../model/types";


export const getAllNotifications = async () => {
  return await fetcherInstance<CommonNotification[]>({
    method: 'GET',
    url: api.userNotifications.getAll,
    headers: {
      'Content-Type': 'application/json',
    },
  });
};

export const setSeenNotification = async (id: string) => {
  return await fetcherInstance<boolean>({
    method: 'POST',
    url: `${api.userNotifications.setSeen}/${id}`,
    headers: {
      'Content-Type': 'application/json',
    },
  });
};