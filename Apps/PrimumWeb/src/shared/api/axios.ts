import axios from 'axios';
import { useUserStore } from '@/entity/user';

export const fetcherInstance = axios.create();

fetcherInstance.interceptors.request.use((config) => {
  config.baseURL = '/api';
  const token = useUserStore.getState().token;
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});