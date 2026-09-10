export enum LessonStatus {
  Waiting = 0,
  Warned = 1,
  Happened = 2,
  Missed = 3,
  MissedWithoutReason = 4,
  MissedDueToException = 5,
  Cancelled = 6,
  MissedByValidReason = 7,
}

export enum LessonReportStatus {
  Ok = 0,

  TeacherHaventConnected = 2,
  StudentHaventConnected = 3,

  TeacherInappropriateContent = 4,
  StudentInappropriateContent = 5,

  TeacherBadBehavior = 6,
  StudentBadBehavior = 7,
}

export interface LessonDto extends Grading {
  id: number;
  courseName: string;
  courseId: number;
  teacherDisplayName: string;
  teacherId: number;
  studentDisplayName: string;
  studentId: number;
  abonementId: number;
  isReferal: boolean;
  price: number;
  lessonStatus: LessonStatus;
  dateTime: string;
  lessonLink?: string;
  teacherEarning?: number;
  isReported: boolean;
  isWorkoff: boolean;
}

export interface Grading {
  homeworkGrade: number | null;
  lessonActivityGrade: number | null;
  repetitionOfMaterialGrade: number | null;
  studyInitiativeGrade: number | null;
  finalGrade: number | null;
}

export interface GradingInputDto {
  homeworkGrade: number;
  lessonActivityGrade: number;
  repetitionOfMaterialGrade: number;
  studyInitiativeGrade: number;
}

export interface LessonDtoPageResult {
  items: LessonDto[] | null;
  totalItemsCount: number;
  totalPages: number;
  currentPage: number;
}

export interface FutureLessonDto {
  id: number;
  courseName: string;
  courseId: number;
  teacherDisplayName: string;
  teacherId: number;
  studentDisplayName: string;
  studentId: number;
  abonementId: number;
  isReferal: boolean;
  price: number;
  lessonStatus: LessonStatus;
  time: string;
  teacherEarning?: number;
  isAbonementActive: boolean;
  isReported: boolean;
  isWorkoff: boolean;
}

export interface LessonsByDateDto {
  date: string;
  dayOfWeek: number;
  lessons: FutureLessonDto[];
}

export interface LessonsByDateDtoPageResult {
  items: LessonsByDateDto[] | null;
  totalItemsCount: number;
  totalPages: number;
  currentPage: number;
}
