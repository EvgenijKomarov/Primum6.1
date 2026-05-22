import { useLocation } from 'react-router';

import { AppRouter } from '@/app/providers/AppRouter.tsx';
import { Header } from '@/widgets/header';
import { ModalRoot } from '@/widgets/modal';

function App() {
  const location = useLocation();
  const showHeader = location.pathname !== '/auth';

  return (
    <>
      {showHeader && <Header />}
      <AppRouter />
      <ModalRoot />
    </>
  );
}

export default App;