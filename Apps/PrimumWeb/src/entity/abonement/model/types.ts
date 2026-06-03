import { BadgeTypeEnum } from "@/shared/enums/badge";

export enum AbonementStatus {
  Active = 0,
  Freezed = 1,
  Deleted = 2,
}

export const ABON_STATUS_CONFIG: Record<AbonementStatus, { label: string; cls: BadgeTypeEnum }> = {
  [AbonementStatus.Active]:           { label: 'Активен',   cls: BadgeTypeEnum.Positive },
  [AbonementStatus.Freezed]:           { label: 'Активен',   cls: BadgeTypeEnum.Warning },
  [AbonementStatus.Deleted]:           { label: 'Активен',   cls: BadgeTypeEnum.Negative},
}

export interface AbonementDto {
  studentDisplayName: string;
  studentId: number;
  teacherDisplayName: string;
  teacherId: number;
  courseName: string;
  courseId: number | null;
  courseThemeName: string;
  courseThemeId: number;
  pricePerLesson: number;
  rating: number | null;
  abonementStatus: AbonementStatus;
}