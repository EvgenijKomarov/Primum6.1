export interface CourseRankDto {
    id: number;
    rank: string;
    level: number;
    requiredExperience: number;
}

export interface CourseRankDtoPageResult {
  items: CourseRankDto[] | null;
  totalItemsCount: number;
  totalPages: number;
  currentPage: number;
}