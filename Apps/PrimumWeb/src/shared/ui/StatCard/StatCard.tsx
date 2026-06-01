import styles from './styles.module.css';

interface Props {
  title: string;
  value: string | React.ReactNode;
}

export const StatCard = ({ title, value }: Props) => {
    return (
    <div className={styles.stat}>
        {title && <h3 className={styles.statTitle}>{title}</h3>}
        {value && <p className={styles.statValue}>{value}</p>}
    </div>)
};