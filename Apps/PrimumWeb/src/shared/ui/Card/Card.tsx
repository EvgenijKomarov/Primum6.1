import styles from './styles.module.css';

interface Props {
  children: React.ReactNode;
  title?: string;
  hoverable?: boolean;
  width?: number;
}

export const Card = ({ children, title, hoverable, width }: Props) => {
    return (
    <div 
        className={`${styles.card} ${hoverable ? '' : styles.nonHoverable}`}
        style={width ? { width: `${width}rem` } : undefined}>
        {title && <h2 className={styles.cardTitle}>{title}</h2>}
        {children}
    </div>);
};