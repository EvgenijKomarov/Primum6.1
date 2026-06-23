import { translateMonth } from "@/features/translation/translation";

export const formatDateLabel = (dateStr: string) => {
  const [y, m, d] = dateStr.split('-').map(Number);
  return `${d} ${translateMonth(m - 1)} ${y}`;
};

export const formatTimeSlot = (timeStr: string) => {
  const [h] = timeStr.split(':').map(Number);
  return `${h}:00 — ${h + 1}:00`;
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