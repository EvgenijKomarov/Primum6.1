import { Route, Routes } from 'react-router';

import { ROUTES, type PrivateRoute } from '@/app/config/router.tsx';

import { AuthGuard } from './AuthGuard.tsx';
import { PageLayout } from './PageLayout.tsx';

export const AppRouter = () => {
  const layoutRoutes = ROUTES.filter((r) => !r.noLayout);
  const plainRoutes = ROUTES.filter((r) => r.noLayout);

  const renderRoute = (route: PrivateRoute) => (
    <Route
      key={route.id}
      path={route.path}
      element={
        route.roles !== undefined
          ? <AuthGuard roles={route.roles}>{route.element}</AuthGuard>
          : route.element
      }
    />
  );

  return (
    <Routes>
      <Route element={<PageLayout />}>
        {layoutRoutes.map(renderRoute)}
      </Route>
      {plainRoutes.map(renderRoute)}
    </Routes>
  );
};