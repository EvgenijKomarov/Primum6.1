import { api } from "@/shared/config/api"
import { getAvailablePromocodes, getStudentPromocodes } from "../api/promocode.api";
import useSWR from "swr";


export const usePromocodes = (onlyBought: boolean, page = 0, pageSize = 20) => {
    const key = onlyBought ? [api.promocodes.student] : [api.promocodes.available];

    const {data, isLoading, mutate} = useSWR(key, async () => {
        const res = onlyBought ? getStudentPromocodes(page, pageSize) : getAvailablePromocodes(page, pageSize);
        return (await res).data;
    });

    return { promocodes: data?.items ?? [], isLoading, mutate };
}