import styles from './CourseCard.module.css'

export interface Course {
  imgSrc: string
  imgAlt: string
  code: string
  title: string
  description: string
  level: string
  lessons: string
  rank: string
  teacher: string
  price: string
}

export default function CourseCard({
  imgSrc,
  imgAlt,
  code,
  title,
  description,
  level,
  lessons,
  rank,
  teacher,
  price,
}: Course) {
  return (
    <article className={styles.course}>
      <img src={imgSrc} alt={imgAlt} />

      <div className={styles.content}>
        <div className={styles.code}>{code}</div>

        <h3 className={styles.title}>{title}</h3>

        <p className={styles.description}>{description}</p>

        <div className={styles.info}>
          <div>
            <small>Уровень</small>
            <strong>{level}</strong>
          </div>

          <div>
            <small>Бесплатно</small>
            <strong>{lessons}</strong>
          </div>

          <div>
            <small>Ранг</small>
            <strong>{rank}</strong>
          </div>
        </div>

        <div className={styles.bottom}>
          <div className={styles.teacher}>{teacher}</div>
          <div className={styles.price}>{price}</div>
        </div>
      </div>
    </article>
  )
}
