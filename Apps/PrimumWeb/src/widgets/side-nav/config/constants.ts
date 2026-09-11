import { Role } from "@/shared/enums/auth.ts";

interface NavItem {
  label: string;
  path: string;
}

export const NAV_ITEMS: Record<Role, NavItem[]> = {
  [Role.ADMIN]: [
    {
      label: "Инциденты",
      path: "/incidents",
    },
    {
      label: "Пользователи",
      path: "/users",
    },
    {
      label: "Платформа",
      path: "/platform-config",
    }
  ],
  [Role.TEACHER]: [
    {
      label: "Мои курсы",
      path: "/courses",
    },
    {
      label: "Расписание",
      path: "/schedule",
    },
    {
      label: "Мои занятия",
      path: "/teacher-lessons",
    },
  ],
  [Role.STUDENT]: [
    {
      label: "Доступные курсы",
      path: "/catalog",
    },
    {
      label: "Мои абонементы",
      path: "/student-abonements",
    },
    {
      label: "Промокоды",
      path: "/promocodes"
    },
    {
      label: "Мои занятия",
      path: "/student-lessons",
    },
  ],
  [Role.GUEST]: [],
};
