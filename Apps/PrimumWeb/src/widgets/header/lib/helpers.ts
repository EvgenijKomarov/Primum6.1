import { Role } from "@/shared/enums";

export const resolveDisplayName = (user: { displayName?: string | null; name?: string | null; surname?: string | null }): string => {
  if (user.displayName) return user.displayName;
  const parts = [user.name, user.surname].filter(Boolean);
  return parts.length > 0 ? parts.join(' ') : 'Profile';
};

export const resolveRoleLabel = (role: Role): string => {
  const labels: Record<Role, string> = {
    [Role.ADMIN]: 'Администратор',
    [Role.TEACHER]: 'Преподаватель',
    [Role.STUDENT]: 'Студент',
    [Role.GUEST]: 'Гость',
  };
  return labels[role] ?? role;
};