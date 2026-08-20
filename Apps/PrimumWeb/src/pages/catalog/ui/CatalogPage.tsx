import { useState } from 'react';

import { usePublicCourses } from '@/entity/course';
import type { CourseDtoLite } from '@/entity/course';

import styles from './CatalogPage.module.css';
import { CourseScheduleSubscribe } from '@/widgets/popups/select-shedule/ui/CourseScheduleSubscribe';
import { Card } from '@/shared/ui/Card/Card';
import { TeacherInfo } from '@/widgets/popups/info/teacher-info/TeacherInfo';
import { CourseRankInfo } from '@/widgets/popups/rank-info/course-rank-info/CourseRankInfo';
import { usePublicThemes } from '@/entity/course-theme/model/usePublicThemes';
import type { CourseThemeDto } from '@/entity/course-theme';

interface CourseCardProps {
  course: CourseDtoLite;
}
const CourseCard = ({ course }: CourseCardProps) => {
  const isFree = course.price === 0;
  const [subscribePopupOpen, setSubscribePopupOpen] = useState(false);


  return (
    <Card hoverable={true} min_width={'30rem'}>
      <div className={styles.card}>
        <div>
          {course.courseThemeName && (
            <span className={styles.cardTheme}>{course.courseThemeName}</span>
          )}
          <h3 className={styles.cardName}>{course.name ?? '—'}</h3>
          {course.about && <p className={styles.cardAbout}>{course.about}</p>}
        </div>

        <div className={styles.cardMeta}>
          <div className={styles.cardMetaItem}>
            <span className={styles.cardMetaLabel}>Макс. уроков</span>
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
              <span className={styles.cardMetaLabel}>Ранг</span>
              <CourseRankInfo rankInput={course.rank} />
            </div>
          )}
        </div>

        <div className={styles.cardFooter}>
          <span className={styles.cardTeacher}>
            <TeacherInfo teacherId={course.teacherId} />
          </span>
          <div className={styles.cardFooterRight}>
            <span className={`${styles.cardPrice} ${isFree ? styles.cardPriceFree : ''}`}>
              {isFree ? 'Бесплатно' : `${course.price.toFixed(0)} ₽`}
            </span>
            <button
              className={styles.subscribeBtn}
              onClick={(e) => { e.stopPropagation(); setSubscribePopupOpen(true); }}
            >
              Записаться
            </button>
          </div>
        </div>
        {subscribePopupOpen && (
          <CourseScheduleSubscribe
            course={course}
            setSubscribePopupOpen={setSubscribePopupOpen}
          />
        )}
      </div>
    </Card>
  );
};

interface ThemeTabProps {
  theme: CourseThemeDto
}
const ThemeTab = ({theme}: ThemeTabProps) => {
  const { courses, isLoading } = usePublicCourses(theme.id);

  return <>{ courses.length > 0 ?  <div className={styles.courseThemeTab}>
        <h2 className={styles.courseThemeTabHeader}>
          {theme.themeName}
        </h2>
        <div className={styles.courseThemeTabContent}>
          {isLoading ? (
              Array.from({ length: 6 }).map((_, i) => (
                <div key={i} className={styles.skeleton} />
              ))
            ) : (
              courses.map((course) => (
                <CourseCard
                  key={course.id}
                  course={course}
                />
              ))
            )}
        </div>
      </div> : null}
    </>
}

export const CatalogPage = () => {
  const { data: themesResult } = usePublicThemes();
  const themes = themesResult?.items?.filter((t) => t.isActive) ?? [];

  return (
    <div className={styles.page}>
      <div className={styles.header}>
        <h1 className={styles.title}>Каталог курсов</h1>
        <p className={styles.subtitle}>Выберите тему и найдите подходящий курс</p>
      </div>

      <div className={styles.content}>
        {themes.map((theme) => (
            <ThemeTab
              key={theme.id}
              theme={theme}
            />
          ))}
      </div>
    </div>
  );
};
