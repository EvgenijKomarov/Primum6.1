import useSWRImmutable from 'swr/immutable';
import { getUserInfo } from '@/entity/user';
import { api } from '@/shared/config/api.ts';
import { Role } from '@/shared/enums';
import { useMemo } from 'react';

const resolveRole = (user: { isAdmin?: boolean | null; isApprovedTeacher?: boolean | null; isApprovedStudent?: boolean | null } | undefined): Role => {
  if (!user) return Role.GUEST;

  if (user.isAdmin) return Role.ADMIN;
  if (user.isApprovedTeacher) return Role.TEACHER;
  if (user.isApprovedStudent) return Role.STUDENT;
  return Role.GUEST;
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

  const role = useMemo(() => {
    return resolveRole(user);
  }, [user]);

  return {
    user,
    role,
    isLoading,
    mutate,
  };
}