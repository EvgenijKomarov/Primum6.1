import { useNavigate } from 'react-router';
import {
  useCurrentUser,
  useUserStore
} from '@/entity/user';
import styles from './ProfilePage.module.css';
import { PersonalInfoCard } from '../cards/PersonalInfoCard';
import { EmailCard } from '../cards/EmailCard';
import { ChatBotsCard } from '../cards/ChatBotsCard';
import { StudentCard } from '../cards/StudentCard';
import { TeacherCard } from '../cards/TeacherCard';
import { Card } from '@/shared/ui/Card/Card';

export const ProfilePage = () => {
  const navigate = useNavigate();
  const clearStore = useUserStore((s) => s.clear);

  const { user, isLoading: userLoading, mutate: mutateUser } = useCurrentUser();

  const handleLogout = async () => {
    clearStore();
    await mutateUser();
    navigate('/auth', { replace: true });
  };

  if (userLoading) {
    return (
      <div className={styles.page}>
        <div className={styles.container}>
          <div className={styles.card} style={{ height: '9rem' }} />
          <div className={styles.card} style={{ height: '7rem' }} />
        </div>
      </div>
    );
  }

  if (!user) return null;

  return (
    <div className={styles.page}>
      <div className={styles.container}>
        <div className={styles.containerColumn}>
          <PersonalInfoCard
            user={user}
            onLogout={handleLogout}
          />
          <EmailCard user={user} mutateUser={mutateUser}/>
          {user.mailConfirmed && <ChatBotsCard user={user}/>}
        </div>

        {user.mailConfirmed ? (
          <>
            <div className={styles.containerColumn}>
              <StudentCard user={user} mutateUser={mutateUser}/>
              <TeacherCard user={user} mutateUser={mutateUser}/>
            </div>
          </>
        ) : (
          <Card width={'40rem'}>
            <p className={styles.warning}>
              Подтвердите почту, чтобы получить доступ к созданию профилей ученика и преподавателя.
            </p>
          </Card>
        )}

      </div>
    </div>
  );
};

