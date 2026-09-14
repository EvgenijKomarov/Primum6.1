import styles from './Hero.module.css'
import Container from '../Container/Container'
import AudienceCard from '../AudienceCard/AudienceCard'

export interface AudienceOption {
  href: string
  label: string
}

interface HeroProps {
  eyebrow?: string
  title?: string
  description?: string
  audienceTitle?: string
  audienceOptions?: AudienceOption[]
  photoSrc?: string
  photoAlt?: string
  terminalLines?: string[]
}

const defaultOptions: AudienceOption[] = [
  { href: '#students', label: 'Я УЧЕНИК / РОДИТЕЛЬ' },
  { href: '#teachers', label: 'Я ПРЕПОДАВАТЕЛЬ' },
]

const defaultTerminalLines = ['> start_learning()', '> level_up()', '> █']

export default function Hero({
  eyebrow = 'PRIMUMCODE / EDUCATION',
  title = 'Сервис нового уровня',
  description = 'Платформа для обучения детей программированию и для преподавателей, которые хотят заниматься обучением, а не рутиной.',
  audienceTitle = 'ВЫБЕРИТЕ СВОЙ ПУТЬ',
  audienceOptions = defaultOptions,
  photoSrc = 'https://img.freepik.com/premium-photo/young-girl-coding-laptop_14117-749835.jpg',
  photoAlt = 'Ребёнок занимается программированием',
  terminalLines = defaultTerminalLines,
}: HeroProps) {
  return (
    <section className={styles.hero}>
      <Container>
        <div className={styles.content}>
          <div className={styles.eyebrow}>{eyebrow}</div>

          <h1 className={styles.title}>
            {title}
          </h1>

          <p className={styles.description}>{description}</p>

          <div className={styles.audienceTitle}>{audienceTitle}</div>

          <div className={styles.audience}>
            {audienceOptions.map((option) => (
              <AudienceCard key={option.href} href={option.href} label={option.label} />
            ))}
          </div>
        </div>

        <div className={styles.photo}>
          <div className={styles.square} />

          <img src={photoSrc} alt={photoAlt} />

          <div className={styles.terminal}>
            {terminalLines.map((line, i) => (
              <span key={i}>
                {line}
                {i < terminalLines.length - 1 && <br />}
              </span>
            ))}
          </div>
        </div>
      </Container>
    </section>
  )
}
