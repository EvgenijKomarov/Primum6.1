import styles from './Referral.module.css'
import Container from '../Container/Container'

interface ReferralProps {
  label?: string
  bigNumber?: string
  smallText?: string
  title?: string
  textBefore?: string
  highlight?: string
  textAfter?: string
}

export default function Referral({
  label = '// 09 — ВАШИ УЧЕНИКИ',
  bigNumber = '80%',
  smallText = 'фиксированная доля от стоимости урока с вашими учениками',
  title = 'У вас уже есть ученики?',
  textBefore = 'Вы можете добавить их к своему курсу по реферальной ссылке. Тогда за уроки с ними вы будете получать фиксированные',
  highlight = '80%',
  textAfter = 'от цены.',
}: ReferralProps) {
  return (
    <section className={styles.section}>
      <Container>
        <div className={styles.grid}>
          <div>
            <div className={styles.label}>{label}</div>
            <div className={styles.bigNumber}>{bigNumber}</div>
            <div className={styles.smallText}>{smallText}</div>
          </div>

          <div>
            <h2 className={styles.title}>{title}</h2>
            <p className={styles.text}>
              {textBefore} <strong style={{color: "var(--color-primary-base)"}}>{highlight}</strong> {textAfter}
            </p>
          </div>
        </div>
      </Container>
    </section>
  )
}
