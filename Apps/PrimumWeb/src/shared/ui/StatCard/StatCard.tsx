import styles from './styles.module.css';

interface Props {
  title: string;
  value: string | React.ReactNode;
}

export const StatCard = ({ title, value }: Props) => {
    return (
    <div className={styles.stat}>
        {title && <h6 className={styles.statLabel}>{title}</h6>}
        {value && <span className={styles.statValue}>{value}</span>}
    </div>)
};