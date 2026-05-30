import { useEffect, useRef, useState } from 'react';
import styles from './Popup.module.css';
import { createPortal } from 'react-dom';

interface PopupProps {
  title: string;
  onClose: () => void;
  children: React.ReactNode;
}

export const Popup = ({ onClose, children, title = "" }: PopupProps) => {
  const ref = useRef<HTMLDivElement>(null);
  const [visible, setVisible] = useState(false);

  useEffect(() => {
    requestAnimationFrame(() => setVisible(true));
  }, []);

  const handleClose = () => {
    setVisible(false);
  };

  const handleTransitionEnd = () => {
    if (!visible) onClose();
  };

  useEffect(() => {
    const handler = (e: MouseEvent) => {
      if (ref.current && !ref.current.contains(e.target as Node)) {
        handleClose();
      }
    };
    document.addEventListener('mousedown', handler);
    return () => document.removeEventListener('mousedown', handler);
  }, [onClose]);

  return createPortal(
    <div
      className={`${styles.overlay} ${visible ? styles.overlayVisible : ''}`}
      onTransitionEnd={handleTransitionEnd}
    >
      <div
        className={`${styles.popup} ${visible ? styles.popupVisible : ''}`}
        ref={ref}
      >
        <div className={styles.header}>
          <span className={styles.title}>{title}</span>
        </div>
        {children}
      </div>
    </div>,
    document.body
  );
};