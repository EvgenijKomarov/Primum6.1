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