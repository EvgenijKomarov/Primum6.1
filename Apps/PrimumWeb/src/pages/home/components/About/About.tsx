import styles from './About.module.css'
import Container from '../Container/Container'
import PhotoFrame from '../PhotoFrame/PhotoFrame'
import aboutPic from './aboutPic.png'

interface AboutProps {
  label?: string
  title?: string
  paragraphs?: string[]
  photoSrc?: string
  photoAlt?: string
}

const defaultParagraphs = [
  'Мы — группа энтузиастов, которые поставили себе цель сделать обучение детей программированию и другим наукам максимально комфортным и интересным.',
  'Мы хотим, чтобы ребёнок не просто проходил уроки, а действительно хотел возвращаться и развиваться дальше.',
]

export default function About({
  label = '// 01 — О ПРОЕКТЕ',
  title = 'Кто мы?',
  paragraphs = defaultParagraphs,
  photoAlt = 'Обучение программированию',
}: AboutProps) {
  return (
    <section className={styles.section}>
      <Container>
        <div className={styles.grid}>
          <div>
            <div className={styles.label}>{label}</div>
            <h2 className={styles.title}>{title}</h2>

            {paragraphs.map((p, i) => (
              <p key={i} className={styles.paragraph}>
                {p}
              </p>
            ))}
          </div>

          <PhotoFrame src={aboutPic} alt={photoAlt} />
        </div>
      </Container>
    </section>
  )
}
