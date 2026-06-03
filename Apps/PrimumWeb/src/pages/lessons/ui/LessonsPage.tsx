import { useState } from 'react';
import {
  LessonStatus,
  useStudentFutureLessons,
  useStudentLessons,
} from '@/entity/lesson';
import type { LessonDto, FutureLessonDto, LessonsByDateDto } from '@/entity/lesson';

import styles from './LessonsPage.module.css';
import { CalendarIcon, ExternalLinkIcon } from '@/shared/icons/types';
import { BadgeTypeEnum } from '@/shared/enums/badge';
import { Badge } from '@/shared/ui/Badge/Badge';
import { TeacherInfo } from '@/widgets/popups/info/teacher-info/TeacherInfo';

// ── Helpers ──────────────────────────────────────────────────────────────────

const RU_DAYS = ['Воскресенье', 'Понедельник', 'Вторник', 'Среда', 'Четверг', 'Пятница', 'Суббота'];

const RU_MONTHS = ['января', 'февраля', 'марта', 'апреля', 'мая', 'июня',
  'июля', 'августа', 'сентября', 'октября', 'ноября', 'декабря'];

const formatDateLabel = (dateStr: string) => {
  const [y, m, d] = dateStr.split('-').map(Number);
  return `${d} ${RU_MONTHS[m - 1]} ${y}`;
};

const formatDateTime = (iso: string) => {
  const dt = new Date(iso);
  const d = dt.getDate();
  const m = RU_MONTHS[dt.getMonth()];
  const hh = String(dt.getHours()).padStart(2, '0');
  const mm = String(dt.getMinutes()).padStart(2, '0');
  return `${d} ${m}, ${hh}:${mm}`;
};

const formatTimeSlot = (timeStr: string) => {
  const [h] = timeStr.split(':').map(Number);
  return `${h}:00 — ${h + 1}:00`;
};

const isToday = (dateStr: string) => {
  const now = new Date();
  const [y, m, d] = dateStr.split('-').map(Number);
  return now.getFullYear() === y && now.getMonth() + 1 === m && now.getDate() === d;
};

// ── Status badge ─────────────────────────────────────────────────────────────

const STATUS_CONFIG: Record<LessonStatus, { label: string; cls: BadgeTypeEnum }> = {
  [LessonStatus.Waiting]:           { label: 'Еще не скоро',   cls: BadgeTypeEnum.Warning },
  [LessonStatus.Warned]:            { label: 'Скоро',     cls: BadgeTypeEnum.Warning  },
  [LessonStatus.Happened]:          { label: 'Прошло',    cls: BadgeTypeEnum.Positive },
  [LessonStatus.Missed]:            { label: 'Пропущено', cls: BadgeTypeEnum.Negative  },
  [LessonStatus.MissedWithoutReason]: { label: 'Пропущено', cls: BadgeTypeEnum.Negative },
};

const StatusBadge = ({ status }: { status: LessonStatus }) => {
  const cfg = STATUS_CONFIG[status] ?? STATUS_CONFIG[LessonStatus.Waiting];
  return <Badge text={cfg.label} badgeType={cfg.cls} />;
};

// ── Grade circle ─────────────────────────────────────────────────────────────

const GradeCircle = ({ grade }: { grade: number }) => {
  const cls = grade >= 4 ? styles.gradeHigh : grade >= 3 ? styles.gradeMid : styles.gradeLow;
  return <span className={`${styles.gradeBadge} ${cls}`}>{grade}</span>;
};

// ── Upcoming lesson card ──────────────────────────────────────────────────────

const UpcomingCard = ({ lesson }: { lesson: FutureLessonDto }) => (
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
);

// ── History lesson card ───────────────────────────────────────────────────────

const HistoryCard = ({ lesson }: { lesson: LessonDto }) => (
  <div className={styles.card}>
    <div className={styles.cardLeft}>
      <span className={styles.cardCourseName}>{lesson.courseName}</span>
      <div className={styles.cardMeta}>
        <TeacherInfo teacherId={lesson.teacherId} />
        <span className={styles.historyDate}>{formatDateTime(lesson.dateTime)}</span>
      </div>
    </div>
    <div className={styles.cardRight}>
      {lesson.grade != null && <GradeCircle grade={Math.round(lesson.grade)} />}
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
);

// ── Date group ────────────────────────────────────────────────────────────────

const DateGroup = ({ group }: { group: LessonsByDateDto }) => {
  const today = isToday(group.date);
  return (
    <div className={styles.dateGroup}>
      <div className={styles.dateHeading}>
        <span className={styles.dateHeadingDay}>{RU_DAYS[group.dayOfWeek]}</span>
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

export const LessonsPage = () => {
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
