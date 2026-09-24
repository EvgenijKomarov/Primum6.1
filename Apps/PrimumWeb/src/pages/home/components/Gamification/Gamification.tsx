import styles from './Gamification.module.css'
import Container from '../Container/Container'
import type { GameCardItem } from '../GameCard/GameCard'
import GameCard from '../GameCard/GameCard'
import gamificationPic from './gamificationPic.png'

interface GamificationProps {
  label?: string
  titleBefore?: string
  titleHighlight?: string
  description?: string
  cards?: GameCardItem[]
  photoSrc?: string
  photoAlt?: string
}

const defaultCards: GameCardItem[] = [
  { title: 'ОЦЕНКИ', description: 'Результат каждого занятия влияет на прогресс.' },
  { title: 'ОПЫТ', description: 'За занятия ученик получает опыт и повышает уровень.' },
  { title: 'МОНЕТКИ', description: 'Внутренняя валюта платформы за хорошие оценки.' },
  { title: 'НАГРАДЫ', description: 'Монеты можно обменивать на цифровые товары в любимых играх.' },
]

export default function Gamification({
  label = '// 04 — ГЕЙМИФИКАЦИЯ',
  titleBefore = 'Учиться также легко,',
  titleHighlight = 'как и играть.',
  description = 'Каждое занятие влияет на прогресс ученика. Чем выше результат — тем больше опыта и монет получает ученик.',
  cards = defaultCards,
  photoAlt = 'Ученик',
}: GamificationProps) {
  return (
    <section className={styles.section}>
      <Container>
        <div className={styles.grid}>
          <div>
            <div className={styles.label}>{label}</div>

            <h2 className={styles.title}>
              {titleBefore} <span className={styles.title} style={{color: "var(--color-primary-base)"}}>{titleHighlight}</span>
            </h2>

            <p className={styles.description}>{description}</p>

            <div className={styles.cards}>
              {cards.map((c, i) => (
                <GameCard key={i} {...c} />
              ))}
            </div>
          </div>

          <div className={styles.photo}>
            <img src={gamificationPic} alt={photoAlt} />
          </div>
        </div>
      </Container>
    </section>
  )
}
