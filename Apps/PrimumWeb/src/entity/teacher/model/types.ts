export interface TeacherProfileDto {
  displayName: string | null;
  about: string | null;
  userId: number;
  isAvailable: boolean;
  level: number;
  rank: string | null;
  experience: number;
  convertionIndex: number | null;
}
