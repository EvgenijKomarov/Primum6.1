interface RuntimeConfig {
  telegramUrl?: string;
  maxUrl?: string;
  vkUrl?: string;
}

declare global {
  interface Window {
    __RUNTIME_CONFIG__?: RuntimeConfig;
  }
}

const runtime = window.__RUNTIME_CONFIG__ ?? {};

// Значения из контейнера (runtime-config.js) важнее вшитых при сборке: образ собирается в CI без них
export const botLinks = {
  telegram: runtime.telegramUrl || import.meta.env.VITE_TELEGRAM_URL || '',
  max: runtime.maxUrl || import.meta.env.VITE_MAX_URL || '',
  vk: runtime.vkUrl || import.meta.env.VITE_VK_URL || '',
};
