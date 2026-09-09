import { api } from "@/shared/config/api";
import useSWRImmutable from "swr/immutable";
import { getTeacherFreeTime } from "../api/teacher.api";


export const useTeacherFreeTime = (teacherId: number) => {
  const { data, isLoading, mutate } = useSWRImmutable(
    [`${api.publicTeacher.getSchedules}/${teacherId}/free-time`],
    async () => (await getTeacherFreeTime(teacherId)).data,
    { revalidateOnMount: true },
  );

  return { teacherFreeTime: data ?? [], isLoading, mutate };
};