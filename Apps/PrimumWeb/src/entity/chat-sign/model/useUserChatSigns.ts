import { api } from "@/shared/config/api";
import useSWR from "swr";
import { getChatSigns } from "../api/chat-sign.api";

export const useUserChatSigns = (enabled = true) => {
  const { data, isLoading, mutate } = useSWR(
    enabled ? [api.user.chatSigns] : null,
    async () => (await getChatSigns(0, 500)).data,
  );

  return { signs: data?.items ?? [], isLoading, mutate };
};