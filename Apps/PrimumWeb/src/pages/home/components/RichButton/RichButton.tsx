import styles from './RichButton.module.css'

interface Props {
  label: string
  onClick?: () => void
}

// <button>, а не <a> без href: работает с клавиатуры и корректно ловит тап на мобильных
export const RichButton = ({label, onClick}: Props) => {
    return (
        <button type="button" className={styles.btn} onClick={onClick}>
            {label}
        </button>
    );
}
