import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';
import { Input } from '@/shared/ui/Input';
import styles from '../ui/ProfilePage.module.css';
import { CheckIcon, EditIcon } from '@/shared/icons';
import { Collapsible } from '@/shared/ui/Collapsible/Collapsible';
import { useEffect, useState } from 'react';
import { EnsurancePopup } from '@/widgets/popups/ensurance-popup/ui/EnsurancePopup';
import { Card } from '@/shared/ui/Card/Card';
import { confirmEmail, sendEmailVerification, type UserDto } from '@/entity/user';
import { useToast } from '@/shared/ui/Toast/useToast';

interface Props {
  user: UserDto;
  mutateUser: () => void;
}

export const EmailCard = ({ user, mutateUser }: Props) => {
  const [ensurancePopupOpen, setEnsurancePopupOpen] = useState(false);
  const [email, setEmail] = useState('');
  const [emailToken, setEmailToken] = useState('');
  const [isSending, setIsSending] = useState(false);
  const [isEditingEmail, setIsEditingEmail] = useState(false);
  const { showToast } = useToast();

  useEffect(() => {
      if (user?.email) setEmail(user.email);
    }, [user?.email]);

  const inputDisabled = user.mailConfirmed && isEditingEmail;

  const handleSendVerification = async () => {
      setIsSending(true);
      try {
        await sendEmailVerification({ correctiveMail: email || undefined });
        showToast('Письмо отправлено', 'success')
      } finally {
        setIsSending(false);
        await mutateUser();
      }
    };
  
    const handleConfirmEmail = async () => {
      await confirmEmail({ token: emailToken });
      await mutateUser();
      setIsEditingEmail(false);
      setEmailToken('');
      showToast('Почта подтверждена', 'success')
    };
  
  return (
    <Card title="Почта"  width={'40rem'}>
      <div className={styles.emailSection}>

        {/* Email input row */}
        <div className={styles.emailRow}>
          <div className={styles.emailInputWrapper}>
            <Input
              value={email}
              onChange={setEmail}
              disabled={inputDisabled}
              placeholder="Электронная почта"
              type="email"
            />
            {user.mailConfirmed && !isEditingEmail && (
              <span className={styles.checkmark}>
                <CheckIcon />
              </span>
            )}
          </div>

          {user.mailConfirmed && !isEditingEmail ? (
            <Button
              variant={ButtonTypeEnum.SECONDARY}
              size={ButtonSizeEnum.SMALL}
              icon={<EditIcon />}
              onClick={() => setIsEditingEmail(true)}
            >
              Изменить
            </Button>
          ) : (
            <div className={styles.emailActions}>
              <Button
                variant={ButtonTypeEnum.PRIMARY}
                size={ButtonSizeEnum.SMALL}
                onClick={() => {
                  if (user.mailConfirmed) {setEnsurancePopupOpen(true);}
                  else {handleSendVerification();}
                }}
                isLoading={isSending}
                disabled={!email.trim()}
              >
                Отправить код
              </Button>
              {ensurancePopupOpen && <EnsurancePopup
                      setPopupOpen={setEnsurancePopupOpen}
                      onConfirm={handleSendVerification}
                      description="При отправке кода, ваш аккаунт станет неподтвержденным и невидимым для системы до тех пор, пока вы снова не подтведите почту. Вы уверены, что хотите отправить код?"
                    />}
              {user.mailConfirmed && isEditingEmail && (
                <Button
                  variant={ButtonTypeEnum.SECONDARY}
                  size={ButtonSizeEnum.SMALL}
                  onClick={() => setIsEditingEmail(false)}
                >
                  Отмена
                </Button>
              )}
            </div>
          )}
        </div>

        {!user.mailConfirmed && (
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
                onChange={setEmailToken}
                placeholder="Код подтверждения"
                type="emailToken"
              />
            </div>
            <Button
              variant={ButtonTypeEnum.PRIMARY}
              size={ButtonSizeEnum.SMALL}
              onClick={() => handleConfirmEmail()}
            >
              Подтвердить
            </Button>
          </div>
        </Collapsible>
      </div>
    </Card>
  );
};
