import styles from './Hero.module.css'
import Container from '../Container/Container'
import AudienceCard from '../AudienceCard/AudienceCard'
import heroPic from './heroPic.png'

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
  photoAlt?: string
  terminalLines?: string[]
}

const defaultOptions: AudienceOption[] = [
  { href: '#students', label: 'БУДУ УЧЕНИКОМ' },
  { href: '#teachers', label: 'БУДУ ПРЕПОДАВАТЕЛЕМ' },
]

const defaultTerminalLines = ['> start_learning()', '> level_up()', '> █']

export default function Hero({
  eyebrow = 'PRIMUMCODE / EDUCATION',
  title = 'Сервис нового уровня',
  description = 'Платформа для обучения детей программированию и для преподавателей, которые хотят заниматься обучением, а не рутиной.',
  audienceTitle = 'ВЫБЕРИТЕ СВОЙ ПУТЬ',
  audienceOptions = defaultOptions,
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

          <img src={heroPic} alt={photoAlt} />

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
