import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';
import styles from '../ui/ProfilePage.module.css';
import { topupStudentBallance, useStudentBalance, useStudentProfile } from '@/entity/student';
import { Card } from '@/shared/ui/Card/Card';
import { StatCard } from '@/shared/ui/StatCard/StatCard';
import { StudentRankInfo } from '@/widgets/popups/rank-info/student-rank-info/StudentRankInfo';
import { CoinIcon } from '@/shared/icons/types';
import { useState } from 'react';
import { Popup } from '@/shared/ui/Popup';
import { Input } from '@/shared/ui/Input';
import { createStudentProfile, type UserDto } from '@/entity/user';
import { useToast } from '@/shared/ui/Toast/useToast';

interface Props {
  /** null = not created yet, undefined = loading */
  user: UserDto;
  mutateUser: () => void;
}

export const StudentCard = ({ user, mutateUser }: Props) => {
  const { showToast } = useToast();

  const handleTopupRequest = async (amount: number) => {
    const url = (await topupStudentBallance(amount)).data;
    if (url) { window.open(url, '_blank'); }
  }
  const { studentProfile, isLoading: studentLoading } = useStudentProfile(
    user?.isApprovedStudent !== null &&
      user?.isApprovedStudent !== undefined &&
      user?.isAvailable === true,
  );
  const handleCreateStudent = async () => {
      setIsCreatingStudent(true);
      try {
        await createStudentProfile();
        await mutateUser();
        showToast('Профиль создан', 'success')
      } finally {
        setIsCreatingStudent(false);
      }
    };
  
  const [isCreatingStudent, setIsCreatingStudent] = useState(false);
  const [topupPopupOpen, setTopupPopupOpen] = useState(false);
  const [topupAmount, setTopupAmount] = useState(100);
  const hasProfile = user?.isApprovedStudent !== null && user?.isApprovedStudent !== undefined;

  const { studentBalance } = useStudentBalance();

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
            onClick={handleCreateStudent}
            isLoading={isCreatingStudent}
          >
            Создать профиль ученика
          </Button>
        </>
      )}

      {hasProfile && (studentLoading || !studentProfile) && <div style={{ height: '4rem' }} />}

      {hasProfile && studentProfile && (
        <>
          <div className={styles.stats}>
            {[
              { label: 'Уровень', value: studentProfile.level },
              { label: 'Ранг', value: <StudentRankInfo rankInput={studentProfile.rank} /> },
              { label: 'Рейтинг', value: studentProfile.rating != null ? studentProfile.rating.toFixed(1) : '—' },
              { label: 'Монеты', value: studentProfile.coins },
              { label: 'Баланс', value: `${studentBalance ? studentBalance.toFixed(2) : '--'} ₽` },
              { label: 'Опыт', value: studentProfile.experience },
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
                    onClick={() => { handleTopupRequest(topupAmount); }}
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
