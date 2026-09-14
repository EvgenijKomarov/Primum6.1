import { useState } from 'react'
import styles from './Faq.module.css'
import Container from '../Container/Container'
import type { FaqItemData } from '../FaqItem/FaqItem'
import FaqItem from '../FaqItem/FaqItem'

interface FaqProps {
  label?: string
  title?: string
  items?: FaqItemData[]
}

const defaultItems: FaqItemData[] = [
  {
    question: 'Какой формат занятий?',
    answer: 'Онлайн-занятия проходят через платформу PrimumCode. Конкретный формат зависит от выбранного курса.',
  },
  {
    question: 'Как отслеживать прогресс?',
    answer: 'Прогресс ученика можно отслеживать через личный кабинет и доступные инструменты платформы.',
  },
  {
    question: 'Как стать преподавателем?',
    answer:
      'Зарегистрируйтесь, создайте профиль преподавателя и заполните необходимые данные. После этого администрация свяжется с вами.',
  },
  {
    question: 'Можно ли работать со своими учениками?',
    answer:
      'Да. Вы можете добавить собственных учеников к своему курсу по реферальной ссылке и получать фиксированные 80% от стоимости уроков с ними.',
  },
]

export default function Faq({ label = '// FAQ', title = 'Остались вопросы?', items = defaultItems }: FaqProps) {
  const [activeIndex, setActiveIndex] = useState<number | null>(null)

  const handleToggle = (index: number) => {
    setActiveIndex((current) => (current === index ? null : index))
  }

  return (
    <section className={styles.section} id="faq">
      <Container>
        <div className={styles.head}>
          <div className={styles.label}>{label}</div>
          <h2 className={styles.title}>{title}</h2>
        </div>

        <div className={styles.list}>
          {items.map((item, i) => (
            <FaqItem
              key={i}
              question={item.question}
              answer={item.answer}
              isActive={activeIndex === i}
              onToggle={() => handleToggle(i)}
            />
          ))}
        </div>
      </Container>
    </section>
  )
}
