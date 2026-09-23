/// <reference types="vite/client" />

interface ImportMetaEnv {
  readonly VITE_CONFIG_SERVICE_URL: string;
  readonly VITE_TELEGRAM_URL?: string;
  readonly VITE_MAX_URL?: string;
  readonly VITE_VK_URL?: string;
}

interface ImportMeta {
  readonly env: ImportMetaEnv;
}
