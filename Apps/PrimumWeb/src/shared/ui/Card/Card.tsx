import styles from './styles.module.css';

interface Props {
  children: React.ReactNode;
  title?: string;
  hoverable?: boolean;
  width?: string;
}

export const Card = ({ children, title, hoverable, width }: Props) => {
    return (
    <div 
        className={`${styles.card} ${hoverable ? '' : styles.nonHoverable}`}
        style={width ? { width: `${width}` } : undefined}>
        {title && <h2 className={styles.cardTitle}>{title}</h2>}
        {children}
    </div>);
};