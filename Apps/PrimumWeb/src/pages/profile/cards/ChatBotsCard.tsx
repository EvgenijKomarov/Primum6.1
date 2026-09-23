import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';
import { Input } from '@/shared/ui/Input';
import type { ChatSign } from '@/entity/chat-sign/model/types';
import styles from '../ui/ProfilePage.module.css';
import { Collapsible } from '@/shared/ui/Collapsible';
import { Card } from '@/shared/ui/Card/Card';
import { StatCard } from '@/shared/ui/StatCard/StatCard';
import { confirmChatSign, deleteChatSign } from '@/entity/chat-sign/api/chat-sign.api';
import { useState } from 'react';
import { EnsurancePopup } from '@/widgets/popups/ensurance-popup/ui/EnsurancePopup';
import { useChatSigns } from '@/entity/chat-sign/model/useUserChatSigns';
import type { UserDto } from '@/entity/user';
import { useToast } from '@/shared/ui/Toast/useToast';
import { botLinks } from '@/shared/config/runtime';

const Sign = ({ sign, onDelete }: { sign: ChatSign; onDelete: () => void }) => {
  const [deletePopupOpen, setDeletePopupOpen] = useState(false);

  return(
  <>
    <StatCard
      title={sign.realizationTag}
      value={sign.username ?? sign.chatId}
      onDelete={async () => setDeletePopupOpen(true)}/>
    {deletePopupOpen && (
      <EnsurancePopup
        description={`Вы уверены, что хотите отвязать профиль от чата бота в ${sign.realizationTag}?`}
        setPopupOpen={setDeletePopupOpen}
        onConfirm={async () => {
          await deleteChatSign(sign);
          await onDelete();
          setDeletePopupOpen(false);
        }}
      />
    )}
  </>);
}

// Кнопку без ссылки не показываем: иначе она выглядит рабочей, но никуда не ведёт
const SocialLinks = () => {
  const socials = [
    { name: 'Telegram', url: botLinks.telegram, color: '#26A5E4' },
    { name: 'MAX', url: botLinks.max, color: '#7C3AED' },
    { name: 'VK', url: botLinks.vk, color: '#0077FF' },
  ].filter(({ url }) => url);

  if (socials.length === 0) return null;

  return (<div style={{ display: 'flex', gap: '12px', flexWrap: 'wrap' }}>
      {socials.map(({ name, url, color }) => (
        <a
          key={name}
          href={url}
          target="_blank"
          rel="noopener noreferrer"
          style={{
            display: 'flex',
            alignItems: 'center',
            gap: '8px',
            padding: '10px 16px',
            borderRadius: '8px',
            backgroundColor: color,
            color: '#fff',
            textDecoration: 'none',
            fontWeight: 500,
            cursor: 'pointer'
          }}
        >
          {name}
        </a>
      ))}
    </div>);
}

interface Props {
  user: UserDto;
}

export const ChatBotsCard = ({ user }: Props) => {
  const [chatSignToken, setChatSignToken] = useState('');
  const { showToast } = useToast();

  const { signs: chatSigns, mutate: mutateChatSigns } = useChatSigns(
    user?.mailConfirmed === true,
  );
  
  const handleConfirmSign = async () => {
      await confirmChatSign(chatSignToken);
      await mutateChatSigns();
      setChatSignToken('');
      showToast('Аккаунт успешно привязан', 'success')
    };
  
  return <Card title="Чат боты" width={'40rem'}>
    <SocialLinks />
    <div className={styles.chatSignsSection}>
      {chatSigns.length === 0 ? (
        <p className={styles.cardDescription}>
          У вас пока нет добавленных чат ботов. Добавьте их, чтобы получать уведомления и
          взаимодействовать с площадкой через мессенджеры.
        </p>
      ) : (
        <div className={styles.stats}>
          {chatSigns.map((sign, index) => 
            <Sign key={`${sign.chatId}-${index}`} sign={sign} onDelete={mutateChatSigns} />)
          }
        </div>
      )}

      <Collapsible title="Ручная привязка к чат боту">
        <div className={styles.signInputRow}>
          <div className={styles.signInputWrapper}>
            <Input
              value={chatSignToken}
              onChange={setChatSignToken}
              placeholder="Код привязки"
              type="chatSign"
            />
          </div>
          <Button
            variant={ButtonTypeEnum.PRIMARY}
            size={ButtonSizeEnum.SMALL}
            onClick={handleConfirmSign}
          >
            Подтвердить
          </Button>
        </div>
      </Collapsible>
    </div>
  </Card>
};
