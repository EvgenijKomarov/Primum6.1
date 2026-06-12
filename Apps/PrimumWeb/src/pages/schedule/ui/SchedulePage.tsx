import { Fragment, useState, useCallback, useMemo } from 'react';
import {
  DayOfWeek,
  useTeacherSchedules,
  createTeacherSchedule,
  deleteTeacherSchedule,
} from '@/entity/schedule';
import type { TeacherScheduleDto } from '@/entity/schedule';
import { Loader } from '@/shared/ui/Loader';

import styles from './SchedulePage.module.css';
import { AbonementInfo } from '@/widgets/popups/info/abonement-info/AbonementInfo';

const DAYS: { label: string; value: DayOfWeek }[] = [
  { label: 'Пн', value: DayOfWeek.Monday },
  { label: 'Вт', value: DayOfWeek.Tuesday },
  { label: 'Ср', value: DayOfWeek.Wednesday },
  { label: 'Чт', value: DayOfWeek.Thursday },
  { label: 'Пт', value: DayOfWeek.Friday },
  { label: 'Сб', value: DayOfWeek.Saturday },
  { label: 'Вс', value: DayOfWeek.Sunday },
];

const HOURS = Array.from({ length: 24 }, (_, i) => i);

const slotKey = (day: DayOfWeek, hour: number) => `${day}-${hour}`;

type SlotStatus = 'empty' | 'available' | 'booked';

const getSlotStatus = (slot: TeacherScheduleDto | undefined): SlotStatus => {
  if (!slot) return 'empty';
  return slot.isAvailable ? 'available' : 'booked';
};

export const SchedulePage = () => {
  const { schedules, isLoading, mutate } = useTeacherSchedules();
  const [loadingKeys, setLoadingKeys] = useState<Set<string>>(new Set());

  const scheduleMap = useMemo(
    () => new Map<string, TeacherScheduleDto>(schedules.map((s) => [slotKey(s.dayOfWeek, s.time), s])),
    [schedules],
  );

  const setKeyLoading = (key: string, loading: boolean) => {
    setLoadingKeys((prev) => {
      const next = new Set(prev);

      if (loading) next.add(key);
      else next.delete(key);

      return next;
    });
  };

  const handleSlotClick = useCallback(
    async (day: DayOfWeek, hour: number) => {
      const key = slotKey(day, hour);
      if (loadingKeys.has(key)) return;

      const existing = scheduleMap.get(key);
      const status = getSlotStatus(existing);

      if (status === 'booked') return;

      setKeyLoading(key, true);
      try {
        if (status === 'empty') {
          await createTeacherSchedule({ dayOfWeek: day, time: hour });
        } else {
          await deleteTeacherSchedule(existing!.id);
        }
        await mutate();
      } finally {
        setKeyLoading(key, false);
      }
    },
    [loadingKeys, scheduleMap, mutate],
  );

  if (isLoading) return <Loader />;

  return (
    <div className={styles.page}>
      <div className={styles.header}>
        <h1 className={styles.title}>Расписание</h1>
      </div>
      <p className={styles.hint}>
        Нажмите на ячейку, чтобы добавить или убрать доступный слот. Слот — 1 час. Занятые ячейки (студент записан) нельзя удалить.
      </p>

      <div className={styles.calendarWrapper}>
        <div className={styles.calendar}>
          {/* Corner */}
          <div className={styles.cornerCell} />

          {/* Day headers */}
          {DAYS.map((d) => (
            <div key={d.value} className={styles.dayHeader}>
              <span className={styles.dayName}>{d.label}</span>
            </div>
          ))}

          {/* Hour rows */}
          {HOURS.map((hour) => (
            <Fragment key={hour}>
              <div className={styles.hourLabel}>
                {hour}:00
              </div>

              {DAYS.map((d) => {
                const key = slotKey(d.value, hour);
                const slot = scheduleMap.get(key);
                const status = getSlotStatus(slot);
                const loading = loadingKeys.has(key);

                const isLastHour = hour === HOURS[HOURS.length - 1];
                const slotCls = [
                  styles.slot,
                  status === 'empty' && styles.slotEmpty,
                  status === 'available' && styles.slotAvailable,
                  //status === 'booked' && styles.slotBooked,
                  loading && styles.slotLoading,
                  isLastHour && styles.slotLastRow,
                ]
                  .filter(Boolean)
                  .join(' ');

                return (
                  <div
                    key={key}
                    className={slotCls}
                    onClick={() => handleSlotClick(d.value, hour)}
                    title={
                      status === 'booked' && slot
                        ? `${slot.studentName ?? 'Студент'} — ${slot.courseName ?? 'курс'}`
                        : status === 'available'
                          ? 'Нажмите, чтобы убрать слот'
                          : 'Нажмите, чтобы добавить слот'
                    }
                  >
                    <div className={styles.slotContent}>
                      {status === 'available' && (
                        <span className={styles.slotDot} />
                      )}
                      {status === 'booked' && slot?.abonementId  && (
                        <AbonementInfo 
                          abonementId={slot.abonementId ?? 1} 
                          badgeStyle={styles.abonementBadge}/>
                      )}
                    </div>
                  </div>
                );
              })}
            </Fragment>
          ))}
        </div>
      </div>

      <div className={styles.legend}>
        <div className={styles.legendItem}>
          <span className={`${styles.legendDot} ${styles.legendDotActive}`} />
          Активный абонемент
        </div>
        <div className={styles.legendItem}>
          <span className={`${styles.legendDot} ${styles.legendDotFreezed}`} />
          Замороженный абонемент
        </div>
      </div>
    </div>
  );
};
