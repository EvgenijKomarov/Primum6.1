import styles from './AudienceCard.module.css'

interface AudienceCardProps {
  href: string
  label: string
}

export default function AudienceCard({ href, label }: AudienceCardProps) {
  return (
    <a href={href} className={styles.card}>
      <span>{label}</span>
      <span className={styles.arrow}>→</span>
    </a>
  )
}
