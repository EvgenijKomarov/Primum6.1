import * as Dialog from '@radix-ui/react-dialog';
import type { PropsWithChildren } from 'react';

import styles from './Modal.module.css';

const XIcon = () => (
  <svg width="16" height="16" viewBox="0 0 16 16" fill="none" aria-hidden="true">
    <path d="M12 4L4 12M4 4l8 8" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" />
  </svg>
);

interface ModalProps {
  open: boolean;
  onClose: () => void;
  title?: string;
}

export const Modal = ({ open, onClose, title, children }: PropsWithChildren<ModalProps>) => (
  <Dialog.Root open={open} onOpenChange={(isOpen) => !isOpen && onClose()}>
    <Dialog.Portal>
      <Dialog.Overlay className={styles.overlay} />
      <Dialog.Content className={styles.content} aria-describedby={undefined}>
        <div className={styles.header}>
          {title ? (
            <Dialog.Title className={styles.title}>{title}</Dialog.Title>
          ) : (
            <Dialog.Title className={styles.title} />
          )}
          <Dialog.Close asChild>
            <button className={styles.closeButton} aria-label="Закрыть">
              <XIcon />
            </button>
          </Dialog.Close>
        </div>
        {children}
      </Dialog.Content>
    </Dialog.Portal>
  </Dialog.Root>
);
