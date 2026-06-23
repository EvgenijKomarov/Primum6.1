import { api } from "@/shared/config/api";
import useSWRImmutable from "swr/immutable";
import { getPublicThemes } from "../api/course-theme.api";

export const usePublicThemes = () =>
  useSWRImmutable(
    [api.publicTheme.getThemes],
    async () => (await getPublicThemes()).data,
    { revalidateOnMount: true },
  );