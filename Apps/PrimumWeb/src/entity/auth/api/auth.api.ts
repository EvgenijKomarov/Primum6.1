import type { LoginDto, RegisterDto } from '@/entity/auth';
import { fetcherInstance } from '@/shared/api/axios.ts';
import { api } from '@/shared/config/api.ts';

export const login = async (data: LoginDto) => {
  return await fetcherInstance<string>({
    method: 'POST',
    url: api.public.login,
    data,
  });
};

export const register = async (data: RegisterDto) => {
  return await fetcherInstance<string>({
    method: 'POST',
    url: api.public.register,
    data,
  });
};