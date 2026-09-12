import { useRef, useState } from 'react';

import { useCommonNotifications } from '@/entity/commonNotification/model/useCommonNotifications';
import { setSeenNotification } from '@/entity/commonNotification/api/common-notification.api';
import { formatDateTime } from '@/shared/format/format-config';
import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import { BellIcon } from '@/shared/icons/types';
import Button from '@/shared/ui/Button/Button.tsx';

import styles from './Header.module.css';
import { useOnClickOutside } from '@/shared/lib/useOnClickOutside/useOnClickOutside';

export const Notifications = () => {
  const { notifications, isLoading, mutate } = useCommonNotifications();
  const [isOpen, setIsOpen] = useState(false);
  const rootRef = useRef<HTMLDivElement>(null);

  useOnClickOutside(rootRef, () => setIsOpen(false), isOpen);

  const unseenCount = notifications.filter((n) => !n.seen).length;

  const handleMarkSeen = async (id: string) => {
    await setSeenNotification(id);
    mutate();
  };

  return (
    <div className={styles.userMenu} ref={rootRef}>
      <Button
        variant={ButtonTypeEnum.PRIMARY}
        size={ButtonSizeEnum.SMALL}
        icon={<BellIcon />}
        onClick={() => setIsOpen((v) => !v)}
        isLoading={isLoading}
      >
        {unseenCount !== 0 && <span className={styles.notificationCount}>{unseenCount}</span>}
      </Button>

      {isOpen && (
        <div className={styles.dropdown} style={{ width: '30rem' }}>
          {notifications.length === 0 ? (
            <span className={styles.notificationText}>Уведомлений нет</span>
          ) : (
            <div className={styles.notificationsTabs}>
              {notifications.map((notification) => (
                <div
                  key={notification.id}
                  className={notification.seen ? styles.notificationTab : styles.newNotificationTab}
                  onClick={() => !notification.seen && handleMarkSeen(notification.id)}
                >
                  <span className={styles.notificationDate}>{formatDateTime(notification.datetime)}</span>
                  <span className={styles.notificationText}>{notification.text}</span>
                </div>
              ))}
            </div>
          )}
        </div>
      )}
    </div>
  );
};