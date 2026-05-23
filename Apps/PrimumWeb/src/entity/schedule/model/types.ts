export enum DayOfWeek {
  Sunday = 0,
  Monday = 1,
  Tuesday = 2,
  Wednesday = 3,
  Thursday = 4,
  Friday = 5,
  Saturday = 6,
}

export interface TeacherScheduleDto {
  id: number;
  dayOfWeek: DayOfWeek;
  time: number;
  isAvailable: boolean;
  studentName: string | null;
  studentId: number | null;
  courseName: string | null;
  courseId: number | null;
}

export interface TeacherScheduleDtoPageResult {
  items: TeacherScheduleDto[] | null;
  totalItemsCount: number;
  totalPages: number;
  currentPage: number;
}

export interface TeacherScheduleInputDto {
  time: number;
  dayOfWeek: DayOfWeek;
}
