import { useRef, useState } from "react";
import styles from './styles.module.css';
import { ChevronIcon } from "@/shared/icons/types";

interface CollapsibleProps {
  title: string;
  children: React.ReactNode;
  defaultOpen?: boolean;
}

export const Collapsible = ({ title, children, defaultOpen = false }: CollapsibleProps) => {
  const [isOpen, setIsOpen] = useState(defaultOpen);
  const contentRef = useRef<HTMLDivElement>(null);

  const handleToggle = () => {
    const next = !isOpen;
    setIsOpen(next);
    if (contentRef.current) {
      contentRef.current.style.maxHeight = next
        ? `${contentRef.current.scrollHeight}px`
        : '0px';
    }
  };

  return (
    <div className={styles.collapsible}>
      <button className={styles.toggle} onClick={handleToggle}>
        <span className={styles.title}>{title}</span>
        <ChevronIcon className={isOpen ? styles.chevronOpen : styles.chevron} />
      </button>
      <div
        ref={contentRef}
        className={styles.content}
        style={{ maxHeight: defaultOpen ? 'none' : '0px' }}
      >
        <div className={styles.inner}>
          {children}
        </div>
      </div>
    </div>
  );
};
