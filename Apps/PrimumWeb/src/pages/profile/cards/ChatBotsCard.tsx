import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';
import { Input } from '@/shared/ui/Input';
import type { ChatSign } from '@/entity/chat-sign/model/types';
import styles from '../ui/ProfilePage.module.css';
import { Collapsible } from '@/shared/ui/Collapsible';
import { Card } from '@/shared/ui/Card/Card';
import { StatCard } from '@/shared/ui/StatCard/StatCard';
import { deleteChatSign } from '@/entity/chat-sign/api/chat-sign.api';
import { useState } from 'react';
import { EnsurancePopup } from '@/widgets/popups/ensurance-popup/ui/EnsurancePopup';

interface Props {
  chatSigns: ChatSign[];
  chatSignToken: string;
  onTokenChange: (v: string) => void;
  onConfirmSign: () => void;
  mutateChatSigns: () => void;
}

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

export const ChatBotsCard = ({ chatSigns, chatSignToken, onTokenChange, onConfirmSign, mutateChatSigns }: Props) => (
  <Card title="Чат боты" width={'40rem'}>
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
              onChange={onTokenChange}
              placeholder="Код привязки"
              type="chatSign"
            />
          </div>
          <Button
            variant={ButtonTypeEnum.PRIMARY}
            size={ButtonSizeEnum.SMALL}
            onClick={onConfirmSign}
          >
            Подтвердить
          </Button>
        </div>
      </Collapsible>
    </div>
  </Card>
);
