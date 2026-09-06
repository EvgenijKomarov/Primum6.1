import { fetcherInstance } from "@/shared/api/axios";
import type { IncidentLogDtoPageResult } from "../model/types";
import { api } from "@/shared/config/api";



export const getIncidentLogs = async (onlyUnrevisioned: boolean, adminUserId?: number, page = 0, pageSize = 50) => {
  return await fetcherInstance<IncidentLogDtoPageResult>({
    method: 'GET',
    url: api.incidents.incidentLogs,
    params: { onlyUnrevisioned, adminUserId, page, pageSize },
  });
};

export const reviseIncidentLog = async (logId: number) => {
  return await fetcherInstance<IncidentLogDtoPageResult>({
    method: 'PATCH',
    url: `${api.incidents.incidentLogs}/${logId}`
  });
};