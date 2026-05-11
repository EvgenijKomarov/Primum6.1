import type { ReactNode } from 'react';
import { AuthPage } from '@/pages/auth/ui/AuthPage.tsx';

interface RouteDef {
  path: string;
  element: ReactNode;
  roles?: string[];
}

interface PrivateRoute extends RouteDef {
  id: number;
}

const ROUTES_DEF: RouteDef[] = [
  {
    path: 'auth',
    element: <AuthPage />,
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
