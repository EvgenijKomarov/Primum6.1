import { useState } from "react";
import styles from "./styles.module.css";
import { useNavigate } from "react-router";
import { LoginForm } from "@/features/login";
import { RegisterForm } from "@/features/register";

export const AuthPage = () => {
  const [isLogin, setIsLogin] = useState(true);
  const navigate = useNavigate();

  const handleSuccess = () => navigate('/profile', { replace: true });

  return (
    <div className={styles.page}>
      <div className={styles.card}>
        {isLogin ? (
          <LoginForm onSwitch={() => setIsLogin(false)} onSuccess={handleSuccess} />
        ) : (
          <RegisterForm onSwitch={() => setIsLogin(true)} onSuccess={handleSuccess} />
        )}
      </div>
    </div>
  );
}
