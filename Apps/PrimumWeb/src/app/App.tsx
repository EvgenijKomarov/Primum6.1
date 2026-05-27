import { AppRouter } from '@/app/providers/AppRouter.tsx';
import { Header } from '@/widgets/header';
import { ModalRoot } from '@/widgets/modal';

function App() {
  return (
    <>
      <Header />
      <AppRouter />
      <ModalRoot />
    </>
  );
}

export default App;