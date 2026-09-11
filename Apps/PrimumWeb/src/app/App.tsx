import { AppRouter } from '@/app/providers/AppRouter.tsx';
import { CurrentUserProvider } from '@/app/providers/CurrentUserProvider';
import { PageContainer } from '@/shared/ui/PageContainer/PageContainer';
import { ToastProvider } from '@/shared/ui/Toast/ToastContext';
import { Header } from '@/widgets/header';
import { ModalRoot } from '@/widgets/modal';
import { SideNav } from '@/widgets/side-nav/ui/SideNav';
import { useState } from 'react';

function App() {
  const [sideOpen, setSideOpen] = useState(false);

  return (
    <CurrentUserProvider>
      <Header setIsOpen={setSideOpen} isOpen={sideOpen}/>
      <SideNav isOpen={sideOpen} setIsOpen={setSideOpen}/>
      <ToastProvider>
        <PageContainer>
          <AppRouter />
        </PageContainer>
        <ModalRoot />
      </ToastProvider>
    </CurrentUserProvider>
  );
}

export default App;