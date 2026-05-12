import { ROUTES } from "@/app/config/router.tsx";
import { Route, Routes } from "react-router";

export const AppRouter = () => {
  return (
    <Routes>
      {ROUTES.map((route) => {
        // TODO: Role check
        if (route.roles) return (
          <Route
            key={route.id}
            path={route.path}
            element={route.element}
          />
        );

        return (
          <Route
            key={route.id}
            path={route.path}
            element={route.element}
          />
        )
      })}
    </Routes>
  )
}