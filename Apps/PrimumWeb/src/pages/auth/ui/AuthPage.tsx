import { useState } from "react";
import { LoginForm } from "@/features/login";
import { RegisterForm } from "@/features/register";

export const AuthPage = () => {
  const [isLogin, setIsLogin] = useState(true);

  return isLogin ? (
    <LoginForm onSwitch={() => setIsLogin(false)} />
  ) : (
    <RegisterForm onSwitch={() => setIsLogin(true)} />
  );
}
