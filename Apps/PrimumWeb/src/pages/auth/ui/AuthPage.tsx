import { useState } from "react";
import styles from "./styles.module.css";
import { useNavigate } from "react-router";
import { LoginForm } from "@/features/login";
import { RegisterForm } from "@/features/register";
import { useCurrentUser } from "@/entity/user/model/useCurrentUser";

export const AuthPage = () => {
  const [isLogin, setIsLogin] = useState(true);
  const navigate = useNavigate();
  const { mutate } = useCurrentUser();

  const handleSuccess = () => navigate('/profile', { replace: true });

  return (
    <div className={styles.page}>
      <div className={styles.card}>
        {isLogin ? (
          <LoginForm onSwitch={() => setIsLogin(false)} onSuccess={handleSuccess} onMutate={mutate} />
        ) : (
          <RegisterForm onSwitch={() => setIsLogin(true)} onSuccess={handleSuccess} onMutate={mutate} />
        )}
      </div>
    </div>
  );
}
