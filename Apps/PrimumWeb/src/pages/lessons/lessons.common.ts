// ── Helpers ──────────────────────────────────────────────────────────────────

import { LessonStatus } from "@/entity/lesson";
import { translateMonth } from "@/features/translation/translation";
import { BadgeTypeEnum } from "@/shared/enums/badge";

export const formatDateLabel = (dateStr: string) => {
  const [y, m, d] = dateStr.split('-').map(Number);
  return `${d} ${translateMonth(m - 1)} ${y}`;
};

export const formatDateTime = (iso: string) => {
  const dt = new Date(iso);
  const d = dt.getDate();
  const m = translateMonth(dt.getMonth());
  const hh = String(dt.getHours()).padStart(2, '0');
  const mm = String(dt.getMinutes()).padStart(2, '0');
  return `${d} ${m}, ${hh}:${mm}`;
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

// ── Status badge ─────────────────────────────────────────────────────────────

export const STATUS_CONFIG: Record<LessonStatus, { label: string; cls: BadgeTypeEnum }> = {
  [LessonStatus.Waiting]:           { label: 'Еще не скоро',   cls: BadgeTypeEnum.Warning },
  [LessonStatus.Warned]:            { label: 'Скоро',     cls: BadgeTypeEnum.Warning  },
  [LessonStatus.Happened]:          { label: 'Прошло',    cls: BadgeTypeEnum.Positive },
  [LessonStatus.Missed]:            { label: 'Пропущено', cls: BadgeTypeEnum.Negative  },
  [LessonStatus.MissedWithoutReason]: { label: 'Пропущено', cls: BadgeTypeEnum.Negative },
};