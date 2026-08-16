export interface AdminProfileDto {
    userId: number;
    displayName: string;
    status: string;
    permissions: Record<string, boolean>;
}

export interface AdminProfileDtoPageResult {
  items: AdminProfileDto[] | null;
  totalItemsCount: number;
  totalPages: number;
  currentPage: number;
}