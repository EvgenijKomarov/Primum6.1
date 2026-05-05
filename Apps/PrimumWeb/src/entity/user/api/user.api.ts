import { fetcherInstance } from '@/shared/api/axios.ts';
import type {
  ConfirmEmailRequest,
  CreateTeacherProfileRequest,
  SendEmailVerificationRequest,
  UserDto,
} from '@/entity/user';
import { userApiConfig } from '@/entity/user/config';

export const getUserInfo = async () => {
  return await fetcherInstance<UserDto>({
    method: 'GET',
    url: userApiConfig.getUserInfo,
  });
};

export const sendEmailVerification = async (data: SendEmailVerificationRequest) => {
  return await fetcherInstance({
    method: 'POST',
    url: userApiConfig.sendEmailVerification,
    params: data,
  });
};

export const confirmEmail = async (data: ConfirmEmailRequest) => {
  return await fetcherInstance({
    method: 'POST',
    url: userApiConfig.confirmEmail,
    data: data.token,
  });
};

export const createTeacherProfile = async (data: CreateTeacherProfileRequest) => {
  return await fetcherInstance({
    method: 'POST',
    url: userApiConfig.createTeacherProfile,
    data: data.aboutTeacher,
  });
};

export const createStudentProfile = async () => {
  return await fetcherInstance({
    method: 'POST',
    url: userApiConfig.createStudentProfile,
  });
};