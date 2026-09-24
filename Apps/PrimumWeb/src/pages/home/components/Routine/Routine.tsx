import styles from './Routine.module.css'
import Container from '../Container/Container'
import PhotoFrame from '../PhotoFrame/PhotoFrame'
import routinePic from './routinePic.png'

interface RoutineProps {
  label?: string
  titleBefore?: string
  titleHighlight?: string
  description?: string
  photoSrc?: string
  photoAlt?: string
}

export default function Routine({
  label = '// 08 — АВТОМАТИЗАЦИЯ',
  titleBefore = 'Оставьте',
  titleHighlight = 'рутину нам.',
  description = 'Вам достаточно создать курсы, выставить окна и ждать, когда ученики сами запишутся на занятие.',
  photoAlt = 'Преподаватель',
}: RoutineProps) {
  return (
    <section className={styles.section}>
      <Container>
        <div className={styles.grid}>
          <div>
            <div className={styles.label}>{label}</div>

            <h2 className={styles.title}>
              {titleBefore} <span className={styles.title} style={{color: "var(--color-primary-base)"}}>{titleHighlight}</span>
            </h2>

            <p className={styles.description}>{description}</p>
          </div>

          <PhotoFrame src={routinePic} alt={photoAlt} />
        </div>
      </Container>
    </section>
  )
}
