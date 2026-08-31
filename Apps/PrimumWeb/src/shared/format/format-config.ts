import type { DayOfWeek } from "@/entity/schedule";
import { translateMonth } from "@/features/translation/translation";

export const formatDateLabel = (dateStr: string) => {
  const [y, m, d] = dateStr.split('-').map(Number);
  return `${d} ${translateMonth(m - 1)} ${y}`;
};

export const formatTimeSlot = (timeStr: string) => {
  const [h, m = 0, s = 0] = timeStr.split(':').map(Number);

  const start = new Date(Date.UTC(1970, 0, 1, h, m, s));
  const end = new Date(start.getTime() + 60 * 60 * 1000);

  const fmt = (dt: Date) =>
    `${dt.getHours()}:${String(dt.getMinutes()).padStart(2, '0')}`;

  return `${fmt(start)} — ${fmt(end)}`;
};

export const isToday = (dateStr: string) => {
  const now = new Date();
  const [y, m, d] = dateStr.split('-').map(Number);
  return now.getFullYear() === y && now.getMonth() + 1 === m && now.getDate() === d;
};

export const formatDateTime = (iso: string) => {
  const dt = new Date(iso);
  const d = dt.getDate();
  const m = translateMonth(dt.getMonth());
  const hh = String(dt.getHours()).padStart(2, '0');
  const mm = String(dt.getMinutes()).padStart(2, '0');
  return `${d} ${m}, ${hh}:${mm}`;
};

/** Конвертирует UTC день/час в локальный день/час */
export const utcToLocal = (utcDay: number, utcHour: number) => {
  // 2024-01-01 был понедельник. Используем его как опорную неделю.
  const date = new Date(Date.UTC(2024, 0, utcDay, utcHour));
  const localDayJs = date.getDay(); // 0=Вс, 1=Пн, ..., 6=Сб
  const localDay = (localDayJs === 0 ? 7 : localDayJs) as DayOfWeek;
  const localHour = date.getHours();
  return { localDay, localHour };
};

/** Конвертирует локальный день/час в UTC день/час для отправки на бэкенд */
export const localToUtc = (localDay: number, localHour: number) => {
  // Создаем дату в локальном часовом поясе браузера
  const date = new Date(2024, 0, localDay, localHour);
  const utcDayJs = date.getUTCDay(); // 0=Вс, 1=Пн, ..., 6=Сб
  const utcDay = (utcDayJs === 0 ? 7 : utcDayJs) as DayOfWeek;
  const utcHour = date.getUTCHours();
  return { utcDay, utcHour };
};
