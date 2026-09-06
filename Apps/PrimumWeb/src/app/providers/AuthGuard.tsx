import type { PropsWithChildren } from 'react';
import { Navigate } from 'react-router';

import { useCurrentUser, useUserStore } from '@/entity/user';
import type { Role } from '@/shared/enums';
import { Loader } from '@/shared/ui/Loader';

type AuthGuardProps = PropsWithChildren<{
  roles?: Role[];
}>;

export const AuthGuard = ({ children, roles = [] }: AuthGuardProps) => {
  const { token } = useUserStore();
  const { user, role, isLoading } = useCurrentUser();

  if (!token) return <Navigate to="/auth" replace />;
  if (isLoading) return <Loader />;
  if (!user) return <Navigate to="/auth" replace />;
  if (roles.length > 0 && !roles.includes(role)) return <Navigate to="/" replace />;

  return <>{children}</>;
};