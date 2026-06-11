import styles from './styles.module.css';

interface Props {
  children: React.ReactNode;
  title?: string;
  hoverable?: boolean;
  width?: string;
  min_width?: string;
}

export const Card = ({ children, title, hoverable, width, min_width }: Props) => {
    const style: React.CSSProperties = {
        ...(width ? { width } : {}),
        ...(min_width ? { minWidth: min_width } : {})
    };

    return (
    <div 
        className={`${styles.card} ${hoverable ? '' : styles.nonHoverable}`}
        style={style}>
        {title && <h2 className={styles.cardTitle}>{title}</h2>}
        {children}
    </div>);
};