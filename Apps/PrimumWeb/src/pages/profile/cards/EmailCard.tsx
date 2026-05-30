import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';
import { Input } from '@/shared/ui/Input';
import styles from '../ui/ProfilePage.module.css';

const CheckIcon = () => (
  <svg viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
    <path
      fillRule="evenodd"
      d="M16.704 4.153a.75.75 0 0 1 .143 1.052l-8 10.5a.75.75 0 0 1-1.127.075l-4.5-4.5a.75.75 0 0 1 1.06-1.06l3.894 3.893 7.48-9.817a.75.75 0 0 1 1.05-.143Z"
      clipRule="evenodd"
    />
  </svg>
);

const EditIcon = () => (
  <svg viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
    <path d="M2.695 14.763l-1.262 3.154a.5.5 0 0 0 .65.65l3.155-1.262a4 4 0 0 0 1.343-.885L17.5 5.5a2.121 2.121 0 0 0-3-3L3.58 13.42a4 4 0 0 0-.885 1.343Z" />
  </svg>
);

interface Props {
  email: string;
  emailToken: string;
  isConfirmed: boolean;
  isEditing: boolean;
  isSending: boolean;
  onEmailChange: (v: string) => void;
  onEmailTokenChange: (v: string) => void;
  onSendVerification: () => void;
  onConfirmEmail: () => void;
  onStartEditing: () => void;
  onCancelEditing: () => void;
}

export const EmailCard = ({
  email,
  emailToken,
  isConfirmed,
  isEditing,
  isSending,
  onEmailChange,
  onEmailTokenChange,
  onSendVerification,
  onConfirmEmail,
  onStartEditing,
  onCancelEditing,
}: Props) => {
  const inputDisabled = isConfirmed && !isEditing;

  return (
    <div className={styles.card}>
      <h2 className={styles.cardTitle}>Почта</h2>
      <div className={styles.emailSection}>

        {/* Email input row */}
        <div className={styles.emailRow}>
          <div className={styles.emailInputWrapper}>
            <Input
              value={email}
              onChange={onEmailChange}
              disabled={inputDisabled}
              placeholder="Электронная почта"
              type="email"
            />
            {isConfirmed && !isEditing && (
              <span className={styles.checkmark}>
                <CheckIcon />
              </span>
            )}
          </div>

          {isConfirmed && !isEditing ? (
            <Button
              variant={ButtonTypeEnum.SECONDARY}
              size={ButtonSizeEnum.SMALL}
              icon={<EditIcon />}
              onClick={onStartEditing}
            >
              Изменить
            </Button>
          ) : (
            <div className={styles.emailActions}>
              <Button
                variant={ButtonTypeEnum.PRIMARY}
                size={ButtonSizeEnum.SMALL}
                onClick={onSendVerification}
                isLoading={isSending}
                disabled={!email.trim()}
              >
                Отправить код
              </Button>
              {isConfirmed && isEditing && (
                <Button
                  variant={ButtonTypeEnum.SECONDARY}
                  size={ButtonSizeEnum.SMALL}
                  onClick={onCancelEditing}
                >
                  Отмена
                </Button>
              )}
            </div>
          )}
        </div>

        {!isConfirmed && (
          <p className={styles.hint}>
            Почта не подтверждена. Введите адрес и отправьте код для подтверждения.
          </p>
        )}

        {/* Confirmation token row */}
        <h2 className={styles.cardSubtitle}>Подтверждение почты</h2>
        <div className={styles.emailRow}>
          <div className={styles.emailInputWrapper}>
            <Input
              value={emailToken}
              onChange={onEmailTokenChange}
              placeholder="Код подтверждения"
              type="emailToken"
            />
          </div>
          <Button
            variant={ButtonTypeEnum.PRIMARY}
            size={ButtonSizeEnum.SMALL}
            onClick={onConfirmEmail}
          >
            Подтвердить код
          </Button>
        </div>

      </div>
    </div>
  );
};
