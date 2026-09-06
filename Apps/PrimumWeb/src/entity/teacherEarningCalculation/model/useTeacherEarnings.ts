import { api } from "@/shared/config/api";
import useSWRImmutable from "swr/immutable";
import { getTeacherEarning } from "../api/teacherEarningCalculator.api";


export const useTeacherEarnings = () => {
  const { data, isLoading, mutate } = useSWRImmutable(
    api.teacherEarnings.base,
    async () => (await getTeacherEarning()).data,
    { revalidateOnMount: true },
  );

  return { teacherEarnings: data, isLoading, mutate };
};