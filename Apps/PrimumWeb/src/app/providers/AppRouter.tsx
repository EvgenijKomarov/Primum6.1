import { Route, Routes } from 'react-router';

import { ROUTES } from '@/app/config/router.tsx';

import { AuthGuard } from './AuthGuard.tsx';

export const AppRouter = () => {
  return (
    <Routes>
      {ROUTES.map((route) => (
        <Route
          key={route.id}
          path={route.path}
          element={
            route.roles !== undefined
              ? <AuthGuard roles={route.roles}>{route.element}</AuthGuard>
              : route.element
          }
        />
      ))}
    </Routes>
  );
};