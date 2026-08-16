import { api } from "@/shared/config/api";
import useSWR from "swr";
import { getAdminProfiles } from "../api/admin.api";


export const useAdminProfiles = (displayName: string, page = 0, pageSize = 20) => {
    const { data, isLoading, mutate } = useSWR(
    [api.adminProfile.other, displayName, page, pageSize],
    async () => (await getAdminProfiles(displayName, page, pageSize)).data,
  );

  return { admins: data?.items ?? [], isLoading, mutate };
}