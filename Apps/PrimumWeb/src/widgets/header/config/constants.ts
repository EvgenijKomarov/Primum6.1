import { Role } from "@/shared/enums/auth.ts";

interface NavItem {
  label: string;
  path: string;
}

export const NAV_ITEMS: Record<Role, NavItem[]> = {
  [Role.ADMIN]: [],
  [Role.TEACHER]: [
    {
      label: "Мои курсы",
      path: "/courses",
    },
    {
      label: "Расписание",
      path: "/schedule",
    },
  ],
  [Role.STUDENT]: [
    {
      label: "Доступные курсы",
      path: "/catalog",
    }
  ],
  [Role.GUEST]: [],
};
