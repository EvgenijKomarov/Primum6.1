import styles from './styles.module.css';

interface Props {
  children: React.ReactNode;
  title?: string;
  className?: string;
}

export const Card = ({ children, title, className }: Props) => {
    return (
    <div className={`${styles.card}${className ? ` ${className}` : ''}`}>
        {title && <h2 className={styles.cardTitle}>{title}</h2>}
        {children}
    </div>);
};