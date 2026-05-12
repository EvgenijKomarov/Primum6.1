import { clsx } from 'clsx';
import { Link, NavLink, useNavigate } from 'react-router';

import { useCurrentUser } from '@/entity/user';
import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';

import styles from './Header.module.css';
import { NAV_ITEMS } from "@/widgets/header/config";
import { resolveDisplayName } from "@/widgets/header/lib";

export const Header = () => {
  const navigate = useNavigate();
  const { role, user } = useCurrentUser();

  const navItems = NAV_ITEMS[role];

  return (
    <header className={styles.header}>
      <Link to="/" className={styles.logo}>Primum</Link>

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
          <Button
            variant={ButtonTypeEnum.SECONDARY}
            size={ButtonSizeEnum.SMALL}
            onClick={() => navigate('/profile')}
          >
            {resolveDisplayName(user)}
          </Button>
        ) : (
          <Button
            variant={ButtonTypeEnum.PRIMARY}
            size={ButtonSizeEnum.SMALL}
            onClick={() => navigate('/auth')}
          >
            Sign In
          </Button>
        )}
      </div>
    </header>
  );
};
