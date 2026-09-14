import styles from './Definition.module.css'

export interface DefinitionItem {
  term: string
  description: string
}

export default function Definition({ term, description }: DefinitionItem) {
  return (
    <div className={styles.definition}>
      <strong>{term}</strong>
      <p>{description}</p>
    </div>
  )
}
