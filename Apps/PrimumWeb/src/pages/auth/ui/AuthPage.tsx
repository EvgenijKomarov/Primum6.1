import { useState } from "react";
import styles from "./styles.module.css";
import { useNavigate } from "react-router";
import { LoginForm } from "@/features/login";
import { RegisterForm } from "@/features/register";
import { useCurrentUser } from "@/entity/user/model/useCurrentUser";
import { Card } from "@/shared/ui/Card/Card";

export const AuthPage = () => {
  const [isLogin, setIsLogin] = useState(true);
  const navigate = useNavigate();
  const { mutate } = useCurrentUser();

  const handleSuccess = () => navigate('/profile', { replace: true });

  return (
    <div className={styles.page}>
      <Card width={'40rem'}>
        {isLogin ? (
          <LoginForm onSwitch={() => setIsLogin(false)} onSuccess={handleSuccess} onMutate={mutate} />
        ) : (
          <RegisterForm onSwitch={() => setIsLogin(true)} onSuccess={handleSuccess} onMutate={mutate} />
        )}
      </Card>
    </div>
  );
}
