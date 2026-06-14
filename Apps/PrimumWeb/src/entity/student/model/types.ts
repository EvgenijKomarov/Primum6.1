export interface StudentProfileDto {
  displayName: string | null;
  userId: number;
  coins: number;
  rating: number | null;
  level: number;
  cash: number;
  rank: string | null;
  experience: number;
}

export interface PaymentResponse {
  error: string | null,
  success: boolean,
  url: string | null
}