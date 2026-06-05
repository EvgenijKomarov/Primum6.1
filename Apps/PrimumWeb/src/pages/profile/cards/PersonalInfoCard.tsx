import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';
import styles from '../ui/ProfilePage.module.css';
import { EnsurancePopup } from '@/widgets/popups/ensurance-popup/ui/EnsurancePopup';
import { useState } from 'react';
import { Card } from '@/shared/ui/Card/Card';

interface Props {
  surname: string | null | undefined;
  name: string | null | undefined;
  patronymic: string | null | undefined;
  onLogout: () => void;
}

export const PersonalInfoCard = ({ surname, name, patronymic, onLogout }: Props) => {
  const [ensurancePopupOpen, setEnsurancePopupOpen] = useState(false);
  
  return (<Card title="Личные данные"  width={40}>
    <div className={styles.fields}>
      {[
        { label: 'Фамилия', value: surname },
        { label: 'Имя', value: name },
        { label: 'Отчество', value: patronymic },
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
