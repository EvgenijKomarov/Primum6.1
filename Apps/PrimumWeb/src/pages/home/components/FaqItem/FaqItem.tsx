import styles from './FaqItem.module.css'

export interface FaqItemData {
  question: string
  answer: string
}

interface FaqItemProps extends FaqItemData {
  isActive: boolean
  onToggle: () => void
}

export default function FaqItem({ question, answer, isActive, onToggle }: FaqItemProps) {
  return (
    <div className={`${styles.item} ${isActive ? styles.active : ''}`}>
      <button className={styles.question} onClick={onToggle}>
        <span>{question}</span>
        <span className={styles.plus}>{isActive ? '×' : '+'}</span>
      </button>

      <div className={styles.answer}>
        <p>{answer}</p>
      </div>
    </div>
  )
}
