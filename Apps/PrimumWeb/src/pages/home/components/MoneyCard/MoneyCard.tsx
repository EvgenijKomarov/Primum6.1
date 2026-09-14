import styles from './MoneyCard.module.css'

export interface MoneyCardItem {
  percent: string
  titleLines: string[]
}

export default function MoneyCard({ percent, titleLines }: MoneyCardItem) {
  return (
    <div className={styles.card}>
      <div className={styles.percent}>{percent}</div>

      <h3 className={styles.title}>
        {titleLines.map((line, i) => (
          <span key={i}>
            {line}
            {i < titleLines.length - 1 && <br />}
          </span>
        ))}
      </h3>
    </div>
  )
}
