import { api } from "@/shared/config/api";
import useSWRImmutable from "swr/immutable";
import { getPublicThemes } from "../api/course-theme.api";
import { getCourseThemes } from "../api/course-theme.byAdmin";
import useSWR from "swr";

export const usePublicThemes = () =>
  useSWRImmutable(
    [api.publicTheme.getThemes],
    async () => (await getPublicThemes()).data,
    { revalidateOnMount: true },
  );

export const useThemesByAdmin = () => {
  const {data, isLoading, mutate} = useSWR([api.adminThemes.base],
    async () => (await getCourseThemes()).data
  );
  return { themes: data?.items ?? [], isLoading, mutate };
}