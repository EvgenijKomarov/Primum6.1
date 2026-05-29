import { fetcherInstance } from '@/shared/api/axios.ts';
import type {
  ConfirmEmailRequest,
  CreateTeacherProfileRequest,
  SendEmailVerificationRequest,
  UserDto,
} from '@/entity/user';
import { api } from '@/shared/config/api.ts';

export const getUserInfo = async () => {
  return await fetcherInstance<UserDto>({
    method: 'GET',
    url: api.user.getUserInfo,
  });
};

export const sendEmailVerification = async (data: SendEmailVerificationRequest) => {
  return await fetcherInstance({
    method: 'POST',
    url: api.user.sendEmailVerification,
    params: data,
  });
};

export const confirmEmail = async (data: ConfirmEmailRequest) => {
  return await fetcherInstance({
    method: 'POST',
    url: api.user.confirmEmail,
    data: data.token,
    headers: { 'Content-Type': 'application/json' }
  });
};

export const createTeacherProfile = async (data: CreateTeacherProfileRequest) => {
  return await fetcherInstance({
    method: 'POST',
    url: api.user.createTeacherProfile,
    data: JSON.stringify(data.aboutTeacher),
    headers: { 'Content-Type': 'application/json' },
  });
};

export const createStudentProfile = async () => {
  return await fetcherInstance({
    method: 'POST',
    url: api.user.createStudentProfile,
  });
};