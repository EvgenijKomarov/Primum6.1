import { useState } from 'react';
import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';
import { TeacherRanks } from '@/widgets/popups/teacher-ranks/ui/TeacherRanks';
import styles from '../ui/ProfilePage.module.css';
import type { TeacherProfileDto } from '@/entity/teacher';
import { Badge } from '@/shared/ui/Badge/Badge';
import { BadgeTypeEnum } from '@/shared/enums/badge';
import { Card } from '@/shared/ui/Card/Card';
import { StatCard } from '@/shared/ui/StatCard/StatCard';

interface Props {
  /** true = approved, false = pending, null = not created, undefined = not a teacher */
  isApproved: boolean | null | undefined;
  profile: TeacherProfileDto | undefined;
  isLoading: boolean;
  isCreating: boolean;
  aboutTeacher: string;
  onAboutChange: (v: string) => void;
  onCreate: () => void;
}

export const TeacherCard = ({
  isApproved,
  profile,
  isLoading,
  isCreating,
  aboutTeacher,
  onAboutChange,
  onCreate,
}: Props) => {
  const [rankPopupOpen, setRankPopupOpen] = useState(false);

  if (isApproved === undefined) return null;

  return (
    <Card title="Профиль преподавателя">

      {/* Pending approval */}
      {isApproved === false && (
        <p className={styles.warning}>
          Пожалуйста, подождите. Ваш профиль преподавателя находится на рассмотрении. Обычно это
          занимает от нескольких часов до пары дней.
        </p>
      )}

      {/* Not created yet */}
      {isApproved === null && (
        <>
          <p className={styles.cardDescription}>
            Создайте профиль преподавателя, чтобы вести курсы и работать с учениками.
          </p>
          <textarea
            className={styles.textarea}
            value={aboutTeacher}
            onChange={(e) => onAboutChange(e.target.value)}
            placeholder="Расскажите о себе: опыт, специализация, подход к обучению…"
          />
          <Button
            variant={ButtonTypeEnum.PRIMARY}
            size={ButtonSizeEnum.NORMAL}
            onClick={onCreate}
            isLoading={isCreating}
            disabled={!aboutTeacher.trim()}
          >
            Создать профиль преподавателя
          </Button>
        </>
      )}

      {/* Approved — loading */}
      {isApproved === true && (isLoading || !profile) && <div style={{ height: '4rem' }} />}

      {/* Approved — loaded */}
      {isApproved === true && profile && (
        <>
          {profile.isAvailable === true ? (
            <Badge text="Доступен" badgeType={BadgeTypeEnum.Positive} />
          ): (
            <Badge text="Недоступен" badgeType={BadgeTypeEnum.Negative} />
          )}

          <div className={styles.stats}>
            {[
              { label: 'Уровень', value: profile.level, onClick: () => setRankPopupOpen(true) },
              { label: 'Ранг', value: profile.rank ?? '—', onClick: () => setRankPopupOpen(true) },
              { label: 'Опыт', value: profile.experience },
            ].map(({ label, value }) => (
              <StatCard
                key={label}
                title={label}
                value={value}
              />
            ))}
          </div>

          {rankPopupOpen && <TeacherRanks setRankPopupOpen={setRankPopupOpen} />}
          {profile.about && <p className={styles.about}>{profile.about}</p>}
        </>
      )}
    </Card>
  );
};
