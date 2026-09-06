import styles from './styles.module.css';

interface Props {
  title?: string;
  value: string | React.ReactNode;
  onClick?: () => void;
  onDelete?: () => void;
}

export const StatCard = ({ title, value, onClick, onDelete }: Props) => {
    return (
    <div className={styles.stat} onClick={onClick}>
        <div className={styles.statHeader}>
            {title && <h6 className={styles.statLabel}>{title}</h6>}
            {onDelete && (<p className={styles.delete} onClick={onDelete}>X</p>)}
        </div>
        {value && <span className={styles.statValue}>{value}</span>}
    </div>)
};