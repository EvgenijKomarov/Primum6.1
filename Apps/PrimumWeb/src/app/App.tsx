import { AppRouter } from '@/app/providers/AppRouter.tsx';
import { ToastProvider } from '@/shared/ui/Toast/ToastContext';
import { Header } from '@/widgets/header';
import { ModalRoot } from '@/widgets/modal';

function App() {
  return (
    <>
      <Header />
      <ToastProvider>
        <AppRouter />
        <ModalRoot />
      </ToastProvider>
    </>
  );
}

export default App;