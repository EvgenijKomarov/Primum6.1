import { register, type RegisterDto } from "@/entity/auth";
import { useFetch } from "@/shared/api/useFetch.ts";
import { Controller, FormProvider, useForm } from "react-hook-form";
import styles from "./styles.module.css";
import { Input } from "@/shared/ui/Input";
import { sendEmailVerification, useUserStore } from "@/entity/user";
import Button from "@/shared/ui/Button/Button.tsx";
import { ButtonTypeEnum } from "@/shared/enums";
import { translateException } from "@/features/exception-translation/translate-exception";
import { EMAIL_PATTERN, emailInputProps, normalizeEmail } from "@/shared/lib/email/email";

// Совпадает с UserIterator.MinPasswordLength / MaxPasswordLength на бэкенде
const MIN_PASSWORD_LENGTH = 8;
const MAX_PASSWORD_LENGTH = 64;

interface RegisterFormProps {
  onSwitch: () => void;
  onSuccess?: () => void;
  onMutate?: () => void;
}

type RegisterForm = RegisterDto & {confirmPassword: string};

export const RegisterForm = ({ onSwitch, onSuccess, onMutate }: RegisterFormProps) => {
  const form = useForm<RegisterForm>({
    defaultValues: { name: '', surname: '', patronymic: '', mailAdress: '', password: '', confirmPassword: '' },
  });
  const setToken = useUserStore((s) => s.setToken);

  const { fetch: fetchRegister, isLoading } = useFetch(register);

const onSubmit = form.handleSubmit(async (data: RegisterForm) => {
  try {
    const response = await fetchRegister({
      ...data,
      name: data.name.trim(),
      surname: data.surname.trim(),
      patronymic: data.patronymic.trim(),
      mailAdress: normalizeEmail(data.mailAdress),
      timeZoneOffset: -(new Date().getTimezoneOffset()),
    });
    setToken(response.data);
    onSuccess?.();
    await onMutate?.();
    await sendEmailVerification({ correctiveMail: undefined });
  } catch (error) {
    form.setError('root', {
      message: error instanceof Error
        ? error.message
        : 'Произошла ошибка при регистрации',
    });
  }
});

  const handleSwitch = () => {
    form.reset();
    onSwitch();
  };

  const { errors } = form.formState;
  const topError = errors.root?.message;

  return (
    <FormProvider {...form}>
      <form onSubmit={onSubmit} noValidate>
        {topError && (
          <div className={styles.formError} role="alert">
            {translateException(topError)}
          </div>
        )}
        <div className={styles.formRows}>
          <div className={styles.formRow}>
            <div className={styles.formCol}>
              <Controller
                name={"name"}
                control={form.control}
                rules={{ validate: (value) => value.trim() !== '' || 'Введите имя' }}
                render={({ field }) => (
                  <Input
                    {...field}
                    autoComplete="given-name"
                    label={"Имя"}
                    placeholder={"Введите ваше имя"}
                    error={errors.name?.message}
                  />
                )}
              />
            </div>
          </div>
          <div className={styles.formRow}>
            <div className={styles.formCol}>
              <Controller
                name={"surname"}
                control={form.control}
                rules={{ validate: (value) => value.trim() !== '' || 'Введите фамилию' }}
                render={({ field }) => (
                  <Input
                    {...field}
                    autoComplete="family-name"
                    label={"Фамилия"}
                    placeholder={"Введите вашу фамилию"}
                    error={errors.surname?.message}
                  />
                )}
              />
            </div>
          </div>
          <div className={styles.formRow}>
            <div className={styles.formCol}>
              <Controller
                name={"patronymic"}
                control={form.control}
                render={({ field }) => (
                  <Input
                    {...field}
                    autoComplete="additional-name"
                    label={"Отчество (если есть)"}
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
                rules={{
                  required: 'Введите адрес электронной почты',
                  validate: (value) => EMAIL_PATTERN.test(normalizeEmail(value)) || 'Некорректный адрес почты',
                }}
                render={({ field }) => (
                  <Input
                    {...field}
                    {...emailInputProps}
                    autoComplete="email"
                    label={"Электронная почта"}
                    placeholder={"Введите адрес электронной почты"}
                    error={errors.mailAdress?.message}
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
                rules={{
                  required: 'Введите пароль',
                  minLength: { value: MIN_PASSWORD_LENGTH, message: `Минимум ${MIN_PASSWORD_LENGTH} символов` },
                  maxLength: { value: MAX_PASSWORD_LENGTH, message: `Максимум ${MAX_PASSWORD_LENGTH} символа` },
                }}
                render={({ field }) => (
                  <Input
                    {...field}
                    type={"password"}
                    autoComplete="new-password"
                    label={"Пароль"}
                    placeholder={`Не короче ${MIN_PASSWORD_LENGTH} символов`}
                    error={errors.password?.message}
                  />
                )}
              />
            </div>
          </div>
          <div className={styles.formRow}>
            <div className={styles.formCol}>
              <Controller
                name={"confirmPassword"}
                control={form.control}
                rules={{
                  validate: (value) =>
                    value === form.getValues('password') || 'Пароли не совпадают',
                }}
                render={({ field }) => (
                  <Input
                    {...field}
                    type={"password"}
                    autoComplete="new-password"
                    label={"Подтверждение пароля"}
                    placeholder={"Введите пароль еще раз"}
                    error={errors.confirmPassword?.message}
                  />
                )}
              />
            </div>
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
