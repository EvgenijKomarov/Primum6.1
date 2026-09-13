import { Link, NavLink, useLocation } from 'react-router';
import { useState } from 'react';
import clsx from 'clsx';

import { useCurrentUser } from '@/entity/user';
import { ButtonSizeEnum } from '@/shared/enums';
import { MenuIcon } from '@/shared/icons/types';
import Button from '@/shared/ui/Button/Button.tsx';
import { SideNav } from '@/widgets/side-nav/ui/SideNav';

import { NAV_ITEMS } from '../config/constants';
import { Notifications } from './Notifications';
import { UserMenu } from './UserMenu';
import styles from './Header.module.css';

const HIDDEN_HEADER_ROUTES = ['/'];
const HIDDEN_ACTIONS_ROUTES = ['/auth'];

export const Header = () => {
  const { role } = useCurrentUser();
  const location = useLocation();
  const [isMobileNavOpen, setIsMobileNavOpen] = useState(false);

  if (HIDDEN_HEADER_ROUTES.includes(location.pathname)) return null;

  const showActions = !HIDDEN_ACTIONS_ROUTES.includes(location.pathname);
  const navItems = NAV_ITEMS[role];

  return (
    <header className={styles.header}>
      <Link to={showActions ? '/profile' : '/'} className={styles.logo}>PrimumCode</Link>

      {showActions && (
        <div className={styles.headerActions}>
          <nav className={styles.desktopNav}>
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

          <div className={styles.desktopActions}>
            <Notifications />
            <UserMenu />
          </div>

          <div className={styles.mobileActions}>
            <Notifications />
            <Button
              size={ButtonSizeEnum.NORMAL}
              icon={<MenuIcon />}
              onClick={() => setIsMobileNavOpen((v) => !v)}
            />
          </div>
        </div>
      )}

      {showActions && (
        <SideNav
          isOpen={isMobileNavOpen}
          onClose={() => setIsMobileNavOpen(false)}
          navItems={navItems}
        />
      )}
    </header>
  );
};