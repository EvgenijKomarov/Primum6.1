import translations from './translations.json';

type TranslationKey = keyof typeof translations;

const GENERIC_ERROR = 'Что-то пошло не так. Попробуйте ещё раз';
const NETWORK_ERROR = 'Нет связи с сервером. Проверьте интернет и попробуйте ещё раз';

// Технические сообщения axios/браузера пользователю не показываем
const isTechnicalMessage = (key: string) =>
  /^Request failed with status code \d+$/.test(key) || key.startsWith('{') || key.startsWith('Unhandled exception');

export function translateException(key: string): string {
  const text = translations[key as TranslationKey];
  if (text) return text;

  if (key === 'Network Error' || key.startsWith('timeout of')) return NETWORK_ERROR;
  if (isTechnicalMessage(key)) return GENERIC_ERROR;

  return key;
}
