import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';
import styles from '../ui/ProfilePage.module.css';
import type { StudentProfileDto } from '@/entity/student';
import { Card } from '@/shared/ui/Card/Card';
import { StatCard } from '@/shared/ui/StatCard/StatCard';
import { StudentRankInfo } from '@/widgets/popups/rank-info/student-rank-info/StudentRankInfo';

interface Props {
  /** null = not created yet, undefined = loading */
  isApproved: boolean | null | undefined;
  profile: StudentProfileDto | undefined;
  isLoading: boolean;
  isCreating: boolean;
  onCreate: () => void;
}

export const StudentCard = ({ isApproved, profile, isLoading, isCreating, onCreate }: Props) => {

  const hasProfile = isApproved !== null && isApproved !== undefined;

  return (
    <Card title="Профиль ученика" className={styles.card}>

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
              { label: 'Уровень', value: profile.level },
              { label: 'Ранг', value: <StudentRankInfo rankInput={profile.rank} /> },
              { label: 'Рейтинг', value: profile.rating != null ? profile.rating.toFixed(1) : '—' },
              { label: 'Монеты', value: profile.coins },
              { label: 'Баланс', value: `${profile.cash.toFixed(2)} ₽` },
              { label: 'Опыт', value: profile.experience },
            ].map(({ label, value }) => (
              <StatCard
                key={label}
                title={label}
                value={value}
              />
            ))}
          </div>
        </>
      )}
    </Card>
  );
};
