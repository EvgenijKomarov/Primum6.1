import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';
import { Input } from '@/shared/ui/Input';
import type { ChatSign } from '@/entity/chat-sign/model/types';
import styles from '../ui/ProfilePage.module.css';

interface Props {
  chatSigns: ChatSign[];
  chatSignToken: string;
  onTokenChange: (v: string) => void;
  onConfirmSign: () => void;
}

export const ChatBotsCard = ({ chatSigns, chatSignToken, onTokenChange, onConfirmSign }: Props) => (
  <div className={styles.card}>
    <h2 className={styles.cardTitle}>Чат боты</h2>
    <div className={styles.chatSignsSection}>
      {chatSigns.length === 0 ? (
        <p className={styles.cardDescription}>
          У вас пока нет добавленных чат ботов. Добавьте их, чтобы получать уведомления и
          взаимодействовать с площадкой через мессенджеры.
        </p>
      ) : (
        <div className={styles.stats}>
          {chatSigns.map((sign) => (
            <div key={sign.chatId} className={styles.stat}>
              <span className={styles.statLabel}>{sign.realizationTag}</span>
              <span className={styles.statValue}>{sign.username ?? sign.chatId}</span>
            </div>
          ))}
        </div>
      )}

      <h2 className={styles.cardSubtitle}>Добавить чат бота</h2>
      <div className={styles.signInputRow}>
        <div className={styles.signInputWrapper}>
          <Input
            value={chatSignToken}
            onChange={onTokenChange}
            placeholder="Подпись"
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
    </div>
  </div>
);
