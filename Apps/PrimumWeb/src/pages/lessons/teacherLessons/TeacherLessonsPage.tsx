import { useTeacherFutureLessons } from "@/entity/lesson/model/useTeacherFutureLessons";
import { useTeacherLessons } from "@/entity/lesson/model/useTeacherLessons";
import { useState } from "react";
import styles from '../lessons.module.css';
import { CalendarIcon, ExternalLinkIcon } from "@/shared/icons/types";
import type { FutureLessonDto, LessonDto, LessonsByDateDto } from "@/entity/lesson";
import { Card } from "@/shared/ui/Card/Card";
import { formatDateLabel, formatDateTime, formatTimeSlot, isToday } from "../lessons.common";
import { translateDayOfWeek } from "@/features/translation/translation";
import { AbonementInfo } from "@/widgets/popups/info/abonement-info/AbonementInfo";
import { Gradinginfo } from "@/widgets/popups/info/grading-info/GradingInfo";
import { StatusBadge } from "../studentLessons/StudentLessonsPage";
import { GradingPopup } from "@/widgets/popups/grading-popup/GradingPopup";

const UpcomingCard = ({ lesson }: { lesson: FutureLessonDto }) => (
  <Card hoverable={true} width={'100%'}>
    <div className={styles.card}>
      <div className={styles.cardLeft}>
        <span className={styles.cardCourseName}>{lesson.courseName}</span>
        <div className={styles.cardMeta}>
          <AbonementInfo abonementId={lesson.abonementId} />
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

const HistoryCard = ({ lesson, onSubmit }: { lesson: LessonDto, onSubmit: () => void }) => (
  <Card hoverable={true} width={'100%'}>
    <div className={styles.card}>
      <div className={styles.cardLeft}>
        <span className={styles.cardCourseName}>{lesson.courseName}</span>
        <div className={styles.cardMeta}>
          <AbonementInfo abonementId={lesson.abonementId} />
          <span className={styles.historyDate}>{formatDateTime(lesson.dateTime)}</span>
        </div>
      </div>
      <div className={styles.cardRight}>
        {!lesson.finalGrade && lesson.lessonLink !== '' ? (
            <GradingPopup lessonId={lesson.id} onSubmit={onSubmit}/>
          ) : (
            <Gradinginfo {...lesson} />
          )}
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
  const { groups, isLoading } = useTeacherFutureLessons();

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
  const { lessons, isLoading, mutate } = useTeacherLessons();

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
      {lessons.map((l) => <HistoryCard key={l.id} lesson={l} onSubmit={ ()=>{mutate();} }/>)}
    </div>
  );
};

// ── Page ─────────────────────────────────────────────────────────────────────

type Tab = 'upcoming' | 'history';

export const TeacherLessonsPage = () => {
  const [activeTab, setActiveTab] = useState<Tab>('upcoming');

  const { groups } = useTeacherFutureLessons();
  const { lessons } = useTeacherLessons();

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
