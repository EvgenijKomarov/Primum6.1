import { useCurrentUser } from '@/entity/user';
import styles from './SideNav.module.css';
import { NavLink, useLocation, useNavigate } from 'react-router';
import { NAV_ITEMS } from '../config/constants';
import clsx from 'clsx';
import { resolveRoleLabel } from '@/widgets/header/lib';

interface SideNavProps {
  isOpen: boolean;
  setIsOpen: (open: boolean) => void;
}

export const SideNav = ({ isOpen, setIsOpen }: SideNavProps) => {
  const { role, user, availableRoles, setActiveRole  } = useCurrentUser();
  const location = useLocation();
  const showNav = location.pathname !== '/auth';
  const navItems = NAV_ITEMS[role];
  const navigate = useNavigate();

  if (!showNav || !navItems) return null;

  return (
    <>
      {/* Оверлей на весь экран под панелью. Клик по нему закрывает слайдер */}
      <div
        className={clsx(styles.overlay, isOpen && styles.overlayVisible)}
        onClick={() => setIsOpen(false)}
      />
      
      <div className={styles.wrapper}>
        <div className={clsx(styles.panel, isOpen && styles.panelOpen)}>
          <div className={styles.userInfo}>
            <span className={styles.userName}>{user?.displayName}</span>
            {availableRoles.map((r) => (
                      <button
                        key={r}
                        className={clsx(styles.dropdownItem, r === role && styles.dropdownItemActive)}
                        onClick={() => { setActiveRole(r); }}
                      >
                        <span>{resolveRoleLabel(r)}</span>
                        {r === role && (
                          <svg width="14" height="14" viewBox="0 0 14 14" fill="none">
                            <path d="M2.5 7L5.5 10L11.5 4" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round"/>
                          </svg>
                        )}
                      </button>
                    ))}
                    <div className={styles.dropdownDivider} />
                    <button
                      className={styles.dropdownItem}
                      onClick={() => { navigate('/profile'); }}
                    >
                      Перейти в профиль
                    </button>
          </div>
          <nav className={styles.navList}>
            {navItems.map((item) => (
              <NavLink
                key={item.path}
                to={item.path}
                onClick={() => setIsOpen(false)}
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