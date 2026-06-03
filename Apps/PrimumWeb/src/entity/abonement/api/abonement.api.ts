import { fetcherInstance } from "@/shared/api/axios";
import type { AbonementDto } from "../model/types";
import { api } from "@/shared/config/api";


export const getTeacherAbonementById = async (id: number) => {
    return await fetcherInstance<AbonementDto>({
        method: 'GET',
        url: `${api.abonement.getByTeacher}/${id}`,
    });
};