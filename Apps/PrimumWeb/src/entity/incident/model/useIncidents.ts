import { api } from "@/shared/config/api";
import useSWR from "swr";
import { getIncidents } from "../api/incidents.api";


export const useIncidents = () => {
    const { data, isLoading, mutate } = useSWR(
    [api.incidents.commonIncidents],
    async () => (await getIncidents()).data,
  );

  return { incidents: data?.items ?? [], isLoading, mutate };
}