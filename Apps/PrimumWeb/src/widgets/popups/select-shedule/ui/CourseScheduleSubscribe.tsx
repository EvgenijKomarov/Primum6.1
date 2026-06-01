import { useState } from 'react';
import useSWR from 'swr';
import { getPublicTeacherSchedules } from '@/entity/teacher';
import { subscribeToCourse } from '@/entity/student';
import { DayOfWeek } from '@/entity/schedule';
import type { TeacherScheduleDto } from '@/entity/schedule';
import type { CourseDto } from '@/entity/course';
import { useFetch } from '@/shared/api/useFetch.ts';
import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';

import styles from './CourseScheduleSubscribe.module.css';
import { Popup } from '@/shared/ui/Popup/Popup';
import { translateException } from '@/features/exception-translation/translate-exception';

const DAY_LABELS: Record<DayOfWeek, string> = {
  [DayOfWeek.Monday]:    'Понедельник',
  [DayOfWeek.Tuesday]:   'Вторник',
  [DayOfWeek.Wednesday]: 'Среда',
  [DayOfWeek.Thursday]:  'Четверг',
  [DayOfWeek.Friday]:    'Пятница',
  [DayOfWeek.Saturday]:  'Суббота',
  [DayOfWeek.Sunday]:    'Воскресенье',
};

const DAY_ORDER: DayOfWeek[] = [
  DayOfWeek.Monday,
  DayOfWeek.Tuesday,
  DayOfWeek.Wednesday,
  DayOfWeek.Thursday,
  DayOfWeek.Friday,
  DayOfWeek.Saturday,
  DayOfWeek.Sunday,
];

interface Props {
  course: CourseDto;
  setSubscribePopupOpen: (open: boolean) => void;
}

export const CourseScheduleSubscribe = ({ course, setSubscribePopupOpen }: Props) => {
  const [selectedSlot, setSelectedSlot] = useState<TeacherScheduleDto | null>(null);
  const [error, setError] = useState<string | null>(null);

  const { data, isLoading } = useSWR(
    ['teacher-public-schedules', course.teacherId],
    async () => (await getPublicTeacherSchedules(course.teacherId)).data,
  );

  const { fetch: doSubscribe, isLoading: isSubmitting } = useFetch(subscribeToCourse);

  const slots = (data?.items ?? []).filter((s) => s.isAvailable);

  const slotsByDay = DAY_ORDER.reduce<Record<DayOfWeek, TeacherScheduleDto[]>>(
    (acc, day) => {
      acc[day] = slots.filter((s) => s.dayOfWeek === day).sort((a, b) => a.time - b.time);
      return acc;
    },
    {} as Record<DayOfWeek, TeacherScheduleDto[]>,
  );

  const handleConfirm = async () => {
    if (!selectedSlot) return;
    setError(null);
    try {
      await doSubscribe(course.id, selectedSlot.id);
    } catch (err: unknown) {
      const msg =
        (err as { response?: { data?: string } })?.response?.data ??
        (err instanceof Error ? err.message : null) ??
        'Не удалось записаться. Попробуйте ещё раз.';
      setError(String(msg));
    }
  };

  const isFree = course.price === 0;
  const priceLabel = isFree ? 'Бесплатно' : `${course.price.toFixed(0)} ₽`;

  return (
    <Popup title="Запись на курс" onClose={() => { setSelectedSlot(null); setError(null); setSubscribePopupOpen(false); }}>
      <div className={styles.content}>
        <div className={styles.courseInfo}>
          <p className={styles.courseName}>{course.name ?? '—'}</p>
          <p className={styles.courseTeacher}>{course.teacherName ?? '—'}</p>
          <div className={styles.courseMeta}>
            <div className={styles.courseMetaItem}>
              <span className={styles.courseMetaLabel}>Уроков</span>
              <span className={styles.courseMetaValue}>{course.maxLessons}</span>
            </div>
            {course.freeLessons > 0 && (
              <div className={styles.courseMetaItem}>
                <span className={styles.courseMetaLabel}>Бесплатно</span>
                <span className={styles.courseMetaValue}>{course.freeLessons}</span>
              </div>
            )}
            <div className={styles.courseMetaItem}>
              <span className={styles.courseMetaLabel}>Цена</span>
              <span className={styles.courseMetaValue}>{priceLabel} / урок</span>
            </div>
          </div>
        </div>

        <p className={styles.sectionLabel}>Выберите удобное время</p>

        {isLoading ? (
          <>
            {[3, 2, 4].map((count, i) => (
              <div key={i} className={styles.skeletonGroup}>
                <div className={styles.skeletonLabel} />
                <div className={styles.skeletonSlots}>
                  {Array.from({ length: count }).map((_, j) => (
                    <div key={j} className={styles.skeletonSlot} />
                  ))}
                </div>
              </div>
            ))}
          </>
        ) : slots.length === 0 ? (
          <div className={styles.empty}>
            <p className={styles.emptyText}>
              Преподаватель пока не добавил доступное время для записи
            </p>
          </div>
        ) : (
          DAY_ORDER.filter((day) => slotsByDay[day].length > 0).map((day) => (
            <div key={day} className={styles.dayGroup}>
              <p className={styles.dayName}>{DAY_LABELS[day]}</p>
              <div className={styles.slotsRow}>
                {slotsByDay[day].map((slot) => (
                  <button
                    key={slot.id}
                    className={styles.slotBtn}
                    onClick={() => setSelectedSlot(slot)}
                  >
                    {slot.time}:00 — {slot.time + 1}:00
                  </button>
                ))}
              </div>
            </div>
          ))
        )}

        {selectedSlot && (<div className={styles.confirm}>
          <div className={styles.sectionLabel}>Подтверждение записи</div>
          <div className={styles.confirmCard}>
            <div className={styles.confirmRow}>
              <span className={styles.confirmLabel}>Курс</span>
              <span className={styles.confirmValue}>{course.name ?? '—'}</span>
            </div>
            <div className={styles.confirmRow}>
              <span className={styles.confirmLabel}>Преподаватель</span>
              <span className={styles.confirmValue}>{course.teacherName ?? '—'}</span>
            </div>
            <div className={styles.confirmRow}>
              <span className={styles.confirmLabel}>День</span>
              <span className={styles.confirmValue}>{DAY_LABELS[selectedSlot.dayOfWeek]}</span>
            </div>
            <div className={styles.confirmRow}>
              <span className={styles.confirmLabel}>Время</span>
              <span className={styles.confirmValue}>
                {selectedSlot.time}:00 — {selectedSlot.time + 1}:00
              </span>
            </div>
            <div className={styles.confirmRow}>
              <span className={styles.confirmLabel}>Стоимость</span>
              <span className={styles.confirmValue}>{priceLabel} / урок</span>
            </div>

            {error && <div className={styles.errorBanner}>{translateException(error)}</div>}

            <div className={styles.confirmActions}>
              <Button
                variant={ButtonTypeEnum.PRIMARY}
                size={ButtonSizeEnum.NORMAL}
                onClick={handleConfirm}
                isLoading={isSubmitting}
              >
                Подтвердить запись
              </Button>
            </div>
          </div>
        </div>)}
      </div>
    </Popup>
  );
};
