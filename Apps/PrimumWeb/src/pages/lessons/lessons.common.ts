// ── Helpers ──────────────────────────────────────────────────────────────────

import { LessonStatus } from "@/entity/lesson";
import { BadgeTypeEnum } from "@/shared/enums/badge";

// ── Status badge ─────────────────────────────────────────────────────────────

export const STATUS_CONFIG: Record<LessonStatus, { label: string; cls: BadgeTypeEnum }> = {
  [LessonStatus.Waiting]:           { label: 'Еще не скоро',   cls: BadgeTypeEnum.Warning },
  [LessonStatus.Warned]:            { label: 'Скоро',     cls: BadgeTypeEnum.Warning  },
  [LessonStatus.Happened]:          { label: 'Прошло',    cls: BadgeTypeEnum.Positive },
  [LessonStatus.Missed]:            { label: 'Пропущено', cls: BadgeTypeEnum.Negative  },
  [LessonStatus.MissedWithoutReason]: { label: 'Пропущено', cls: BadgeTypeEnum.Negative },
  [LessonStatus.MissedDueToException]: { label: 'Техническая ошибка', cls: BadgeTypeEnum.Negative },
  [LessonStatus.Freezed]:          { label: 'Заморожен',    cls: BadgeTypeEnum.Warning },
  [LessonStatus.MissedDueToFreezing]:          { label: 'Пропущено из-за заморозки',    cls: BadgeTypeEnum.Negative },
};