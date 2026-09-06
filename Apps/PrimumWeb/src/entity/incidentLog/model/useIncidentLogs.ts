import { api } from "@/shared/config/api";
import useSWR from "swr";
import { getIncidentLogs } from "../api/incidentLogs.api";



export const useIncidentLogs = (onlyRevisioned: boolean, adminUserId?: number) => {
    const { data, isLoading, mutate } = useSWR(
    [api.incidents.commonIncidents, onlyRevisioned, adminUserId],
    async () => (await getIncidentLogs(onlyRevisioned, adminUserId)).data,
  );

  return { logs: data?.items ?? [], isLoading, mutate };
}