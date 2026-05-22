import styles from "./styles.module.css";
import { Controller, FormProvider, useForm } from "react-hook-form";
import { login, type LoginDto } from "@/entity/auth";
import { useFetch } from "@/shared/api/useFetch.ts";
import { Input } from "@/shared/ui/Input";
import { useUserStore } from "@/entity/user";
import Button from "@/shared/ui/Button/Button.tsx";
import { ButtonTypeEnum } from "@/shared/enums";

interface LoginFormProps {
  onSwitch: () => void;
  onSuccess?: () => void;
}

export const LoginForm = ({ onSwitch, onSuccess }: LoginFormProps) => {
  const form = useForm<LoginDto>();
  const setToken = useUserStore((s) => s.setToken);

  const { fetch: fetchLogin, isLoading } = useFetch(login);

  const onSubmit = form.handleSubmit(async (data) => {
    const response = await fetchLogin(data);
    setToken(response.data);
    onSuccess?.();
  });

  const handleSwitch = () => {
    form.reset();
    onSwitch();
  };

  return (
    <FormProvider {...form}>
      <form onSubmit={onSubmit}>
        <div className={styles.formRow}>
          <div className={styles.formCol}>
            <Controller
              name={"email"}
              control={form.control}
              render={({ field }) => (
                <Input
                  {...field}
                  label={"Электронная почта"}
                  placeholder={"Введите адрес электронной почты"}
                />
              )}
            />
          </div>
        </div>
        <div className={styles.formRow}>
          <div className={styles.formCol}>
            <Controller
              name={"password"}
              control={form.control}
              render={({ field }) => (
                <Input
                  {...field}
                  type={"password"}
                  label={"Пароль"}
                  placeholder={"Введите пароль"}
                />
              )}
            />
          </div>
        </div>
        <div className={styles.formActions}>
          <Button type="submit" isLoading={isLoading}>
            Войти
          </Button>
          <Button variant={ButtonTypeEnum.SECONDARY} onClick={handleSwitch}>
            Зарегистрироваться
          </Button>
        </div>
      </form>
    </FormProvider>
  );
};