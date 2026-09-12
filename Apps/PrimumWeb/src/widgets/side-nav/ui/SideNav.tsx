import { useCurrentUser } from '@/entity/user';
import styles from './SideNav.module.css';
import { NavLink, useLocation } from 'react-router';
import { NAV_ITEMS } from '../config/constants';
import clsx from 'clsx';
import { resolveRoleLabel } from '@/widgets/header/lib';
import { useEffect, useRef } from 'react';

const MOBILE_BREAKPOINT = 640; // синхронизировано с @media (max-width: 40rem) в CSS
const HOVER_EDGE_ZONE_PX = 24;

interface SideNavProps {
  isOpen: boolean;
  setIsOpen: (open: boolean) => void;
}

export const SideNav = ({ isOpen, setIsOpen }: SideNavProps) => {
  const { role, user, availableRoles, setActiveRole } = useCurrentUser();
  const location = useLocation();
  const showNav = location.pathname !== '/auth';
  const navItems = NAV_ITEMS[role];

  const panelRef = useRef<HTMLDivElement>(null);
  const hoverOpenedRef = useRef(false);
  const rafRef = useRef<number | null>(null);

  // Блокируем скролл body, пока панель открыта.
  useEffect(() => {
    if (isOpen) {
      document.body.style.overflow = 'hidden';
    } else {
      document.body.style.overflow = '';
    }
    return () => {
      document.body.style.overflow = '';
    };
  }, [isOpen]);

  // Выдвижение по наведению на правый край / закрытие при уходе мыши.
  // Только для десктопа с мышью.
  useEffect(() => {
    if (!showNav || !navItems) return;
    if (window.matchMedia('(pointer: coarse)').matches) return; // тач-устройства — выключено

    const isDesktopWidth = () => window.innerWidth > MOBILE_BREAKPOINT;

    const closeIfHoverOpened = () => {
      if (hoverOpenedRef.current) {
        hoverOpenedRef.current = false;
        setIsOpen(false);
      }
    };

    const handleMouseMove = (e: MouseEvent) => {
      if (rafRef.current !== null) return;

      rafRef.current = requestAnimationFrame(() => {
        rafRef.current = null;

        if (!isDesktopWidth()) return;

        const nearEdge = e.clientX >= window.innerWidth - HOVER_EDGE_ZONE_PX;

        if (nearEdge && !isOpen) {
          hoverOpenedRef.current = true;
          setIsOpen(true);
          return;
        }

        if (hoverOpenedRef.current && isOpen) {
          const panelRect = panelRef.current?.getBoundingClientRect();
          const overPanel = panelRect ? e.clientX >= panelRect.left : false;
          if (!overPanel && !nearEdge) {
            closeIfHoverOpened();
          }
        }
      });
    };

    const handleDocumentMouseLeave = () => {
      closeIfHoverOpened();
    };

    window.addEventListener('mousemove', handleMouseMove);
    document.documentElement.addEventListener('mouseleave', handleDocumentMouseLeave);

    return () => {
      window.removeEventListener('mousemove', handleMouseMove);
      document.documentElement.removeEventListener('mouseleave', handleDocumentMouseLeave);
      if (rafRef.current !== null) cancelAnimationFrame(rafRef.current);
    };
  }, [isOpen, setIsOpen, showNav, navItems]);

  // Клик снаружи закрывает панель — независимо от того, как она была открыта.
  useEffect(() => {
    if (!isOpen) return;

    const handleClickOutside = (e: MouseEvent) => {
      if (panelRef.current && !panelRef.current.contains(e.target as Node)) {
        hoverOpenedRef.current = false;
        setIsOpen(false);
      }
    };

    document.addEventListener('mousedown', handleClickOutside);
    return () => document.removeEventListener('mousedown', handleClickOutside);
  }, [isOpen, setIsOpen]);

  if (!showNav || !navItems) return null;

  return (
    <>
      <div
        className={clsx(styles.overlay, isOpen && styles.overlayVisible)}
        onClick={() => setIsOpen(false)}
      />

      <div className={styles.wrapper}>
        <div
          ref={panelRef}
          className={clsx(styles.panel, isOpen && styles.panelOpen)}
        >
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