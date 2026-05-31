import { useTeacherCourses } from '@/entity/course';
import type { CourseDto } from '@/entity/course';
import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';
import { Loader } from '@/shared/ui/Loader';

import styles from './CoursesPage.module.css';
import { BookIcon, PlusIcon } from '@/shared/icons/types';
import { Badge } from '@/shared/ui/Badge/Badge';
import { BadgeTypeEnum } from '@/shared/enums/badge';
import { useState } from 'react';
import { CreateCourseForm } from '@/widgets/popups/create-course';

const CourseCard = ({ course }: { course: CourseDto }) => {

  return (
    <div className={styles.card}>
      <div className={styles.cardTop}>
        <h3 className={styles.courseName}>{course.name ?? '—'}</h3>
      </div>

      <div className={styles.badges}>
        {course.onCheck && (
          <Badge text="На проверке" badgeType={BadgeTypeEnum.Warning} />
        )}
        {course.isAvailable === false && (
          <Badge text="Недоступен" badgeType={BadgeTypeEnum.Negative} />
        )}
        {course.isActive ? (
          <Badge text="Активен" badgeType={BadgeTypeEnum.Positive} />
        ) : (
          <Badge text="Скрыт" badgeType={BadgeTypeEnum.Negative} />
        )}
      </div>

      {course.courseThemeName && (
        <span className={styles.theme}>{course.courseThemeName}</span>
      )}

      <div className={styles.metaRow}>
        <div className={styles.metaItem}>
          <span className={styles.metaLabel}>Цена</span>
          <span className={styles.metaValue}>{course.price.toFixed(0)} ₽</span>
        </div>
        <div className={styles.metaItem}>
          <span className={styles.metaLabel}>Максимум уроков в неделю</span>
          <span className={styles.metaValue}>{course.maxLessons}</span>
        </div>
        <div className={styles.metaItem}>
          <span className={styles.metaLabel}>Бесплатных уроков</span>
          <span className={styles.metaValue}>{course.freeLessons}</span>
        </div>
      </div>
    </div>
  );
};

export const CoursesPage = () => {
  const { courses, isLoading, mutate } = useTeacherCourses();
  const [coursePopupOpen, setCoursePopupOpen] = useState(false);

  if (isLoading) return <Loader />;

  return (
    <div className={styles.page}>
      <div className={styles.header}>
        <h1 className={styles.title}>Мои курсы</h1>
        <Button
          variant={ButtonTypeEnum.PRIMARY}
          size={ButtonSizeEnum.NORMAL}
          icon={<PlusIcon />}
          onClick={() => setCoursePopupOpen(true)}
        >
          Создать курс
        </Button>
      </div>
      {coursePopupOpen && (
        <CreateCourseForm
          setCoursePopupOpen={setCoursePopupOpen}
          onSuccess={mutate}
        />
      )}

      {courses.length === 0 ? (
        <div className={styles.empty}>
          <BookIcon />
          <p className={styles.emptyText}>Курсов пока нет. Создайте первый!</p>
        </div>
      ) : (
        <div className={styles.grid}>
          {courses.map((course) => (
            <CourseCard key={course.id} course={course} />
          ))}
        </div>
      )}
    </div>
  );
};
