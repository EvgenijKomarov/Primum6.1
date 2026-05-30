import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';
import styles from '../ui/ProfilePage.module.css';

interface Props {
  surname: string | null | undefined;
  name: string | null | undefined;
  patronymic: string | null | undefined;
  onLogout: () => void;
}

export const PersonalInfoCard = ({ surname, name, patronymic, onLogout }: Props) => (
  <div className={styles.card}>
    <div className={styles.cardHeader}>
      <h2 className={styles.cardTitle}>Личные данные</h2>
      <Button variant={ButtonTypeEnum.TEXT} size={ButtonSizeEnum.SMALL} onClick={onLogout}>
        Выйти
      </Button>
    </div>
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
  </div>
);
