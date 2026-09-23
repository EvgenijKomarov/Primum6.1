import styles from './RichButton.module.css'

interface Props {
  label: string
  onClick?: () => void
}

export const RichButton = ({label, onClick}: Props) => {
    return (
        <a className={styles.btn} onClick={onClick}>
            {label}
        </a>
    );
}