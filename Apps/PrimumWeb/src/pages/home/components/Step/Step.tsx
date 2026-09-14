import styles from './Step.module.css'

export interface StepItem {
  number: string
  title: string
  description: string
}

export default function Step({ number, title, description }: StepItem) {
  return (
    <div className={styles.step}>
      <div className={styles.number}>{number}</div>
      <h3 className={styles.title}>{title}</h3>
      <p className={styles.description}>{description}</p>
    </div>
  )
}
