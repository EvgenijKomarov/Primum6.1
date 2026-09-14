import styles from './Monetization.module.css'
import Container from '../Container/Container'
import SectionHead from '../SectionHead/SectionHead'
import type { MoneyCardItem } from '../MoneyCard/MoneyCard'
import type { DefinitionItem } from '../Definition/Definition'
import MoneyCard from '../MoneyCard/MoneyCard'
import Definition from '../Definition/Definition'

interface MonetizationProps {
  label?: string
  title?: string
  description?: string
  cards?: MoneyCardItem[]
  totalLabel?: string
  totalValue?: string
  definitions?: DefinitionItem[]
}

const defaultCards: MoneyCardItem[] = [
  { percent: '30%', titleLines: ['НЕСГОРАЕМАЯ', 'ЧАСТЬ'] },
  { percent: '0–20%', titleLines: ['БОНУС ЗА', 'КОНВЕРСИЮ'] },
  { percent: '0–20%', titleLines: ['БОНУС ЗА', 'УРОВЕНЬ'] },
  { percent: '0–10%', titleLines: ['БОНУС ЗА', 'СЕРИЮ'] },
]

const defaultDefinitions: DefinitionItem[] = [
  {
    term: 'КОНВЕРСИЯ',
    description:
      'Это абонементы, у которых хотя бы один урок был оплачен. Для вычисления берётся среднее значение среди 10 последних абонементов после бесплатных уроков.',
  },
  {
    term: 'УРОВЕНЬ',
    description: 'За проведённые уроки вы и ваш курс получаете опыт и уровень. Чем выше уровень, тем больше бонус.',
  },
  {
    term: 'СЕРИЯ',
    description: 'За каждый оплаченный урок в абонементе начисляется бонус 2,5%. Максимальный бонус — 10%.',
  },
]

export default function Monetization({
  label = '// 09 — МОНЕТИЗАЦИЯ',
  title = 'Сколько вы будете получать?',
  description = 'Цены за уроки вы назначаете сами. Ваш процент зависит от уровня профиля, конверсии и оплаченных занятий.',
  cards = defaultCards,
  totalLabel = 'Максимальная доля от стоимости урока',
  totalValue = 'ДО 80%',
  definitions = defaultDefinitions,
}: MonetizationProps) {
  return (
    <section className={styles.section}>
      <Container>
        <SectionHead label={label} title={title} description={description} />

        <div className={styles.grid}>
          {cards.map((c, i) => (
            <MoneyCard key={i} {...c} />
          ))}
        </div>

        <div className={styles.total}>
          <span>{totalLabel}</span>
          <strong>{totalValue}</strong>
        </div>

        <div className={styles.definitions}>
          {definitions.map((d, i) => (
            <Definition key={i} {...d} />
          ))}
        </div>
      </Container>
    </section>
  )
}
