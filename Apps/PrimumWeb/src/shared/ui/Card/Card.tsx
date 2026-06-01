import styles from './styles.module.css';

interface Props {
  children: React.ReactNode;
  title?: string;
}

export const Card = ({ children, title }: Props) => {
    return (
    <div className={styles.card}>
        {title && <h2 className={styles.cardTitle}>{title}</h2>}
        {children}
    </div>);
};