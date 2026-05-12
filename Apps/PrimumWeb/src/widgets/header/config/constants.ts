import { Role } from "@/shared/enums/auth.ts";

interface NavItem {
  label: string;
  path: string;
}

export const NAV_ITEMS: Record<Role, NavItem[]> = {
  [Role.ADMIN]: [],
  [Role.TEACHER]: [],
  [Role.STUDENT]: [],
  [Role.GUEST]: [],
};
