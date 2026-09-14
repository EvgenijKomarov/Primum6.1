import type { ReactNode } from 'react'
import styles from './SectionHead.module.css'

interface SectionHeadProps {
  label: string
  title: ReactNode
  description?: ReactNode
  withMargin?: boolean
}

export default function SectionHead({ label, title, description, withMargin = true }: SectionHeadProps) {
  return (
    <div className={withMargin ? styles.head : undefined}>
      <div className={styles.label}>{label}</div>
      <h2 className={styles.title}>{title}</h2>
      {description && <p className={styles.description}>{description}</p>}
    </div>
  )
}
