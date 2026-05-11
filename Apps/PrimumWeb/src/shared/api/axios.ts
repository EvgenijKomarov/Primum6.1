import axios from 'axios';
import { useUserStore } from '@/entity/user';

const {
  VITE_PUBLIC_WEB_API,
} = import.meta.env;

export const fetcherInstance = axios.create({
  baseURL: VITE_PUBLIC_WEB_API,
});

fetcherInstance.interceptors.request.use((config) => {
  const token = useUserStore.getState().token;
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});