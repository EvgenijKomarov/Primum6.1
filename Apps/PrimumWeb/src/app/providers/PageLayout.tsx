import { PageContainer } from "@/shared/ui/PageContainer/PageContainer";
import { Outlet } from "react-router";


export function PageLayout() {
  return (
    <PageContainer>
      <Outlet />
    </PageContainer>
  );
}