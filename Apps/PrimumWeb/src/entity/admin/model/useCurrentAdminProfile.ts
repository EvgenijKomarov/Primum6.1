import useSWR from "swr";
import { getAdminProfile, getSelfAdminProfile } from "../api/admin.api";
import { api } from "@/shared/config/api";

export const useSelfAdminProfile = () => {
  const { data, isLoading, mutate } = useSWR(
    [api.adminProfile.self],
    async () => (await getSelfAdminProfile()).data,
  );

  return { adminProfile: data, isLoading, mutate };
};

export const useAdminProfile = (userId: number) => {
  const { data, isLoading, mutate } = useSWR(
    [api.adminProfile.other, userId],
    async () => (await getAdminProfile(userId)).data,
  );

  return { adminProfile: data, isLoading, mutate };
};