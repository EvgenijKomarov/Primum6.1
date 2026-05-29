import { fetcherInstance } from "@/shared/api/axios";
import { api } from "@/shared/config/api";
import type { ChatSignPageResult } from "../model/types";


export const getChatSigns = async (page = 0, pageSize = 50) => {
  return await fetcherInstance<ChatSignPageResult>({
    method: 'GET',
    url: api.user.chatSigns,
    params: { page, pageSize },
  });
};

export const confirmChatSign = async (token: string) => {
  return await fetcherInstance({
    method: 'POST',
    url: api.user.chatSigns,
    headers: {
      'Content-Type': 'application/json',
    },
    data: JSON.stringify(token),
  });
};