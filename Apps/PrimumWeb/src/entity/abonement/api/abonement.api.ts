import { fetcherInstance } from "@/shared/api/axios";
import type { AbonementDto, AbonementDtoPageResult, AbonementInputStatus } from "../model/types";
import { api } from "@/shared/config/api";
import type { LessonsByDateDtoPageResult } from "@/entity/lesson";
import type { AbonementScheduleDtoPageResult } from "@/entity/schedule";


export const getTeacherAbonementById = async (id: number) => {
    return await fetcherInstance<AbonementDto>({
        method: 'GET',
        url: `${api.teacherAbonement.getById}/${id}`,
    });
};

export const getStudentAbonementsAsync = async (page = 0, pageSize = 20) => {
    return await fetcherInstance<AbonementDtoPageResult>({
        method: 'GET',
        url: `${api.studentAbonement.base}`,
        params: { page, pageSize },
    });
}

export const getStudentAbonementAsync = async (id: number) => {
    return await fetcherInstance<AbonementDto>({
        method: 'GET',
        url: `${api.studentAbonement.base}/${id}`,
    });
}

export const GetStudentAbonementSchedulesAsync = async (id: number, page = 0, pageSize = 100) => {
    return await fetcherInstance<AbonementScheduleDtoPageResult>({
        method: 'GET',
        url: `${api.studentAbonement.base}/${id}/shedules`,
        params: { page, pageSize },
    });
}

export const GetStudentAbonementLessonsAsync = async (id: number, page = 0, pageSize = 100) => {
    return await fetcherInstance<LessonsByDateDtoPageResult>({
        method: 'GET',
        url: `${api.studentAbonement.base}/${id}/lessons`,
        params: { page, pageSize },
    });
}

export const StudentAbonementChangeStatusAsync = async (id: number, data: AbonementInputStatus ) => {
    return await fetcherInstance<number>({
        method: 'PATCH',
        url: `${api.studentAbonement.base}/${id}/status`,
        headers: { 'Content-Type': 'application/json' },
        data
    });
}

export const DeleteStudentAbonementAsync = async (id: number) => {
    return await fetcherInstance<number>({
        method: 'DELETE',
        url: `${api.studentAbonement.base}/${id}`,
    });
}