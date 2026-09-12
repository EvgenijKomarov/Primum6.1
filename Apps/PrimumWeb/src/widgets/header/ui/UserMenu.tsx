import { useRef, useState } from 'react';
import { useNavigate } from 'react-router';
import clsx from 'clsx';

import { useCurrentUser } from '@/entity/user';
import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';
import { resolveDisplayName, resolveRoleLabel } from '../lib';

import styles from './Header.module.css';
import { useOnClickOutside } from '@/shared/lib/useOnClickOutside/useOnClickOutside';

export const UserMenu = () => {
  const navigate = useNavigate();
  const { user, role, setActiveRole, availableRoles } = useCurrentUser();
  const [isOpen, setIsOpen] = useState(false);
  const rootRef = useRef<HTMLDivElement>(null);

  useOnClickOutside(rootRef, () => setIsOpen(false), isOpen);

  if (!user) {
    return (
      <Button variant={ButtonTypeEnum.SECONDARY} size={ButtonSizeEnum.SMALL} onClick={() => navigate('/auth')}>
        Войти/Зарегистрироваться
      </Button>
    );
  }

  return (
    <div className={styles.userMenu} ref={rootRef}>
      <Button variant={ButtonTypeEnum.SECONDARY} size={ButtonSizeEnum.NORMAL} onClick={() => setIsOpen((v) => !v)}>
        <div className={styles.userInfoButton}>
          <div className={styles.userInfo}>
            <span className={styles.userName}>{resolveDisplayName(user)}</span>
            <span className={styles.userRole}>{resolveRoleLabel(role)}</span>
          </div>
          <svg
            className={clsx(styles.chevron, isOpen && styles.chevronOpen)}
            width="12" height="12" viewBox="0 0 12 12" fill="none"
          >
            <path d="M2 4L6 8L10 4" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
          </svg>
        </div>
      </Button>

      {isOpen && (
        <div className={styles.dropdown}>
          <p className={styles.dropdownLabel}>Активный профиль</p>
          {availableRoles.map((r) => (
            <button
              key={r}
              className={clsx(styles.dropdownItem, r === role && styles.dropdownItemActive)}
              onClick={() => { setActiveRole(r); setIsOpen(false); }}
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
      )}
    </div>
  );
};