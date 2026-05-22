import { useState } from 'react';
import useSWRImmutable from 'swr/immutable';

import { usePublicCourses } from '@/entity/course';
import type { CourseDto } from '@/entity/course';
import { getPublicThemes } from '@/entity/course-theme';
import { api } from '@/shared/config/api.ts';

import styles from './CatalogPage.module.css';

const usePublicThemes = () =>
  useSWRImmutable(
    [api.publicTheme.getThemes],
    async () => (await getPublicThemes()).data,
    { revalidateOnMount: true },
  );

const EmptyIcon = () => (
  <svg className={styles.emptyIcon} viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.5">
    <path strokeLinecap="round" strokeLinejoin="round" d="M21 21l-5.197-5.197m0 0A7.5 7.5 0 1 0 5.196 5.196a7.5 7.5 0 0 0 10.607 10.607Z" />
  </svg>
);

const CourseCard = ({ course }: { course: CourseDto }) => {
  const isFree = course.price === 0;

  return (
    <article className={styles.card}>
      <div>
        {course.courseThemeName && (
          <span className={styles.cardTheme}>{course.courseThemeName}</span>
        )}
        <h3 className={styles.cardName}>{course.name ?? '—'}</h3>
        {course.about && <p className={styles.cardAbout}>{course.about}</p>}
      </div>

      <div className={styles.cardMeta}>
        <div className={styles.cardMetaItem}>
          <span className={styles.cardMetaLabel}>Уроков</span>
          <span className={styles.cardMetaValue}>{course.maxLessons}</span>
        </div>
        {course.freeLessons > 0 && (
          <div className={styles.cardMetaItem}>
            <span className={styles.cardMetaLabel}>Бесплатно</span>
            <span className={styles.cardMetaValue}>{course.freeLessons}</span>
          </div>
        )}
        {course.rank && (
          <div className={styles.cardMetaItem}>
            <span className={styles.cardMetaLabel}>Уровень</span>
            <span className={styles.rankBadge}>{course.rank}</span>
          </div>
        )}
      </div>

      <div className={styles.cardFooter}>
        <span className={styles.cardTeacher}>{course.teacherName ?? '—'}</span>
        <span className={`${styles.cardPrice} ${isFree ? styles.cardPriceFree : ''}`}>
          {isFree ? 'Бесплатно' : `${course.price.toFixed(0)} ₽`}
        </span>
      </div>
    </article>
  );
};

export const CatalogPage = () => {
  const [selectedThemeId, setSelectedThemeId] = useState<number | null>(null);

  const { data: themesResult, isLoading: themesLoading } = usePublicThemes();
  const themes = themesResult?.items?.filter((t) => t.isActive) ?? [];

  const { courses, isLoading: coursesLoading } = usePublicCourses(selectedThemeId);

  const isLoading = themesLoading || coursesLoading;

  return (
    <div className={styles.page}>
      <div className={styles.header}>
        <h1 className={styles.title}>Каталог курсов</h1>
        <p className={styles.subtitle}>Выберите тему и найдите подходящий курс</p>
      </div>

      <div className={styles.filterBar}>
        <button
          className={`${styles.chip} ${selectedThemeId === null ? styles.chipActive : ''}`}
          onClick={() => setSelectedThemeId(null)}
        >
          Все
        </button>
        {themes.map((theme) => (
          <button
            key={theme.id}
            className={`${styles.chip} ${selectedThemeId === theme.id ? styles.chipActive : ''}`}
            onClick={() => setSelectedThemeId(theme.id)}
          >
            {theme.themeName}
          </button>
        ))}
      </div>

      <div className={styles.grid}>
        {isLoading ? (
          Array.from({ length: 6 }).map((_, i) => (
            <div key={i} className={styles.skeleton} />
          ))
        ) : courses.length === 0 ? (
          <div className={styles.empty}>
            <EmptyIcon />
            <p className={styles.emptyText}>
              {selectedThemeId ? 'Курсов по этой теме пока нет' : 'Курсов пока нет'}
            </p>
          </div>
        ) : (
          courses.map((course) => <CourseCard key={course.id} course={course} />)
        )}
      </div>
    </div>
  );
};
