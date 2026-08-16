import { api } from "@/shared/config/api";
import useSWR from "swr";
import { getUserInfo } from "../api/userByAdmin.api";

export const useUsersByAdmin = (displayName: string, page = 0, pageSize = 20) => {
    const { data, isLoading, mutate } = useSWR(
    [api.user.byAdmin, displayName, page, pageSize],
    async () => (await getUserInfo(displayName, page, pageSize)).data,
  );

  return { users: data?.items ?? [], isLoading, mutate };
}