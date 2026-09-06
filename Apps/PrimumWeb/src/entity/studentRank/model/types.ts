export interface StudentRankDto {
    id: number;
    rank: string;
    level: number;
    requiredExperience: number;
    coinDiscount: number;
}

export interface StudentRankDtoPageResult {
  items: StudentRankDto[] | null;
  totalItemsCount: number;
  totalPages: number;
  currentPage: number;
}