import axios from 'axios';

const {
  VITE_PUBLIC_WEB_API,
} = import.meta.env;

export const fetcherInstance = axios.create({
  baseURL: VITE_PUBLIC_WEB_API,
})