import { fetcherInstance } from "@/shared/api/axios";
import type { IncidentDecisionInputDto, IncidentDtoPageResult } from "../model/types";
import { api } from "@/shared/config/api";


export const getIncidents = async (page = 0, pageSize = 500) => {
  return await fetcherInstance<IncidentDtoPageResult>({
    method: 'GET',
    url: api.incidents.commonIncidents,
    params: { page, pageSize },
  });
};

export const solveIncident = async (data: IncidentDecisionInputDto) => {
  return await fetcherInstance<number>({
    method: 'PATCH',
    url: api.incidents.commonIncidents,
    data,
  });
};