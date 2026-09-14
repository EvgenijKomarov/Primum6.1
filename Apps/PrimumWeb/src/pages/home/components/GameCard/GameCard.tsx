import styles from './GameCard.module.css'

export interface GameCardItem {
  title: string
  description: string
}

export default function GameCard({ title, description }: GameCardItem) {
  return (
    <div className={styles.card}>
      <strong>{title}</strong>
      <span>{description}</span>
    </div>
  )
}
