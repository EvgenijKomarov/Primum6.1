export interface IncidentLogDto {
    id: number,
    adminUserId: number,
    dateTime: string,
    adminDisplayName: string,
    description: string,
    isRevisioned: boolean
}

export interface IncidentLogDtoPageResult {
  items: IncidentLogDto[] | null;
  totalItemsCount: number;
  totalPages: number;
  currentPage: number;
}