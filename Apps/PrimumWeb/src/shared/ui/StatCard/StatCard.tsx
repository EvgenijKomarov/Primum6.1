import styles from './styles.module.css';

interface Props {
  title?: string;
  value: string | React.ReactNode;
  onClick?: () => void;
}

export const StatCard = ({ title, value, onClick }: Props) => {
    return (
    <div className={styles.stat} onClick={onClick}>
        {title && <h6 className={styles.statLabel}>{title}</h6>}
        {value && <span className={styles.statValue}>{value}</span>}
    </div>)
};