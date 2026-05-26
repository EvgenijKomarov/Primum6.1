import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import { resolve } from "path";
import tailwindcss from '@tailwindcss/vite';
import { env } from 'process';

// https://vite.dev/config/
export default defineConfig(() => {
  return {
    plugins: [react(), tailwindcss()],
    resolve: {
      alias: {
        '@': resolve(__dirname, 'src'),
      },
    },
    css: {
      devSourcemap: false,
    },
    server: {
      host: '127.0.0.1',
      port: 5173,
      proxy: {
        '/config': {
          target: env.VITE_CONFIG_SERVICE_URL || 'http://localhost:5000',
          changeOrigin: true,
        },
        '/api': {
          target: env.VITE_WEB_API_URL ||'http://localhost:5002',
          changeOrigin: true,
        },
      },
    },
    optimizeDeps: {
      force: true,
    },
  };
});