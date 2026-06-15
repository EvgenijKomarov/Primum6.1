import { api } from "@/shared/config/api";
import useSWR from "swr";
import { getAllNotifications } from "../api/common-notification.api";


export const useCommonNotifications = () => {
  const { data, isLoading, mutate } = useSWR(
    [api.userNotifications.getAll],
    async () => (await getAllNotifications()).data,
  );

  return { notifications: data ?? [], isLoading, mutate };
};