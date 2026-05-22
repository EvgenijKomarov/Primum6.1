export interface CourseThemeDto {
  id: number;
  themeName: string | null;
  isActive: boolean;
}

export interface CourseThemeDtoPageResult {
  items: CourseThemeDto[] | null;
  totalItemsCount: number;
  totalPages: number;
  currentPage: number;
}
