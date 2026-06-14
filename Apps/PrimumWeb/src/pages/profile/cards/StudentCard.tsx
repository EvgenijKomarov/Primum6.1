import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';
import styles from '../ui/ProfilePage.module.css';
import type { StudentProfileDto } from '@/entity/student';
import { Card } from '@/shared/ui/Card/Card';
import { StatCard } from '@/shared/ui/StatCard/StatCard';
import { StudentRankInfo } from '@/widgets/popups/rank-info/student-rank-info/StudentRankInfo';
import { CoinIcon } from '@/shared/icons/types';
import { useState } from 'react';
import { Popup } from '@/shared/ui/Popup';
import { Input } from '@/shared/ui/Input';

interface Props {
  /** null = not created yet, undefined = loading */
  isApproved: boolean | null | undefined;
  profile: StudentProfileDto | undefined;
  isLoading: boolean;
  isCreating: boolean;
  onCreate: () => void;
  onRequestTopup: (amount: number) => void;
}

export const StudentCard = ({ isApproved, profile, isLoading, isCreating, onCreate, onRequestTopup }: Props) => {
  const [topupPopupOpen, setTopupPopupOpen] = useState(false);
  const [topupAmount, setTopupAmount] = useState(100);
  const hasProfile = isApproved !== null && isApproved !== undefined;

  return (
    <Card title="Профиль ученика"  width={'40rem'}>

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
          <div className={styles.buttons}>
            <Button
              variant={ButtonTypeEnum.PRIMARY}
              size={ButtonSizeEnum.SMALL}
              icon={<CoinIcon />}
              onClick={() => {setTopupPopupOpen(true)}}
            >
              Пополнить балланс
            </Button>
            {topupPopupOpen && 
              <Popup 
                onClose={() => {setTopupPopupOpen(false)}}
                title={'Пополнить балланс'}
              >
                <div className={styles.balancePopup}>
                  <Input 
                    value={topupAmount}
                    onChange={(val) => {setTopupAmount(Number(val))}}
                    placeholder="Введите сумму"
                  />
                  <Button
                    variant={ButtonTypeEnum.PRIMARY}
                    size={ButtonSizeEnum.SMALL}
                    icon={<CoinIcon />}
                    onClick={() => { onRequestTopup(topupAmount); }}
                  >
                    Пополнить
                  </Button>
                </div>
              </Popup>}
          </div>
        </>
      )}
    </Card>
  );
};
