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
import { Card } from '@/shared/ui/Card/Card';
import { CourseRankInfo } from '@/widgets/popups/rank-info/course-rank-info/CourseRankInfo';

const CourseCard = ({ course }: { course: CourseDto }) => {

  return (
    <Card hoverable={true} width={'100%'}>
      <div className={styles.cardTop}>
        <h3 className={styles.courseName}>{course.name ?? '—'}</h3>
        <span className={styles.theme}>{course.courseThemeName}</span>
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
      <table className={styles.metaTable}>
        <tbody>
          <tr className={styles.metaRow}>
            <td className={styles.metaItem}>
              <div className={styles.metaItemInner}>
                <span className={styles.metaLabel}>Уровень</span>
                <span className={styles.metaValue}>{course.level}</span>
              </div>
            </td>
            <td className={styles.metaItem}>
              <div className={styles.metaItemInner}>
                <span className={styles.metaLabel}>Ранг</span>
                <CourseRankInfo rankInput={course.rank} />
              </div>
            </td>
            <td className={styles.metaItem}>
              <div className={styles.metaItemInner}>
                <span className={styles.metaLabel}>Опыт</span>
                <span className={styles.metaValue}>{course.experience}</span>
              </div>
            </td>
          </tr>
          <tr className={styles.metaRow}>
            <td className={styles.metaItem}>
              <div className={styles.metaItemInner}>
                <span className={styles.metaLabel}>Цена</span>
                <span className={styles.metaValue}>{course.price.toFixed(0)} ₽</span>
              </div>
            </td>
            <td className={styles.metaItem}>
              <div className={styles.metaItemInner}>
                <span className={styles.metaLabel}>Бесплатных занятий</span>
                <span className={styles.metaValue}>{course.freeLessons}</span>
              </div>
            </td>
            <td className={styles.metaItem}>
              <div className={styles.metaItemInner}>
                <span className={styles.metaLabel}>Максимум занятий в неделю</span>
                <span className={styles.metaValue}>{course.maxLessons}</span>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
      <div className={styles.description}>
        <p className={styles.descriptionText}>
          {course.about ?? 'Описание отсутствует'}
        </p>
      </div>
    </Card>
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
