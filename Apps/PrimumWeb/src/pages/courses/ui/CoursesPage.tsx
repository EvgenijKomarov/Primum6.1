import { CreateCourseForm } from '@/features/create-course';
import { ApproveStatus, useTeacherCourses } from '@/entity/course';
import type { CourseDto } from '@/entity/course';
import { useModal } from '@/shared/lib/modal';
import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';
import { Loader } from '@/shared/ui/Loader';

import styles from './CoursesPage.module.css';

const APPROVE_STATUS_LABEL: Record<ApproveStatus, { label: string; cls: string }> = {
  [ApproveStatus.Approved]:                { label: 'Одобрен',          cls: styles.badgeApproved },
  [ApproveStatus.NeedModeratorReview]:     { label: 'На проверке',      cls: styles.badgePending  },
  [ApproveStatus.NeedAdministratorReview]: { label: 'У администратора', cls: styles.badgeReview   },
  [ApproveStatus.NeedManagerReview]:       { label: 'У менеджера',      cls: styles.badgeReview   },
};

const PlusIcon = () => (
  <svg viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
    <path d="M10.75 4.75a.75.75 0 0 0-1.5 0v4.5h-4.5a.75.75 0 0 0 0 1.5h4.5v4.5a.75.75 0 0 0 1.5 0v-4.5h4.5a.75.75 0 0 0 0-1.5h-4.5v-4.5Z" />
  </svg>
);

const BookIcon = () => (
  <svg className={styles.emptyIcon} viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.5" aria-hidden="true">
    <path strokeLinecap="round" strokeLinejoin="round" d="M12 6.042A8.967 8.967 0 0 0 6 3.75c-1.052 0-2.062.18-3 .512v14.25A8.987 8.987 0 0 1 6 18c2.305 0 4.408.867 6 2.292m0-14.25a8.966 8.966 0 0 1 6-2.292c1.052 0 2.062.18 3 .512v14.25A8.987 8.987 0 0 0 18 18a8.967 8.967 0 0 0-6 2.292m0-14.25v14.25" />
  </svg>
);

const CourseCard = ({ course }: { course: CourseDto }) => {
  const status = APPROVE_STATUS_LABEL[course.approveStatus];

  return (
    <div className={styles.card}>
      <div className={styles.cardTop}>
        <h3 className={styles.courseName}>{course.name ?? '—'}</h3>
      </div>

      <div className={styles.badges}>
        <span className={`${styles.badge} ${status.cls}`}>
          <span className={styles.dot} />
          {status.label}
        </span>
        <span className={`${styles.badge} ${course.isActive ? styles.badgeActive : styles.badgeInactive}`}>
          <span className={styles.dot} />
          {course.isActive ? 'Активен' : 'Скрыт'}
        </span>
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
          <span className={styles.metaLabel}>Уроков</span>
          <span className={styles.metaValue}>{course.maxLessons}</span>
        </div>
        <div className={styles.metaItem}>
          <span className={styles.metaLabel}>Бесплатно</span>
          <span className={styles.metaValue}>{course.freeLessons}</span>
        </div>
      </div>
    </div>
  );
};

const MODAL_ID = 'create-course';

export const CoursesPage = () => {
  const { courses, isLoading, mutate } = useTeacherCourses();
  const { open, close } = useModal();

  const handleOpenCreate = () => {
    open({
      id: MODAL_ID,
      title: 'Новый курс',
      content: (
        <CreateCourseForm
          onSuccess={() => { mutate(); close(MODAL_ID); }}
          onCancel={() => close(MODAL_ID)}
        />
      ),
    });
  };

  if (isLoading) return <Loader />;

  return (
    <div className={styles.page}>
      <div className={styles.header}>
        <h1 className={styles.title}>Мои курсы</h1>
        <Button
          variant={ButtonTypeEnum.PRIMARY}
          size={ButtonSizeEnum.NORMAL}
          icon={<PlusIcon />}
          onClick={handleOpenCreate}
        >
          Создать курс
        </Button>
      </div>

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
