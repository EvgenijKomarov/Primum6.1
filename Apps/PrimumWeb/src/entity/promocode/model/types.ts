export interface PromocodeDto {
    id: number;
    studentId?: number;
    code?: string;
    coinsPrice: number;
    title: string;
    description: string;
    isAvailable: boolean;
}

export interface PromocodeDtoPageResult {
  items: PromocodeDto[] | null;
  totalItemsCount: number;
  totalPages: number;
  currentPage: number;
}