import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';
import { Input } from '@/shared/ui/Input';
import styles from '../ui/ProfilePage.module.css';
import { CheckIcon, EditIcon } from '@/shared/icons';
import { Collapsible } from '@/shared/ui/Collapsible/Collapsible';
import { useState } from 'react';
import { EnsurancePopup } from '@/widgets/popups/ensurance-popup/ui/EnsurancePopup';
import { Card } from '@/shared/ui/Card/Card';

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
  const [ensurancePopupOpen, setEnsurancePopupOpen] = useState(false);
  
  return (
    <Card title="Почта" className={styles.card}>
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
                onClick={() => setEnsurancePopupOpen(true)}
                isLoading={isSending}
                disabled={!email.trim()}
              >
                Отправить код
              </Button>
              {ensurancePopupOpen && <EnsurancePopup
                      setPopupOpen={setEnsurancePopupOpen}
                      onConfirm={onSendVerification}
                      description="При отправке кода, ваш аккаунт станет неподтвержденным и невидимым для системы до тех пор, пока вы снова не подтведите почту. Вы уверены, что хотите отправить код?"
                    />}
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
        <Collapsible title="Ручное подтверждение почты">
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
              Подтвердить
            </Button>
          </div>
        </Collapsible>
      </div>
    </Card>
  );
};
