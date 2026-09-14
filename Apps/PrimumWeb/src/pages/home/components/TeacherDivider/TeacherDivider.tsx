import styles from './TeacherDivider.module.css'
import Container from '../Container/Container'

interface TeacherDividerProps {
  label?: string
  title?: string
  description?: string
}

export default function TeacherDivider({
  label = '// FOR TEACHERS',
  title = 'А если вы преподаватель?',
  description = 'Создавайте курсы, проводите занятия, а рутину оставьте платформе.',
}: TeacherDividerProps) {
  return (
    <section className={styles.section}>
      <Container>
        <div className={styles.label}>{label}</div>
        <h2 className={styles.title}>{title}</h2>
        <p className={styles.description}>{description}</p>
      </Container>
    </section>
  )
}
