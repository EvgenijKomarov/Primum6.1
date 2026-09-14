import styles from './Courses.module.css'
import Container from '../Container/Container'
import SectionHead from '../SectionHead/SectionHead'
import { usePublicCourses, type CourseDto, type CourseDtoLite } from '@/entity/course'
import { usePublicThemes } from '@/entity/course-theme/model/usePublicThemes'
import { useState } from 'react'
import clsx from 'clsx'

interface CoursesProps {
  label?: string
  title?: string
  description?: string
  courses?: CourseDto[]
}

const CourseCard = ({
  name,
  about,
  level,
  freeLessons,
  rank,
  teacherName,
  price,
}: CourseDtoLite) => {
  return (
    <article className={styles.course}>
      <div className={styles.content}>
        <h3 className={styles.title}>{name}</h3>

        <p className={styles.description}>{about}</p>

        <div className={styles.info}>
          <div>
            <small>Уровень</small>
            <strong>{level}</strong>
          </div>

          <div>
            <small>Бесплатно</small>
            <strong>{freeLessons}</strong>
          </div>

          <div>
            <small>Ранг</small>
            <strong>{rank}</strong>
          </div>
        </div>

        <div className={styles.bottom}>
          <div className={styles.teacher}>{teacherName}</div>
          <div className={styles.price}>{price} р.</div>
        </div>
      </div>
    </article>
  )
}

export default function Courses({
  label = '// 02 — КУРСЫ',
  title = 'Наши курсы',
  description = 'Начните с основ и постепенно превращайте свои идеи в настоящие программы.',
}: CoursesProps) {
  const [selectedThemeId, setSelectedThemeId] = useState<number | null>(null);

  const { courses } = usePublicCourses(selectedThemeId, 0, 3);
  const { data: themesResult } = usePublicThemes();
  const themes = themesResult?.items ?? [];

  return (
    <section className={styles.section}>
      <Container>
        <SectionHead label={label} title={title} description={description} />
      </Container>

      <Container>
        <div className={styles.themes}>
          <button
            type="button"
            className={clsx(styles.theme, selectedThemeId === null && styles.themeActive)}
            onClick={() => setSelectedThemeId(null)}
          >
            Все
          </button>

          {themes.map((theme) => (
            <button
              key={theme.id}
              type="button"
              className={clsx(styles.theme, selectedThemeId === theme.id && styles.themeActive)}
              onClick={() => setSelectedThemeId(theme.id)}
            >
              {theme.themeName}
            </button>
          ))}
        </div>
      </Container>

      <Container>
        <div className={styles.grid}>
          {courses.map((course) => (
            <CourseCard key={course.id} {...course} />
          ))}
        </div>
      </Container>
    </section>
  );
}
