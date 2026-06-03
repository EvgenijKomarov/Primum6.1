export enum AbonementStatus {
  Active = 0,
  Freezed = 1,
  Deleted = 2,
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