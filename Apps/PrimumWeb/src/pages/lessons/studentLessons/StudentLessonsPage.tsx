import { useState } from 'react';
import {
  useStudentFutureLessons,
  useStudentLessons,
} from '@/entity/lesson';
import type { LessonDto, FutureLessonDto, LessonsByDateDto } from '@/entity/lesson';
import { LessonStatus } from '@/entity/lesson';
import styles from '../lessons.module.css';
import { CalendarIcon, ExternalLinkIcon } from '@/shared/icons/types';
import { TeacherInfo } from '@/widgets/popups/info/teacher-info/TeacherInfo';
import { Gradinginfo } from '@/widgets/popups/info/grading-info/GradingInfo';
import { Card } from '@/shared/ui/Card/Card';
import { translateDayOfWeek } from '@/features/translation/translation';
import { formatDateLabel, formatDateTime, formatTimeSlot, isToday, STATUS_CONFIG } from '../lessons.common';
import { Badge } from '@/shared/ui/Badge/Badge';

export const StatusBadge = ({ status }: { status: LessonStatus }) => {
  const cfg = STATUS_CONFIG[status] ?? STATUS_CONFIG[LessonStatus.Waiting];
  return <Badge text={cfg.label} badgeType={cfg.cls} />;
};

const UpcomingCard = ({ lesson }: { lesson: FutureLessonDto }) => (
  <Card hoverable={true} width={'100%'}>
    <div className={styles.card}>
      <div className={styles.cardLeft}>
        <span className={styles.cardCourseName}>{lesson.courseName}</span>
        <div className={styles.cardMeta}>
          <TeacherInfo teacherId={lesson.teacherId} />
          <span className={styles.cardTime}>{formatTimeSlot(lesson.time)}</span>
        </div>
      </div>
      <div className={styles.cardRight}>
        <span className={`${styles.cardPrice} ${lesson.price === 0 ? styles.cardPriceFree : ''}`}>
          {lesson.price === 0 ? 'Бесплатно' : `${Number(lesson.price).toFixed(0)} ₽`}
        </span>
        <StatusBadge status={lesson.lessonStatus} />
      </div>
    </div>
  </Card>
);

// ── History lesson card ───────────────────────────────────────────────────────

const HistoryCard = ({ lesson }: { lesson: LessonDto }) => (
  <Card hoverable={true} width={'100%'}>
    <div className={styles.card}>
      <div className={styles.cardLeft}>
        <span className={styles.cardCourseName}>{lesson.courseName}</span>
        <div className={styles.cardMeta}>
          <TeacherInfo teacherId={lesson.teacherId} />
          <span className={styles.historyDate}>{formatDateTime(lesson.dateTime)}</span>
        </div>
      </div>
      <div className={styles.cardRight}>
        <Gradinginfo {...lesson} />
        <span className={`${styles.cardPrice} ${lesson.price === 0 ? styles.cardPriceFree : ''}`}>
          {lesson.price === 0 ? 'Бесплатно' : `${Number(lesson.price).toFixed(0)} ₽`}
        </span>
        <StatusBadge status={lesson.lessonStatus} />
        {lesson.lessonLink && (
          <a
            href={lesson.lessonLink}
            target="_blank"
            rel="noopener noreferrer"
            className={styles.linkBtn}
          >
            <ExternalLinkIcon />
            Открыть
          </a>
        )}
      </div>
    </div>
  </Card>
);

// ── Date group ────────────────────────────────────────────────────────────────

const DateGroup = ({ group }: { group: LessonsByDateDto }) => {
  const today = isToday(group.date);
  return (
    <div className={styles.dateGroup}>
      <div className={styles.dateHeading}>
        <span className={styles.dateHeadingDay}>{translateDayOfWeek(group.dayOfWeek)}</span>
        <span className={styles.dateHeadingDate}>{formatDateLabel(group.date)}</span>
        {today && <span className={styles.dateHeadingToday}>Сегодня</span>}
      </div>
      <div className={styles.lessonList}>
        {group.lessons.map((l) => <UpcomingCard key={l.id} lesson={l} />)}
      </div>
    </div>
  );
};

// ── Tab content ───────────────────────────────────────────────────────────────

const UpcomingTab = () => {
  const { groups, isLoading } = useStudentFutureLessons();

  if (isLoading) return (
    <div className={styles.lessonList}>
      {Array.from({ length: 3 }).map((_, i) => <div key={i} className={styles.skeletonCard} />)}
    </div>
  );

  if (groups.length === 0) return (
    <div className={styles.empty}>
      <CalendarIcon />
      <p className={styles.emptyText}>Предстоящих занятий пока нет</p>
    </div>
  );

  return <>{groups.map((g) => <DateGroup key={g.date} group={g} />)}</>;
};

const HistoryTab = () => {
  const { lessons, isLoading } = useStudentLessons();

  if (isLoading) return (
    <div className={styles.lessonList}>
      {Array.from({ length: 5 }).map((_, i) => <div key={i} className={styles.skeletonCard} />)}
    </div>
  );

  if (lessons.length === 0) return (
    <div className={styles.empty}>
      <CalendarIcon />
      <p className={styles.emptyText}>История занятий пуста</p>
    </div>
  );

  return (
    <div className={styles.lessonList}>
      {lessons.map((l) => <HistoryCard key={l.id} lesson={l} />)}
    </div>
  );
};

// ── Page ─────────────────────────────────────────────────────────────────────

type Tab = 'upcoming' | 'history';

export const StudentLessonsPage = () => {
  const [activeTab, setActiveTab] = useState<Tab>('upcoming');

  const { groups } = useStudentFutureLessons();
  const { lessons } = useStudentLessons();

  const upcomingCount = groups.reduce((acc, g) => acc + g.lessons.length, 0);

  return (
    <div className={styles.page}>
      <h1 className={styles.title}>Мои занятия</h1>

      <div className={styles.tabs}>
        <button
          className={`${styles.tab} ${activeTab === 'upcoming' ? styles.tabActive : ''}`}
          onClick={() => setActiveTab('upcoming')}
        >
          Предстоящие
          {upcomingCount > 0 && <span className={styles.tabCount}>{upcomingCount}</span>}
        </button>
        <button
          className={`${styles.tab} ${activeTab === 'history' ? styles.tabActive : ''}`}
          onClick={() => setActiveTab('history')}
        >
          История
          {lessons.length > 0 && <span className={styles.tabCount}>{lessons.length}</span>}
        </button>
      </div>

      {activeTab === 'upcoming' ? <UpcomingTab /> : <HistoryTab />}
    </div>
  );
};
