export interface CourseDto {
  id: number;
  name: string | null;
  teacherName: string | null;
  courseThemeName: string | null;
  about: string | null;
  courseThemeId: number;
  teacherId: number;
  price: number;
  maxLessons: number;
  freeLessons: number;
  teacherAbout: string | null;
  isActive: boolean;
  level: number;
  rank: string | null;
  isAvailable: boolean;
  onCheck: boolean;
}

export interface CourseDtoPageResult {
  items: CourseDto[] | null;
  totalItemsCount: number;
  totalPages: number;
  currentPage: number;
}

export interface CourseInputDto {
  name: string | null;
  description: string | null;
  price: number;
  freeLessons: number;
  maxLessons: number;
  courseThemeId: number;
}
