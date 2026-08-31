import { useMemo, useState } from 'react';
import useSWR from 'swr';
import { getPublicTeacherSchedules } from '@/entity/teacher';
import { subscribeToCourse } from '@/entity/student';
import { DayOfWeek } from '@/entity/schedule';
import type { TeacherScheduleDto } from '@/entity/schedule';
import type { CourseDtoLite } from '@/entity/course';
import { useFetch } from '@/shared/api/useFetch.ts';
import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';

import styles from './CourseScheduleSubscribe.module.css';
import { Popup } from '@/shared/ui/Popup/Popup';
import { translateException } from '@/features/exception-translation/translate-exception';
import { translateDayOfWeek } from '@/features/translation/translation';
import { utcToLocal } from '@/shared/format/format-config';

const DAY_ORDER: DayOfWeek[] = [
  DayOfWeek.Monday,
  DayOfWeek.Tuesday,
  DayOfWeek.Wednesday,
  DayOfWeek.Thursday,
  DayOfWeek.Friday,
  DayOfWeek.Saturday,
  DayOfWeek.Sunday,
];

type LocalSlot = TeacherScheduleDto & {
  localDay: DayOfWeek;
  localHour: number;
};

interface Props {
  course: CourseDtoLite;
  setSubscribePopupOpen: (open: boolean) => void;
  onSubscribe?: () => void;
}

export const CourseScheduleSubscribe = ({ course, onSubscribe, setSubscribePopupOpen }: Props) => {
  const [selectedSlot, setSelectedSlot] = useState<LocalSlot | null>(null);
  const [error, setError] = useState<string | null>(null);

  const { data, isLoading } = useSWR(
    ['teacher-public-schedules', course.teacherId],
    async () => (await getPublicTeacherSchedules(course.teacherId)).data,
  );

  const { fetch: doSubscribe, isLoading: isSubmitting } = useFetch(subscribeToCourse);

  const localSlots = useMemo<LocalSlot[]>(() => {
    const rawSlots = (data?.items ?? []).filter((s) => s.isAvailable);
    return rawSlots.map((slot) => {
      const { localDay, localHour } = utcToLocal(slot.dayOfWeek, slot.time);
      return { ...slot, localDay, localHour };
    });
  }, [data]);

  // 2. Группируем и сортируем слоты уже по локальному дню и часу
  const slotsByDay = useMemo(() => {
    return DAY_ORDER.reduce<Record<DayOfWeek, LocalSlot[]>>(
      (acc, day) => {
        acc[day] = localSlots
          .filter((s) => s.localDay === day)
          .sort((a, b) => a.localHour - b.localHour);
        return acc;
      },
      {} as Record<DayOfWeek, LocalSlot[]>
    );
  }, [localSlots]);

  const handleConfirm = async () => {
    if (!selectedSlot) return;
    setError(null);
    try {
      await doSubscribe(course.id, selectedSlot.id);
      if (onSubscribe) { onSubscribe(); }
      setSubscribePopupOpen(false);
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

  const formatHour = (h: number) => String(h).padStart(2, '0');

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
        ) : localSlots.length === 0 ? (
          <div className={styles.empty}>
            <p className={styles.emptyText}>
              Преподаватель пока не добавил доступное время для записи
            </p>
          </div>
        ) : (
          DAY_ORDER.filter((day) => slotsByDay[day].length > 0).map((day) => (
            <div key={day} className={styles.dayGroup}>
              <p className={styles.dayName}>{translateDayOfWeek(day)}</p>
              <div className={styles.slotsRow}>
                {slotsByDay[day].map((slot) => {
                  const startHour = formatHour(slot.localHour);
                  const endHour = formatHour((slot.localHour + 1) % 24);
                  const isSelected = selectedSlot?.id === slot.id;

                  return (
                    <button
                      key={slot.id}
                      className={`${styles.slotBtn} ${isSelected ? styles.slotBtnSelected : ''}`}
                      onClick={() => setSelectedSlot(slot)}
                    >
                      {startHour}:00 — {endHour}:00
                    </button>
                  );
                })}
              </div>
            </div>
          ))
        )}

        {selectedSlot && (
          <div className={styles.confirm}>
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
                <span className={styles.confirmValue}>{translateDayOfWeek(selectedSlot.localDay)}</span>
              </div>
              <div className={styles.confirmRow}>
                <span className={styles.confirmLabel}>Время</span>
                <span className={styles.confirmValue}>
                  {formatHour(selectedSlot.localHour)}:00 — {formatHour((selectedSlot.localHour + 1) % 24)}:00
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
          </div>)
        }
      </div>
    </Popup>
  );
};
