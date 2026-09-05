import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router';

import { topupStudentBallance, useStudentProfile } from '@/entity/student';
import { useTeacherProfile } from '@/entity/teacher';
import {
  createStudentProfile,
  sendEmailVerification,
  useCurrentUser,
  useUserStore,
  confirmEmail
} from '@/entity/user';

import styles from './ProfilePage.module.css';
import { confirmChatSign } from '@/entity/chat-sign/api/chat-sign.api';
import { useChatSigns } from '@/entity/chat-sign/model/useUserChatSigns';
import { PersonalInfoCard } from '../cards/PersonalInfoCard';
import { EmailCard } from '../cards/EmailCard';
import { ChatBotsCard } from '../cards/ChatBotsCard';
import { StudentCard } from '../cards/StudentCard';
import { TeacherCard } from '../cards/TeacherCard';
import { Card } from '@/shared/ui/Card/Card';
import { useToast } from '@/shared/ui/Toast/useToast';

export const ProfilePage = () => {
  const navigate = useNavigate();
  const clearStore = useUserStore((s) => s.clear);
  const { showToast } = useToast();

  const { user, isLoading: userLoading, mutate: mutateUser } = useCurrentUser();
  const { signs: chatSigns, mutate: mutateChatSigns } = useChatSigns(
    user?.mailConfirmed === true,
  );
  const { studentProfile, isLoading: studentLoading } = useStudentProfile(
    user?.isApprovedStudent !== null &&
      user?.isApprovedStudent !== undefined &&
      user?.isAvailable === true,
  );
  const { teacherProfile, isLoading: teacherLoading } = useTeacherProfile(
    user?.isApprovedTeacher === true &&
      user?.isApprovedStudent !== undefined &&
      user?.isAvailable === true,
  );

  const [email, setEmail] = useState('');
  const [emailToken, setEmailToken] = useState('');
  const [chatSignToken, setChatSignToken] = useState('');
  const [isEditingEmail, setIsEditingEmail] = useState(false);
  const [isSending, setIsSending] = useState(false);
  const [isCreatingStudent, setIsCreatingStudent] = useState(false);

  useEffect(() => {
    if (user?.email) setEmail(user.email);
  }, [user?.email]);

  const handleSendVerification = async () => {
    setIsSending(true);
    try {
      await sendEmailVerification({ correctiveMail: email || undefined });
      showToast('Письмо отправлено', 'success')
    } finally {
      setIsSending(false);
      await mutateUser();
    }
  };

  const handleConfirmEmail = async () => {
    await confirmEmail({ token: emailToken });
    await mutateUser();
    setIsEditingEmail(false);
    setEmailToken('');
    showToast('Почта подтверждена', 'success')
  };

  const handleConfirmSign = async () => {
    await confirmChatSign(chatSignToken);
    await mutateChatSigns();
    setChatSignToken('');
    showToast('Аккаунт успешно привязан', 'success')
  };

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

  const handleTopupRequest = async (amount: number) => {
    const url = (await topupStudentBallance(amount)).data;
    if (url) { window.open(url, '_blank'); }
  }

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
            surname={user.surname}
            name={user.name}
            patronymic={user.patronymic}
            onLogout={handleLogout}
          />
          <EmailCard
            email={email}
            emailToken={emailToken}
            isConfirmed={user.mailConfirmed}
            isEditing={isEditingEmail}
            isSending={isSending}
            onEmailChange={setEmail}
            onEmailTokenChange={setEmailToken}
            onSendVerification={handleSendVerification}
            onConfirmEmail={handleConfirmEmail}
            onStartEditing={() => setIsEditingEmail(true)}
            onCancelEditing={() => {
              setEmail(user.email ?? '');
              setIsEditingEmail(false);
            }}
          />
          {user.mailConfirmed && <ChatBotsCard
                chatSigns={chatSigns}
                chatSignToken={chatSignToken}
                onTokenChange={setChatSignToken}
                onConfirmSign={handleConfirmSign}
                mutateChatSigns={mutateChatSigns}
              />}
        </div>

        {user.mailConfirmed ? (
          <>
            <div className={styles.containerColumn}>
              <StudentCard
                isApproved={user.isApprovedStudent}
                profile={studentProfile}
                isLoading={studentLoading}
                isCreating={isCreatingStudent}
                onCreate={handleCreateStudent}
                onRequestTopup={handleTopupRequest}
              />

              <TeacherCard
                isApproved={user.isApprovedTeacher}
                profile={teacherProfile}
                isLoading={teacherLoading}
              />
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

