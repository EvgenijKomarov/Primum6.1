import styles from './TeacherPlatform.module.css'
import Container from '../Container/Container'
import type { BenefitItem } from '../Benefit/Benefit'
import Benefit from '../Benefit/Benefit'
import teacherPlatformPic from './teacherPlatformPic.png'

interface TeacherPlatformProps {
  label?: string
  title?: string
  description?: string
  photoSrc?: string
  photoAlt?: string
  benefits?: BenefitItem[]
}

const defaultBenefits: BenefitItem[] = [
  { title: 'СОЗДАЙТЕ КУРС', description: 'Создайте собственный курс и настройте его под себя.' },
  { title: 'ВЫСТАВЬТЕ ОКНА', description: 'Укажите свободное время, когда готовы проводить занятия.' },
  { title: 'ЖДИТЕ УЧЕНИКОВ', description: 'Ученики сами смогут записаться на подходящее время.' },
  { title: 'ВСЁ В ОДНОМ МЕСТЕ', description: 'Не нужно вручную координировать расписание и занятия.' },
]

export default function TeacherPlatform({
  label = '// 07 — ПРЕПОДАВАТЕЛЯМ',
  title = 'Что мы предлагаем?',
  description = 'Удобную платформу для создания и проведения онлайн-занятий.',
  photoAlt = 'Преподаватель',
  benefits = defaultBenefits,
}: TeacherPlatformProps) {
  return (
    <section className={styles.section}>
      <Container>
        <div className={styles.intro}>
          <div>
            <div className={styles.label}>{label}</div>
            <h2 className={styles.title}>{title}</h2>
            <p className={styles.description}>{description}</p>
          </div>

          <img src={teacherPlatformPic} alt={photoAlt} className={styles.photo} />
        </div>

        <div className={styles.benefits}>
          {benefits.map((b, i) => (
            <Benefit key={i} {...b} />
          ))}
        </div>
      </Container>
    </section>
  )
}
