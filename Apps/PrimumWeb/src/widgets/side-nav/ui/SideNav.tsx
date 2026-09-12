import { useEffect } from 'react';
import { NavLink, useNavigate } from 'react-router';
import clsx from 'clsx';

import { useCurrentUser } from '@/entity/user';
import { resolveDisplayName, resolveRoleLabel } from '@/widgets/header/lib';
import type { NavItem } from '@/widgets/header/config/constants';

import styles from './SideNav.module.css';
import Button from '@/shared/ui/Button/Button';
import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';

interface SideNavProps {
  isOpen: boolean;
  onClose: () => void;
  navItems: NavItem[];
}

export const SideNav = ({ isOpen, onClose, navItems }: SideNavProps) => {
  const { role, user, availableRoles, setActiveRole } = useCurrentUser();
  const navigate = useNavigate();

  // Блокируем скролл body, пока панель открыта
  useEffect(() => {
    document.body.style.overflow = isOpen ? 'hidden' : '';
    return () => { document.body.style.overflow = ''; };
  }, [isOpen]);

  return (
    <>
      {/* Оверлей — только визуальное затемнение, кликом не закрывается */}
      <div className={clsx(styles.overlay, isOpen && styles.overlayVisible)} />

      <div className={styles.wrapper}>
        <div className={clsx(styles.panel, isOpen && styles.panelOpen)}>
          {user ? (
            <div className={styles.userInfo}>
              <span className={styles.userName}>{resolveDisplayName(user)}</span>
              {availableRoles.map((r) => (
                <button
                  key={r}
                  className={clsx(styles.dropdownItem, r === role && styles.dropdownItemActive)}
                  onClick={() => setActiveRole(r)}
                >
                  <span>{resolveRoleLabel(r)}</span>
                  {r === role && (
                    <svg width="14" height="14" viewBox="0 0 14 14" fill="none">
                      <path d="M2.5 7L5.5 10L11.5 4" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
                    </svg>
                  )}
                </button>
              ))}
            </div>
          ) : (
            <div className={styles.unauthorized}>
              <p className={styles.unauthorizedDescription}>Для доступа на площадку необходимо аутентифицироваться</p>
              <Button variant={ButtonTypeEnum.PRIMARY} size={ButtonSizeEnum.SMALL} onClick={() => navigate('/auth')}>
                Войти/Зарегистрироваться
              </Button>
            </div>
          )}

          <nav className={styles.navList}>
            {navItems.map((item) => (
              <NavLink
                key={item.path}
                to={item.path}
                onClick={onClose}
                className={({ isActive }) => clsx(styles.navLink, isActive && styles.navLinkActive)}
              >
                {item.label}
              </NavLink>
            ))}
          </nav>
        </div>
      </div>
    </>
  );
};