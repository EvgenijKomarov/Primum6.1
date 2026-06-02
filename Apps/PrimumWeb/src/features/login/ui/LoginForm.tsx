import styles from "./styles.module.css";
import { Controller, FormProvider, useForm } from "react-hook-form";
import { login, type LoginDto } from "@/entity/auth";
import { useFetch } from "@/shared/api/useFetch.ts";
import { Input } from "@/shared/ui/Input";
import { useUserStore } from "@/entity/user";
import Button from "@/shared/ui/Button/Button.tsx";
import { ButtonTypeEnum } from "@/shared/enums";
import { translateException } from "@/features/exception-translation/translate-exception";

interface LoginFormProps {
  onSwitch: () => void;
  onSuccess?: () => void;
  onMutate?: () => void;
}

export const LoginForm = ({ onSwitch, onSuccess, onMutate }: LoginFormProps) => {
  const form = useForm<LoginDto>();
  const setToken = useUserStore((s) => s.setToken);

  const { fetch: fetchLogin, isLoading } = useFetch(login);

  const onSubmit = form.handleSubmit(async (data) => {
    try{
      const response = await fetchLogin(data);
      setToken(response.data);
      await onMutate?.();
      onSuccess?.();
    } catch (error) {
      form.setError('root', {
        message: error instanceof Error
          ? error.message
          : 'Произошла ошибка при входе',
      });
    }
  });

  const handleSwitch = () => {
    form.reset();
    onSwitch();
  };

  const topError = form.formState.errors.root?.message;

  return (
    <FormProvider {...form}>
      <form onSubmit={onSubmit}>
        {topError && (
          <div className={styles.formError}>
            {translateException(topError)}
          </div>
        )}
        <div className={styles.formRows}>
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
        </div>
      </form>
    </FormProvider>
  );
};