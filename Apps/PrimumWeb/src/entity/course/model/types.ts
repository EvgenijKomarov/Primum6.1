export interface CourseDtoLite {
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
}

export interface CourseDto extends CourseDtoLite {
  onCheck: boolean;
  experience: number;
  referalLink: string;
}

export interface CourseDtoLitePageResult {
  items: CourseDtoLite[] | null;
  totalItemsCount: number;
  totalPages: number;
  currentPage: number;
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
