import { AppRouter } from '@/app/providers/AppRouter.tsx';
import { CurrentUserProvider } from '@/app/providers/CurrentUserProvider';
import { PageContainer } from '@/shared/ui/PageContainer/PageContainer';
import { ToastProvider } from '@/shared/ui/Toast/ToastContext';
import { Header } from '@/widgets/header';
import { ModalRoot } from '@/widgets/modal';

function App() {
  return (
    <CurrentUserProvider>
      <Header />
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