import { useState } from 'react';
import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';
import styles from '../ui/ProfilePage.module.css';
import type { StudentProfileDto } from '@/entity/student';
import { StudentRanks } from '@/widgets/popups/student-ranks/ui/StudentRanks';

interface Props {
  /** null = not created yet, undefined = loading */
  isApproved: boolean | null | undefined;
  profile: StudentProfileDto | undefined;
  isLoading: boolean;
  isCreating: boolean;
  onCreate: () => void;
}

export const StudentCard = ({ isApproved, profile, isLoading, isCreating, onCreate }: Props) => {
  const [rankPopupOpen, setRankPopupOpen] = useState(false);

  const hasProfile = isApproved !== null && isApproved !== undefined;

  return (
    <div className={styles.card}>
      <h2 className={styles.cardTitle}>Профиль ученика</h2>

      {!hasProfile && (
        <>
          <p className={styles.cardDescription}>
            Создайте профиль ученика, чтобы записываться на курсы и отслеживать прогресс.
          </p>
          <Button
            variant={ButtonTypeEnum.PRIMARY}
            size={ButtonSizeEnum.NORMAL}
            onClick={onCreate}
            isLoading={isCreating}
          >
            Создать профиль ученика
          </Button>
        </>
      )}

      {hasProfile && (isLoading || !profile) && <div style={{ height: '4rem' }} />}

      {hasProfile && profile && (
        <>
          <div className={styles.stats}>
            {[
              { label: 'Уровень', value: profile.level, onClick: () => setRankPopupOpen(true) },
              { label: 'Ранг', value: profile.rank ?? '—', onClick: () => setRankPopupOpen(true) },
              { label: 'Рейтинг', value: profile.rating != null ? profile.rating.toFixed(1) : '—' },
              { label: 'Монеты', value: profile.coins },
              { label: 'Баланс', value: `${profile.cash.toFixed(2)} ₽` },
              { label: 'Опыт', value: profile.experience },
            ].map(({ label, value, onClick }) => (
              <div
                key={label}
                className={styles.stat}
                onClick={onClick}
                style={onClick ? { cursor: 'pointer' } : undefined}
              >
                <span className={styles.statLabel}>{label}</span>
                <span className={styles.statValue}>{value}</span>
              </div>
            ))}
          </div>
          {rankPopupOpen && <StudentRanks setRankPopupOpen={setRankPopupOpen} />}
        </>
      )}
    </div>
  );
};
