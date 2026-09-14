import styles from './Benefit.module.css'

export interface BenefitItem {
  title: string
  description: string
}

export default function Benefit({ title, description }: BenefitItem) {
  return (
    <div className={styles.benefit}>
      <h3 className={styles.title}>{title}</h3>
      <p className={styles.description}>{description}</p>
    </div>
  )
}
