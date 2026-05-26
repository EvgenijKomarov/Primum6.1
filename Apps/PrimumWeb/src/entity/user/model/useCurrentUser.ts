import useSWRImmutable from 'swr/immutable';
import { getUserInfo } from '@/entity/user';
import { api } from '@/shared/config/api.ts';
import { Role } from '@/shared/enums';
import { useCallback, useMemo, useState } from 'react';

const ACTIVE_ROLE_KEY = 'activeRole';

const resolveRoles = (user: { isAdmin?: boolean | null; isApprovedTeacher?: boolean | null; isApprovedStudent?: boolean | null } | undefined): Role[] => {
  if (!user) return [Role.GUEST];

  const roles: Role[] = [];
  if (user.isAdmin) roles.push(Role.ADMIN);
  if (user.isApprovedTeacher) roles.push(Role.TEACHER);
  if (user.isApprovedStudent !== null && user.isApprovedStudent !== undefined) roles.push(Role.STUDENT);

  return roles.length > 0 ? roles : [Role.GUEST];
};

const getStoredRole = (): Role => {
  return localStorage.getItem(ACTIVE_ROLE_KEY) as Role | null || Role.GUEST;
};

export const useCurrentUser = () => {
  const {
    data: user,
    isLoading,
    mutate,
  } = useSWRImmutable(
    [api.user.getUserInfo],
    async () => {
      const response = await getUserInfo();
      return response.data;
    },
    { revalidateOnMount: true }
  );

  const availableRoles = useMemo(() => resolveRoles(user), [user]);

  const [activeRole, setActiveRoleState] = useState<Role>(getStoredRole);

  const role = useMemo(() => {
    if (availableRoles.includes(activeRole)) return activeRole;
    return availableRoles[0];
  }, [availableRoles, activeRole]);

  const setActiveRole = useCallback((newRole: Role) => {
    localStorage.setItem(ACTIVE_ROLE_KEY, newRole);
    setActiveRoleState(newRole);
  }, []);

  return {
    user,
    role,
    availableRoles,
    setActiveRole,
    isLoading,
    mutate,
  };
};