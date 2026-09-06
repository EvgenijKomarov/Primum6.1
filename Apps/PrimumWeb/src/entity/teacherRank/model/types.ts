export interface TeacherRankDto {
    id: number;
    rank: string;
    level: number;
    requiredExperience: number;
    earningMultiplier: number;
}

export interface TeacherRankDtoPageResult {
  items: TeacherRankDto[] | null;
  totalItemsCount: number;
  totalPages: number;
  currentPage: number;
}