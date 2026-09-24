import styles from './TeacherStart.module.css'
import Container from '../Container/Container'
import SectionHead from '../SectionHead/SectionHead'
import type { StepItem } from '../Step/Step'
import Step from '../Step/Step'

interface TeacherStartProps {
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
    description: 'Заполните профиль преподавателя и укажите необходимые данные.',
  },
  {
    number: '03',
    title: 'ПРОВЕРКА',
    description: 'После создания профиля администрация свяжется с вами.',
  },
  { number: '04', title: 'НАЧАЛО', description: 'Создавайте курсы и назначайте свободное время' },
]

export default function TeacherStart({
  label = '// 11 — СТАРТ',
  title = 'Как начать преподавать?',
  description = 'Зарегистрируйтесь, создайте профиль и дождитесь связи с администрацией.',
  steps = defaultSteps,
  footnote = '* Для работы необходимо быть самозанятым.',
}: TeacherStartProps) {
  return (
    <section className={styles.section}>
      <Container>
        <SectionHead label={label} title={title} description={description} />

        <div className={styles.steps}>
          {steps.map((s, i) => (
            <Step key={i} {...s} />
          ))}
        </div>

        <div className={styles.footnote}>{footnote}</div>
      </Container>
    </section>
  )
}
