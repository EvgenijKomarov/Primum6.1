import styles from './Courses.module.css'
import Container from '../Container/Container'
import SectionHead from '../SectionHead/SectionHead'
import type { CourseDto } from '@/entity/course'

interface CoursesProps {
  label?: string
  title?: string
  description?: string
  courses?: CourseDto[]
}

export default function Courses({
  label = '// 02 — КУРСЫ',
  title = 'Наши курсы',
  description = 'Начните с основ и постепенно превращайте свои идеи в настоящие программы.',
}: CoursesProps) {
  return (
    <section className={styles.section}>
      <Container>
        <SectionHead label={label} title={title} description={description} />

        <div className={styles.grid}>
        </div>
      </Container>
    </section>
  )
}
