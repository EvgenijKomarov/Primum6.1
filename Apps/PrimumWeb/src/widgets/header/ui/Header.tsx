import { Link, useLocation, useNavigate } from 'react-router';

import { useCurrentUser } from '@/entity/user';
import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';

import styles from './Header.module.css';
import { useEffect, useRef, useState } from 'react';
import { BellIcon, MenuIcon } from '@/shared/icons/types';
import { useCommonNotifications } from '@/entity/commonNotification/model/useCommonNotifications';
import { setSeenNotification } from '@/entity/commonNotification/api/common-notification.api';
import { formatDateTime } from '@/shared/format/format-config';
import { SideNav } from '@/widgets/side-nav/ui/SideNav';
const Notifications = () => {
  const { notifications, isLoading, mutate } = useCommonNotifications();
  const [open, setOpen] = useState(false);
  const menuRef = useRef<HTMLDivElement>(null);

  const handleSetSeenNotification = async (id: string) => {
    await setSeenNotification(id);
    mutate();
  }

  useEffect(() => {
    if (!open) return;
    const handleClick = (e: MouseEvent) => {
      if (menuRef.current && !menuRef.current.contains(e.target as Node)) {
        setOpen(false);
      }
    };
    document.addEventListener('mousedown', handleClick);
    return () => document.removeEventListener('mousedown', handleClick);
  }, [open]);

  const newNotificationsCount = notifications.filter((x) => {return x.seen === false}).length;

  return (<div className={styles.userMenu} ref={menuRef}>
            <Button 
                  variant={ButtonTypeEnum.PRIMARY}
                  size={ButtonSizeEnum.SMALL}
                  icon={<BellIcon/>}
                  onClick={() => {setOpen(v => !v)}}
                  isLoading = {isLoading}
            >
              {newNotificationsCount !== 0 && (
                <span className={styles.notificationCount}>{newNotificationsCount}</span>
              )}
            </Button>
            {open && (
              <div className={styles.dropdown} style={{ width: '30rem'}}>
                  {notifications.length !== 0 ? (
                    <div className={styles.notificationsTabs}>
                      {notifications.map((notification) => {
                        return (
                          <div 
                            className={notification.seen ? styles.notificationTab : styles.newNotificationTab}
                            onClick={() => {if (!notification.seen){ handleSetSeenNotification(notification.id); }}}
                          >
                            <span className={styles.notificationDate}>{formatDateTime(notification.datetime)}</span>
                            <span className={styles.notificationText}>{notification.text}</span>
                          </div>)
                      })}
                    </div>
                  ) : (
                    <span className={styles.notificationText}>Уведомлений нет</span>
                  )}
              </div>
            )}
          </div>)
}


export const Header = () => {
  const navigate = useNavigate();
  const { user, } = useCurrentUser();
  const [open, setOpen] = useState(false);

  const location = useLocation();
  const showActions = !['/auth'].includes(location.pathname);
  const showHeader = !['/'].includes(location.pathname);

  return (
    <>
      {showHeader && (
        <>
          <SideNav isOpen={open} setIsOpen={setOpen}/>
          <header className={styles.header}>
            <Link to="/profile" className={styles.logo}>PrimumCode</Link>

            {showActions && (<div className={styles.headerActions}>
              <div className={styles.actions}>
                {user ? (
                  <div className={styles.right}>
                    <Notifications
                    />
                    <Button 
                      icon={<MenuIcon/>}
                      onClick={() => setOpen(!open)}/>
                  </div>
                ) : (
                  <Button
                    variant={ButtonTypeEnum.SECONDARY}
                    size={ButtonSizeEnum.SMALL}
                    onClick={() => navigate('/auth')}
                  >
                    Войти/Зарегистрироваться
                  </Button>
                )}
              </div>
            </div>) }
          </header>
        </>
        )}
    </>
  );
};
