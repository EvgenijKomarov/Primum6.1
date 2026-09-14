import styles from './Feature.module.css'

export interface FeatureItem {
  number: string
  title: string
  description: string
}

export default function Feature({ number, title, description }: FeatureItem) {
  return (
    <div className={styles.feature}>
      <div className={styles.number}>{number}</div>

      <div>
        <h3 className={styles.title}>{title}</h3>
        <p className={styles.description}>{description}</p>
      </div>
    </div>
  )
}
