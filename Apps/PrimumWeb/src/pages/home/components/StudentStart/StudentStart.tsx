import Container from '../Container/Container'
import SectionHead from '../SectionHead/SectionHead'
import type { StepItem } from '../Step/Step'
import Step from '../Step/Step'
import styles from './StudentStart.module.css'

interface StudentStartProps {
  label?: string
  title?: string
  description?: string
  steps?: StepItem[]
  footnote?: string
}

const defaultSteps: StepItem[] = [
  { number: '01', title: 'РЕГИСТРАЦИЯ', description: 'Создайте аккаунт на платформе.' },
  {
    number: '02',
    title: 'ПРОФИЛЬ',
    description: 'Создайте профиль ученика в личном кабинете.',
  },
  {
    number: '03',
    title: 'ЗАПИСЬ',
    description: 'Во вкладке "Доступные курсы" выберите курс и свободное время преподавателя. Занятия сами создадутся',
  },
]

export default function TeacherStart({
  label = '// 5 — СТАРТ',
  title = 'Как начать заниматься?',
  description = 'Зарегистрируйтесь, создайте профиль и запишитесь на понравившийся',
  steps = defaultSteps,
}: StudentStartProps) {
  return (
    <section className={styles.section}>
      <Container>
        <SectionHead label={label} title={title} description={description} />

        <div className={styles.steps}>
          {steps.map((s, i) => (
            <Step key={i} {...s} />
          ))}
        </div>
      </Container>
    </section>
  )
}
