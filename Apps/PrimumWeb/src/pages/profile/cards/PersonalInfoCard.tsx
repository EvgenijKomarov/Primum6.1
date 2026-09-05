import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';
import styles from '../ui/ProfilePage.module.css';
import { EnsurancePopup } from '@/widgets/popups/ensurance-popup/ui/EnsurancePopup';
import { useState } from 'react';
import { Card } from '@/shared/ui/Card/Card';
import type { UserDto } from '@/entity/user';

interface Props {
  user: UserDto;
  onLogout: () => void;
}

export const PersonalInfoCard = ({ user, onLogout }: Props) => {
  const [ensurancePopupOpen, setEnsurancePopupOpen] = useState(false);
  
  return (<Card title="Личные данные"  width={'40rem'}>
    <div className={styles.fields}>
      {[
        { label: 'Фамилия', value: user.surname },
        { label: 'Имя', value: user.name },
        { label: 'Отчество', value: user.patronymic },
      ].map(({ label, value }) => (
        <div key={label} className={styles.field}>
          <span className={styles.fieldLabel}>{label}</span>
          <span className={styles.fieldValue}>{value ?? '—'}</span>
        </div>
      ))}
    </div>
    <Button variant={ButtonTypeEnum.SECONDARY} size={ButtonSizeEnum.SMALL} onClick={() => setEnsurancePopupOpen(true)}>
        Выйти
      </Button>
      {ensurancePopupOpen && <EnsurancePopup 
        setPopupOpen={setEnsurancePopupOpen}
        onConfirm={onLogout}
        description="Вы уверены, что хотите выйти из аккаунта?"
      />}
  </Card>
)};
