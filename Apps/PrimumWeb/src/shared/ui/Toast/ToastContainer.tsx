import { useState, useEffect, useCallback } from 'react';
import type { Toast } from './toast.types';
import styles from './ToastContainer.module.css';

interface Props {
  toasts: Toast[];
  onDismiss: (id: number) => void;
}

export function ToastContainer({ toasts, onDismiss }: Props) {
  return (
    <div className={styles.container}>
      {toasts.map(toast => (
        <ToastItem key={toast.id} toast={toast} onDismiss={onDismiss} />
      ))}
    </div>
  );
}

function ToastItem({ toast, onDismiss }: { toast: Toast; onDismiss: (id: number) => void }) {
  const [visible, setVisible] = useState(false);
  const [hiding, setHiding] = useState(false);

  const dismiss = useCallback(() => {
    setHiding(true);
  }, []);

  useEffect(() => {
    requestAnimationFrame(() => setVisible(true));
  }, []);

  useEffect(() => {
    const timer = setTimeout(dismiss, toast.duration);
    return () => clearTimeout(timer);
  }, [toast.duration, dismiss]);

  const handleTransitionEnd = () => {
    if (hiding) onDismiss(toast.id);
  };

  return (
    <div
      className={`${styles.toast} ${styles[toast.type]} ${visible && !hiding ? styles.visible : ''}`}
      onClick={dismiss}
      onTransitionEnd={handleTransitionEnd}
      role="alert"
    >
      {toast.message}
    </div>
  );
}