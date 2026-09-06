import { api } from "@/shared/config/api";
import useSWR from "swr";
import { getPromocodes } from "../api/promocodesByAdmin.api";


export const usePromocodes = (searchQuery: string, page = 0, pageSize = 20) => {
    const {data, isLoading, mutate} = useSWR([api.promocodes.byAdmin, searchQuery], async () => {
        return (await getPromocodes(searchQuery, page, pageSize)).data;
    });

    return { promocodes: data?.items ?? [], isLoading, mutate };
}