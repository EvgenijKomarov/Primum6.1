import { useLocation } from 'react-router';

import { AppRouter } from '@/app/providers/AppRouter.tsx';
import { Header } from '@/widgets/header';

function App() {
  const location = useLocation();
  const showHeader = location.pathname !== '/auth';

  return (
    <>
      {showHeader && <Header />}
      <AppRouter />
    </>
  );
}

export default App;