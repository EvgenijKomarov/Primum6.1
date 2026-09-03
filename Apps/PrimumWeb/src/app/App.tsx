import { AppRouter } from '@/app/providers/AppRouter.tsx';
import { CurrentUserProvider } from '@/app/providers/CurrentUserProvider';
import { ToastProvider } from '@/shared/ui/Toast/ToastContext';
import { Header } from '@/widgets/header';
import { ModalRoot } from '@/widgets/modal';

function App() {
  return (
    <CurrentUserProvider>
      <Header />
      <ToastProvider>
        <AppRouter />
        <ModalRoot />
      </ToastProvider>
    </CurrentUserProvider>
  );
}

export default App;