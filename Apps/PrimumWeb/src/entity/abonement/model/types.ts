export enum AbonementStatus {
  Active = 0,
  Freezed = 1,
  Deleted = 2,
}

export enum AbonementInputStatus {
  Activate = 0,
  Freeze = 1,
}

export interface AbonementDtoPageResult {
  items: AbonementDto[] | null;
  totalItemsCount: number;
  totalPages: number;
  currentPage: number;
}

export interface AbonementDto {
  id: number;
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