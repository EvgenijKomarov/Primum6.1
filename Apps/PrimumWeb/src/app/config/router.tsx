import type { ReactNode } from 'react';
import { AuthPage } from '@/pages/auth/ui/AuthPage.tsx';
import { HomePage } from "@/pages/home/ui/HomePage.tsx";
import { ProfilePage } from "@/pages/profile/index.ts";
import type { Role } from '@/shared/enums';

interface RouteDef {
  path: string;
  element: ReactNode;
  roles?: Role[];
}

interface PrivateRoute extends RouteDef {
  id: number;
}

const ROUTES_DEF: RouteDef[] = [
  {
    path: 'auth',
    element: <AuthPage />,
  },
  {
    path: '/',
    element: <HomePage />,
  },
  {
    path: '/profile',
    element: <ProfilePage />,
  },
];

const makeRoutes = (routeDefs: RouteDef[]): PrivateRoute[] => {
  let idx = 1000;

  return routeDefs.map((x) => {
    return {
      id: idx++,
      ...x
    };
  });
};

export const ROUTES = makeRoutes(ROUTES_DEF);
