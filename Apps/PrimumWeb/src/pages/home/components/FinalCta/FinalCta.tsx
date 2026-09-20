import styles from './FinalCta.module.css'
import Container from '../Container/Container'
import HomepageButton from '../Button/HomepageButton'
import { useNavigate } from 'react-router'
import { RichButton } from '../RichButton/RichButton'

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
  { href: '#teachers', label: 'ПРЕПОДАВАТЕЛЯМ', variant: 'outline' },
  { href: '#students', label: 'БУДУЩИМ УЧЕНИКАМ', variant: 'outline' },
]

export default function FinalCta({
  label = '// SYSTEM READY',
  title = 'Готовы начать?',
  description = 'Зарегистрируйтесь, подтвердите почту, и создайте профиль в личном кабинете',
  actions = defaultActions,
}: FinalCtaProps) {
  const navigate = useNavigate();

  return (
    <section className={styles.section}>
      <Container>
        <div className={styles.label}>{label}</div>
        <h2 className={styles.title}>{title}</h2>
        <p className={styles.description}>{description}</p>

        <div className={styles.buttons}>
          <RichButton
            label='ЗАРЕГИСТРИРОВАТЬСЯ'
            onClick={() => navigate('/auth')}
          />
          {actions.map((action) => (
            <HomepageButton key={action.href + action.label} href={action.href} variant={action.variant}>
              {action.label}
            </HomepageButton>
          ))}
        </div>
      </Container>
    </section>
  )
}
