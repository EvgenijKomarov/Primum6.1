
export interface ChatSign {
  realizationTag: string;
  chatId: number;
  username: string;
}

export interface ChatSignPageResult {
  items: ChatSign[] | null;
  totalItemsCount: number;
  totalPages: number;
  currentPage: number;
}