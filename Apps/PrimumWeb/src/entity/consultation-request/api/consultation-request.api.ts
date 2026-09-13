import { fetcherInstance } from "@/shared/api/axios";
import { api } from "@/shared/config/api";
import type { ConsultationRequestInput } from "../model/types";


export const requestConsultation = async (data: ConsultationRequestInput) => {
  return await fetcherInstance<number>({
    method: 'POST',
    url: api.publicConsultation.base,
    headers: { 'Content-Type': 'application/json' },
    data,
  });
};