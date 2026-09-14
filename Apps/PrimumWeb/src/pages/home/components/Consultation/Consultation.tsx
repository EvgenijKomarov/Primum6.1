import styles from './Consultation.module.css'
import Container from '../Container/Container'
import { ConsultationBlock } from '../ConsultationBlock/ConsultationBlock'

interface ConsultationProps {
  label?: string
  title?: string
  description?: string
  submitLabel?: string
  successLabel?: string
}

export default function Consultation({
  label = '// 05 — КОНСУЛЬТАЦИЯ',
  title = 'Остались вопросы?',
  description = 'Оставьте свои контакты — мы свяжемся с вами и расскажем всё необходимое об обучении и платформе.'
}: ConsultationProps) {

  return (
    <section className={styles.section}>
      <Container>
        <div className={styles.grid}>
          <div>
            <div className={styles.label}>{label}</div>
            <h2 className={styles.title}>{title}</h2>
            <p className={styles.description}>{description}</p>
          </div>

          <ConsultationBlock/>
        </div>
      </Container>
    </section>
  )
}
