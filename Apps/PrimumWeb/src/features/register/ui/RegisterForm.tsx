import { register, type RegisterDto } from "@/entity/auth";
import { useFetch } from "@/shared/api/useFetch.ts";
import { Controller, FormProvider, useForm } from "react-hook-form";
import styles from "./styles.module.css";
import { Input } from "@/shared/ui/Input";
import { useUserStore } from "@/entity/user";
import Button from "@/shared/ui/Button/Button.tsx";
import { ButtonTypeEnum } from "@/shared/enums";

interface RegisterFormProps {
  onSwitch: () => void;
}

export const RegisterForm = ({ onSwitch }: RegisterFormProps) => {
  const form = useForm<RegisterDto>();
  const setToken = useUserStore((s) => s.setToken);

  const { fetch: fetchRegister, isLoading } = useFetch(register);

  const onSubmit = form.handleSubmit(async (data) => {
    const response = await fetchRegister(data);
    setToken(response.data);
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
              name={"name"}
              control={form.control}
              render={({ field }) => (
                <Input
                  {...field}
                  label={"Имя"}
                  placeholder={"Введите ваше имя"}
                />
              )}
            />
          </div>
          <div className={styles.formCol}>
            <Controller
              name={"surname"}
              control={form.control}
              render={({ field }) => (
                <Input
                  {...field}
                  label={"Фамилия"}
                  placeholder={"Введите вашу фамилию"}
                />
              )}
            />
          </div>
          <div className={styles.formCol}>
            <Controller
              name={"patronymic"}
              control={form.control}
              render={({ field }) => (
                <Input
                  {...field}
                  label={"Отчество"}
                  placeholder={"Введите ваше отчество"}
                />
              )}
            />
          </div>
        </div>
        <div className={styles.formRow}>
          <div className={styles.formCol}>
            <Controller
              name={"mailAdress"}
              control={form.control}
              render={({ field }) => (
                <Input
                  {...field}
                  label={"Email"}
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
            Зарегистрироваться
          </Button>
          <Button variant={ButtonTypeEnum.SECONDARY} onClick={handleSwitch}>
            Войти
          </Button>
        </div>
      </form>
    </FormProvider>
  );
};