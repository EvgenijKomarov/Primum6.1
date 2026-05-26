import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router';

import { useStudentProfile } from '@/entity/student';
import { useTeacherProfile } from '@/entity/teacher';
import {
  createStudentProfile,
  createTeacherProfile,
  sendEmailVerification,
  useCurrentUser,
  useUserStore,
} from '@/entity/user';
import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';
import { Input } from '@/shared/ui/Input';

import styles from './ProfilePage.module.css';

const CheckIcon = () => (
  <svg viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
    <path
      fillRule="evenodd"
      d="M16.704 4.153a.75.75 0 0 1 .143 1.052l-8 10.5a.75.75 0 0 1-1.127.075l-4.5-4.5a.75.75 0 0 1 1.06-1.06l3.894 3.893 7.48-9.817a.75.75 0 0 1 1.05-.143Z"
      clipRule="evenodd"
    />
  </svg>
);

const EditIcon = () => (
  <svg viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
    <path d="M2.695 14.763l-1.262 3.154a.5.5 0 0 0 .65.65l3.155-1.262a4 4 0 0 0 1.343-.885L17.5 5.5a2.121 2.121 0 0 0-3-3L3.58 13.42a4 4 0 0 0-.885 1.343Z" />
  </svg>
);

export const ProfilePage = () => {
  const navigate = useNavigate();
  const clearStore = useUserStore((s) => s.clear);
  const { user, isLoading: userLoading, mutate: mutateUser } = useCurrentUser();

  const { studentProfile, isLoading: studentLoading } = useStudentProfile(
    user?.isApprovedStudent !== null,
  );
  const { teacherProfile, isLoading: teacherLoading } = useTeacherProfile(
    user?.isApprovedTeacher === true,
  );

  const [email, setEmail] = useState('');
  const [isEditingEmail, setIsEditingEmail] = useState(false);
  const [isSending, setIsSending] = useState(false);

  const [aboutTeacher, setAboutTeacher] = useState('');
  const [isCreatingStudent, setIsCreatingStudent] = useState(false);
  const [isCreatingTeacher, setIsCreatingTeacher] = useState(false);

  useEffect(() => {
    if (user?.email) setEmail(user.email);
  }, [user?.email]);

  const handleSendVerification = async () => {
    setIsSending(true);
    try {
      await sendEmailVerification({ correctiveMail: email || undefined });
    } finally {
      setIsSending(false);
    }
  };

  const handleCancelEdit = () => {
    setEmail(user?.email ?? '');
    setIsEditingEmail(false);
  };

  const handleCreateStudent = async () => {
    setIsCreatingStudent(true);
    try {
      await createStudentProfile();
      await mutateUser();
    } finally {
      setIsCreatingStudent(false);
    }
  };

  const handleLogout = async () => {
    clearStore();
    await mutateUser(undefined, { revalidate: false });
    navigate('/auth', { replace: true });
  };

  const handleCreateTeacher = async () => {
    setIsCreatingTeacher(true);
    try {
      await createTeacherProfile({ aboutTeacher });
      await mutateUser();
    } finally {
      setIsCreatingTeacher(false);
    }
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

  const emailConfirmed = user.mailConfirmed;
  const emailDisabled = emailConfirmed && !isEditingEmail;

  return (
    <div className={styles.page}>
      <div className={styles.container}>

        {/* ── Personal info ── */}
        <div className={styles.card}>
          <div className={styles.cardHeader}>
            <h2 className={styles.cardTitle}>Личные данные</h2>
            <Button
              variant={ButtonTypeEnum.TEXT}
              size={ButtonSizeEnum.SMALL}
              onClick={handleLogout}
            >
              Выйти
            </Button>
          </div>
          <div className={styles.fields}>
            <div className={styles.field}>
              <span className={styles.fieldLabel}>Фамилия</span>
              <span className={styles.fieldValue}>{user.surname ?? '—'}</span>
            </div>
            <div className={styles.field}>
              <span className={styles.fieldLabel}>Имя</span>
              <span className={styles.fieldValue}>{user.name ?? '—'}</span>
            </div>
            <div className={styles.field}>
              <span className={styles.fieldLabel}>Отчество</span>
              <span className={styles.fieldValue}>{user.patronymic ?? '—'}</span>
            </div>
          </div>
        </div>

        {/* ── Email ── */}
        <div className={styles.card}>
          <h2 className={styles.cardTitle}>Почта</h2>
          <div className={styles.emailSection}>
            <div className={styles.emailRow}>
              <div className={styles.emailInputWrapper}>
                <Input
                  value={email}
                  onChange={setEmail}
                  disabled={emailDisabled}
                  placeholder="Электронная почта"
                  type="email"
                />
                {emailConfirmed && !isEditingEmail && (
                  <span className={styles.checkmark}>
                    <CheckIcon />
                  </span>
                )}
              </div>

              {emailConfirmed && !isEditingEmail ? (
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
                    onClick={handleSendVerification}
                    isLoading={isSending}
                    disabled={!email.trim()}
                  >
                    Отправить код
                  </Button>
                  {emailConfirmed && isEditingEmail && (
                    <Button
                      variant={ButtonTypeEnum.SECONDARY}
                      size={ButtonSizeEnum.SMALL}
                      onClick={handleCancelEdit}
                    >
                      Отмена
                    </Button>
                  )}
                </div>
              )}
            </div>

            {!emailConfirmed && (
              <p className={styles.hint}>
                Почта не подтверждена. Введите адрес и отправьте код для подтверждения.
              </p>
            )}
          </div>
        </div>
        {emailConfirmed ? (
          <>
            {/* ── Student card ── */}
            {user.isApprovedStudent !== null ? (
              <div className={styles.card}>
                <h2 className={styles.cardTitle}>Профиль ученика</h2>
                {studentLoading || !studentProfile ? (
                  <div style={{ height: '4rem' }} />
                ) : (
                  <>
                    <div className={styles.stats}>
                      <div className={styles.stat}>
                        <span className={styles.statLabel}>Уровень</span>
                        <span className={styles.statValue}>{studentProfile.level}</span>
                      </div>
                      <div className={styles.stat}>
                        <span className={styles.statLabel}>Ранг</span>
                        <span className={styles.statValue}>{studentProfile.rank ?? '—'}</span>
                      </div>
                      <div className={styles.stat}>
                        <span className={styles.statLabel}>Рейтинг</span>
                        <span className={styles.statValue}>
                          {studentProfile.rating != null ? studentProfile.rating.toFixed(1) : '—'}
                        </span>
                      </div>
                      <div className={styles.stat}>
                        <span className={styles.statLabel}>Монеты</span>
                        <span className={styles.statValue}>{studentProfile.coins}</span>
                      </div>
                      <div className={styles.stat}>
                        <span className={styles.statLabel}>Баланс</span>
                        <span className={styles.statValue}>{studentProfile.cash.toFixed(2)} ₽</span>
                      </div>
                    </div>
                  </>
                )}
              </div>
            ) : (
              <div className={styles.card}>
                <h2 className={styles.cardTitle}>Профиль ученика</h2>
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
              </div>
            )}

            {/* ── Teacher card ── */}
          {(() => {
            switch (user.isApprovedTeacher) {
              case true:
                return (
                  <div className={styles.card}>
                  <h2 className={styles.cardTitle}>Профиль преподавателя</h2>
                  {teacherLoading || !teacherProfile ? (
                    <div style={{ height: '4rem' }} />
                  ) : (
                    <>
                      <span
                        className={`${styles.badge} ${
                          teacherProfile.isAvailable ? styles.badgeAvailable : styles.badgeUnavailable
                        }`}
                      >
                        <span className={styles.dot} />
                        {teacherProfile.isAvailable ? 'Доступен' : 'Недоступен'}
                      </span>
                      <div className={styles.stats}>
                        <div className={styles.stat}>
                          <span className={styles.statLabel}>Уровень</span>
                          <span className={styles.statValue}>{teacherProfile.level}</span>
                        </div>
                        <div className={styles.stat}>
                          <span className={styles.statLabel}>Ранг</span>
                          <span className={styles.statValue}>{teacherProfile.rank ?? '—'}</span>
                        </div>
                      </div>
                      {teacherProfile.about && (
                        <p className={styles.about}>{teacherProfile.about}</p>
                      )}
                    </>
                  )}
                </div>
                );

              case false:
                return (
                  <div className={styles.card}>
                    <h2 className={styles.cardTitle}>Профиль преподавателя</h2>
                    <p className={styles.warning}>
                      Пожалуйста, подождите. Ваш профиль преподавателя находится на рассмотрении. Обычно это занимает от нескольких часов до пары дней.
                    </p>
                  </div>
                );

              case null:
                return (
                  <div className={styles.card}>
                  <h2 className={styles.cardTitle}>Профиль преподавателя</h2>
                  <p className={styles.cardDescription}>
                    Создайте профиль преподавателя, чтобы вести курсы и работать с учениками.
                  </p>
                  <textarea
                    className={styles.textarea}
                    value={aboutTeacher}
                    onChange={(e) => setAboutTeacher(e.target.value)}
                    placeholder="Расскажите о себе: опыт, специализация, подход к обучению…"
                  />
                  <Button
                    variant={ButtonTypeEnum.PRIMARY}
                    size={ButtonSizeEnum.NORMAL}
                    onClick={handleCreateTeacher}
                    isLoading={isCreatingTeacher}
                    disabled={!aboutTeacher.trim()}
                  >
                    Создать профиль преподавателя
                  </Button>
                </div>
                );

              default:
                return null;
            }
          })()}

            
          </>
        ) : (
          <div className={styles.card}>
            <p className={styles.warning}>
              Подтвердите почту, чтобы получить доступ к созданию профилей ученика и преподавателя.
            </p>
          </div>
        )}
      </div>
    </div>
  );
};
