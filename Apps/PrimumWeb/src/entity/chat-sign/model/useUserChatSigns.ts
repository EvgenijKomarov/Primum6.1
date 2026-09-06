import { api } from "@/shared/config/api";
import useSWR from "swr";
import { getChatSigns, getChatSignsByAdmin } from "../api/chat-sign.api";

export const useChatSigns = (enabled = true) => {
  const { data, isLoading, mutate } = useSWR(
    enabled ? [api.user.chatSigns] : null,
    async () => (await getChatSigns(0, 500)).data,
  );

  return { signs: data?.items ?? [], isLoading, mutate };
};

export const useUserChatSigns = (userId: number) => {
  const { data, isLoading, mutate } = useSWR(
    [api.user.chatSigns, userId],
    async () => (await getChatSignsByAdmin(userId, 0, 20)).data,
  );

  return { signs: data?.items ?? [], isLoading, mutate };
};