import { fetcherInstance } from "@/shared/api/axios";
import { api } from "@/shared/config/api";
import type { CourseThemeDtoPageResult, CourseThemeInputDto } from "../model/types";



export const getCourseThemes = async (page = 0, pageSize = 100) => {
    return await fetcherInstance<CourseThemeDtoPageResult>({
        method: 'GET',
        url: api.adminThemes.base,
        params: { page, pageSize },
    });
};

export const createCourseTheme = async (data: CourseThemeInputDto) => {
    return await fetcherInstance({
        method: 'POST',
        url: api.adminThemes.base,
        headers: { 'Content-Type': 'application/json' },
        data,
    });
};

export const editCourseTheme = async (themeId: number, data: CourseThemeInputDto) => {
    return await fetcherInstance({
        method: 'PATCH',
        url: `${api.adminThemes.base}/${themeId}`,
        headers: { 'Content-Type': 'application/json' },
        data,
    });
}