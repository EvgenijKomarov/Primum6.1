import styles from './FinalCta.module.css'
import Container from '../Container/Container'
import Button from '../Button/Button'

export interface FinalCtaAction {
  href: string
  label: string
  variant: 'primary' | 'outline'
}

interface FinalCtaProps {
  label?: string
  title?: string
  description?: string
  actions?: FinalCtaAction[]
}

const defaultActions: FinalCtaAction[] = [
  { href: '#students', label: 'НАЧАТЬ УЧИТЬСЯ', variant: 'primary' },
  { href: '#teachers', label: 'СТАТЬ ПРЕПОДАВАТЕЛЕМ', variant: 'outline' },
]

export default function FinalCta({
  label = '// SYSTEM READY',
  title = 'Готовы начать?',
  description = 'Выберите свой путь в PrimumCode — учиться или преподавать.',
  actions = defaultActions,
}: FinalCtaProps) {
  return (
    <section className={styles.section}>
      <Container>
        <div className={styles.label}>{label}</div>
        <h2 className={styles.title}>{title}</h2>
        <p className={styles.description}>{description}</p>

        <div className={styles.buttons}>
          {actions.map((action) => (
            <Button key={action.href + action.label} href={action.href} variant={action.variant}>
              {action.label}
            </Button>
          ))}
        </div>
      </Container>
    </section>
  )
}
