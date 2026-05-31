import { CreateCourseForm } from '@/features/create-course';
import { ApproveStatus, useTeacherCourses } from '@/entity/course';
import type { CourseDto } from '@/entity/course';
import { useModal } from '@/shared/lib/modal';
import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';
import { Loader } from '@/shared/ui/Loader';

import styles from './CoursesPage.module.css';
import { BookIcon, PlusIcon } from '@/shared/icons/types';

const APPROVE_STATUS_LABEL: Record<ApproveStatus, { label: string; cls: string }> = {
  [ApproveStatus.Approved]:                { label: 'Одобрен',          cls: styles.badgeApproved },
  [ApproveStatus.NeedModeratorReview]:     { label: 'На проверке',      cls: styles.badgePending  },
  [ApproveStatus.NeedAdministratorReview]: { label: 'У администратора', cls: styles.badgeReview   },
  [ApproveStatus.NeedManagerReview]:       { label: 'У менеджера',      cls: styles.badgeReview   },
};

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
