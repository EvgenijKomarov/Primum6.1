import type { ReactNode } from 'react';
import { AuthPage } from '@/pages/auth/ui/AuthPage.tsx';
import { CatalogPage } from '@/pages/catalog/index.ts';
import { CoursesPage } from '@/pages/courses/index.ts';
import { SchedulePage } from '@/pages/schedule/index.ts';
import { HomePage } from "@/pages/home/ui/HomePage.tsx";
import { ProfilePage } from "@/pages/profile/index.ts";
import { Role } from '@/shared/enums';
import { RedirectPage } from '@/pages/redirect-page/RedirectPage';
import { StudentLessonsPage } from '@/pages/studentLessons';
import { StudentAbonementPage } from '@/pages/studentAbonements/ui/StudentAbonementPage';
import { PromocodesCatalogPage } from '@/pages/promocodesCatalog/ui/PromocodesCatalogPage';

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
  {
    path: '/courses',
    element: <CoursesPage />,
    roles: [Role.TEACHER],
  },
  {
    path: '/schedule',
    element: <SchedulePage />,
    roles: [Role.TEACHER],
  },
  {
    path: '/catalog',
    element: <CatalogPage />,
    roles: [Role.STUDENT],
  },
  {
    path: '/student-lessons',
    element: <StudentLessonsPage />,
    roles: [Role.STUDENT],
  },
  {
    path: '/student-abonements',
    element: <StudentAbonementPage />,
    roles: [Role.STUDENT],
  },
  {
    path: '/promocodes',
    element: <PromocodesCatalogPage />,
    roles: [Role.STUDENT],
  },
  {
    path: '/confirm-email',
    element: <RedirectPage apiUrl="/user/confirm-email" redirectTo="/profile" />,
  },
  {
    path: '/confirm-chat',
    element: <RedirectPage apiUrl="/user/chat-signs" redirectTo="/profile" />,
  }
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
