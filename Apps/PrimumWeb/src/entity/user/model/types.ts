export interface UserDto {
  id: number;
  email: string | null;
  name: string | null;
  surname: string | null;
  patronymic: string | null;
  displayName: string | null;
  isBanned: boolean;
  mailConfirmed: boolean;
  isApprovedStudent: boolean | null;
  isApprovedTeacher: boolean | null;
  isAdmin: boolean | null;
  isAvailable: boolean;
}

export interface UserDtoPageResult {
  items: UserDto[] | null;
  totalItemsCount: number;
  totalPages: number;
  currentPage: number;
}

export interface SendEmailVerificationRequest {
  correctiveMail?: string;
}

export interface ConfirmEmailRequest {
  token: string;
}

export interface CreateTeacherProfileRequest {
  about: string;
  inn: string,
  phone: string,
  accountNumber: string,
  bankBIC: string
}