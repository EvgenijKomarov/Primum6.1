import axios from 'axios';
import { useUserStore } from '@/entity/user';
import { FetchError } from './fetchError';

export const fetcherInstance = axios.create();

fetcherInstance.interceptors.request.use((config) => {
  config.baseURL = '/api';
  const token = useUserStore.getState().token;
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

fetcherInstance.interceptors.response.use(
  (response) => response,
  (error) => Promise.reject(
    new FetchError(
      error.response?.data?.error ?? error.message,
      error.response?.data ?? null,
    )
  )
);