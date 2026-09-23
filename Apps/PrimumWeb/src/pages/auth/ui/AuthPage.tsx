import styles from "./styles.module.css";
import { Navigate, useNavigate, useSearchParams } from "react-router";
import { LoginForm } from "@/features/login";
import { RegisterForm } from "@/features/register";
import { useCurrentUser } from "@/entity/user/model/useCurrentUser";
import { Card } from "@/shared/ui/Card/Card";

const REGISTER_MODE = 'register';

export const AuthPage = () => {
  // Режим в адресе, чтобы на регистрацию можно было дать прямую ссылку: /auth?mode=register
  const [searchParams, setSearchParams] = useSearchParams();
  const isLogin = searchParams.get('mode') !== REGISTER_MODE;
  const navigate = useNavigate();
  const { user, mutate } = useCurrentUser();

  const handleSuccess = () => navigate('/profile', { replace: true });
  const setMode = (mode?: string) => setSearchParams(mode ? { mode } : {}, { replace: true });

  // Уже вошедшему пользователю форма входа не нужна
  if (user) return <Navigate to="/profile" replace />;

  return (
    <div className={styles.page}>
      <Card width={'40rem'}>
        {isLogin ? (
          <LoginForm onSwitch={() => setMode(REGISTER_MODE)} onSuccess={handleSuccess} onMutate={mutate} />
        ) : (
          <RegisterForm onSwitch={() => setMode()} onSuccess={handleSuccess} onMutate={mutate} />
        )}
      </Card>
    </div>
  );
}
