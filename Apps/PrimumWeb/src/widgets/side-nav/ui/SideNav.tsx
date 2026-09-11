
import { useCurrentUser } from '@/entity/user';
import styles from './SideNav.module.css';
import { NavLink, useLocation } from 'react-router';
import { NAV_ITEMS } from '../config/constants';
import { useRef, useState } from 'react';
import clsx from 'clsx';

const OPEN_THRESHOLD = 0.35; // доля ширины панели, после которой считаем "открыто"
const DRAG_TOLERANCE = 6; // px, ниже которого считаем это кликом, а не драгом

export const SideNav = () => {
  const { role } = useCurrentUser();
  const location = useLocation();
  const showNav = location.pathname !== '/auth';
  const navItems = NAV_ITEMS[role];

  const [open, setOpen] = useState(false);
  const [dragging, setDragging] = useState(false);
  const [dragOffset, setDragOffset] = useState(0);

  const panelRef = useRef<HTMLDivElement>(null);
  const panelWidthRef = useRef(256);
  const startXRef = useRef(0);
  const startOpenRef = useRef(false);
  const didDragRef = useRef(false);

  const handlePointerDown = (e: React.PointerEvent) => {
    (e.target as HTMLElement).setPointerCapture(e.pointerId);
    panelWidthRef.current = panelRef.current?.offsetWidth ?? panelWidthRef.current;
    startXRef.current = e.clientX;
    startOpenRef.current = open;
    didDragRef.current = false;
    setDragging(true);
  };

  const handlePointerMove = (e: React.PointerEvent) => {
    if (!dragging) return;
    const delta = e.clientX - startXRef.current;
    if (Math.abs(delta) > DRAG_TOLERANCE) didDragRef.current = true;

    const base = startOpenRef.current ? 0 : -panelWidthRef.current;
    const next = Math.min(0, Math.max(-panelWidthRef.current, base + delta));
    setDragOffset(next);
  };

  const handlePointerUp = () => {
    if (!dragging) return;
    setDragging(false);

    if (didDragRef.current) {
      const width = panelWidthRef.current;
      setOpen(dragOffset > -width * (1 - OPEN_THRESHOLD));
    } else {
      setOpen((v) => !v);
    }
    setDragOffset(0);
  };

  if (!showNav) return null;

  return (
    <>
      <div
        className={clsx(styles.overlay, open && !dragging && styles.overlayVisible)}
        onClick={() => setOpen(false)}
      />
      <div className={styles.wrapper}>
        <div
          ref={panelRef}
          className={clsx(styles.panel, open && !dragging && styles.panelOpen)}
          style={dragging ? { transform: `translateX(${dragOffset}px)` } : undefined}
        >
          <nav className={styles.navList}>
            {navItems.map((item) => (
              <NavLink
                key={item.path}
                to={item.path}
                onClick={() => setOpen(false)}
                className={({ isActive }) => clsx(styles.navLink, isActive && styles.navLinkActive)}
              >
                {item.label}
              </NavLink>
            ))}
          </nav>

          <div
            className={styles.tab}
            onPointerDown={handlePointerDown}
            onPointerMove={handlePointerMove}
            onPointerUp={handlePointerUp}
            onPointerCancel={handlePointerUp}
          >
            <svg
              width="10" height="16" viewBox="0 0 10 16" fill="none"
              style={{ transform: open ? 'rotate(180deg)' : 'none' }}
            >
              <path d="M2 2L8 8L2 14" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round"/>
            </svg>
          </div>
        </div>
      </div>
    </>
  );
};