import styles from './Platform.module.css'
import Container from '../Container/Container'
import PhotoFrame from '../PhotoFrame/PhotoFrame'
import type { FeatureItem } from '../Feature/Feature'
import Feature from '../Feature/Feature'
import platformPic from './platformPic.png'

interface PlatformProps {
  label?: string
  titleBefore?: string
  titleHighlight?: string
  description?: string
  features?: FeatureItem[]
  photoSrc?: string
  photoAlt?: string
}

const defaultFeatures: FeatureItem[] = [
  {
    number: '01',
    title: 'ЛИЧНЫЙ КАБИНЕТ',
    description: 'Все занятия и информация об обучении находятся в одном месте.',
  },
  {
    number: '02',
    title: 'ОТСЛЕЖИВАНИЕ ПРОГРЕССА',
    description: 'Следите за развитием ученика и его результатами.',
  },
  {
    number: '03',
    title: 'ДОСТУП ИЗ МЕССЕНДЖЕРОВ',
    description: 'Получайте доступ к платформе и отслеживайте прогресс в любое время.',
  },
]

export default function Platform({
  label = '// 03 — ОБУЧЕНИЕ',
  titleBefore = 'Учебный процесс',
  titleHighlight = 'нового уровня.',
  description = 'Мы предоставляем гибкие и удобные возможности для обучения детей. Всё необходимое находится в одном месте.',
  features = defaultFeatures,
  photoAlt = 'Онлайн обучение',
}: PlatformProps) {
  return (
    <section className={styles.section}>
      <Container>
        <div className={styles.grid}>
          <PhotoFrame src={platformPic} alt={photoAlt} />

          <div>
            <div className={styles.label}>{label}</div>

            <h2 className={styles.title}>
              {titleBefore} <span className={styles.title} style={{color: "var(--color-primary-base)"}}>{titleHighlight}</span>
            </h2>

            <p className={styles.description}>{description}</p>

            <div className={styles.features}>
              {features.map((f, i) => (
                <Feature key={i} {...f} />
              ))}
            </div>
          </div>
        </div>
      </Container>
    </section>
  )
}
