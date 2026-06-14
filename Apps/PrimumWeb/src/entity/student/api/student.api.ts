import { fetcherInstance } from '@/shared/api/axios.ts';
import { api } from '@/shared/config/api.ts';
import type { StudentProfileDto, PaymentResponse } from '@/entity/student';

export const getStudentProfile = async () => {
  return await fetcherInstance<StudentProfileDto>({
    method: 'GET',
    url: api.student.getProfile,
  });
};

export const subscribeToCourse = async (courseId: number, teacherScheduleId: number) => {
  return await fetcherInstance<number>({
    method: 'POST',
    url: `${api.student.subscribe}/${courseId}/${teacherScheduleId}`,
  });
};

export const topupStudentBallance = async (amount: number) => {
  return await fetcherInstance<PaymentResponse>({
    method: 'POST',
    url: api.studentBalance.topup,
    params: {amount: amount}
  })
}

export const withdrawnStudentBallance = async (amount: number) => {
  return await fetcherInstance<string>({
    method: 'POST',
    url: api.studentBalance.withdrawn,
    params: {amount: amount}
  })
}
