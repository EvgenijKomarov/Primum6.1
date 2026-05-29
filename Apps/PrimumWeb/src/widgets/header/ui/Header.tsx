import { clsx } from 'clsx';
import { Link, NavLink, useLocation, useNavigate } from 'react-router';

import { useCurrentUser } from '@/entity/user';
import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';

import styles from './Header.module.css';
import { NAV_ITEMS } from "@/widgets/header/config";
import { resolveDisplayName, resolveRoleLabel } from "@/widgets/header/lib";
import { useEffect, useRef, useState } from 'react';

export const Header = () => {
  const navigate = useNavigate();
  const { role, user, availableRoles, setActiveRole } = useCurrentUser();
  const [open, setOpen] = useState(false);
  const menuRef = useRef<HTMLDivElement>(null);

  const navItems = NAV_ITEMS[role];

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

  const location = useLocation();
  const showActions = location.pathname !== '/auth';

  return (
    <header className={styles.header}>
      <Link to="/" className={styles.logo}>PrimumCode</Link>

      {showActions && (<div className={styles.headerActions}>
        <nav className={styles.nav}>
          {navItems.map((item) => (
            <NavLink
              key={item.path}
              to={item.path}
              className={({ isActive }) => clsx(styles.navLink, isActive && styles.navLinkActive)}
            >
              {item.label}
            </NavLink>
          ))}
        </nav>

        <div className={styles.actions}>
          {user ? (
            <div className={styles.userMenu} ref={menuRef}>
              <Button
                variant={ButtonTypeEnum.SECONDARY}
                size={ButtonSizeEnum.SMALL}
                onClick={() => setOpen(v => !v)}
              >
                <span className={styles.userInfo}>
                  <span className={styles.userName}>{resolveDisplayName(user)}</span>
                  <span className={styles.userRole}>{resolveRoleLabel(role)}</span>
                </span>
                <svg
                  className={clsx(styles.chevron, open && styles.chevronOpen)}
                  width="12" height="12" viewBox="0 0 12 12" fill="none"
                >
                  <path d="M2 4L6 8L10 4" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round"/>
                </svg>
              </Button>

              {open && (
                <div className={styles.dropdown}>
                  <p className={styles.dropdownLabel}>Активный профиль</p>
                  {availableRoles.map((r) => (
                    <button
                      key={r}
                      className={clsx(styles.dropdownItem, r === role && styles.dropdownItemActive)}
                      onClick={() => { setActiveRole(r); setOpen(false); }}
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
                    onClick={() => { navigate('/profile'); setOpen(false); }}
                  >
                    Перейти в профиль
                  </button>
                </div>
              )}
            </div>
          ) : (
            <Button
              variant={ButtonTypeEnum.PRIMARY}
              size={ButtonSizeEnum.SMALL}
              onClick={() => navigate('/auth')}
            >
              Войти/Зарегистрироваться
            </Button>
          )}
        </div>
      </div>) }
    </header>
  );
};
