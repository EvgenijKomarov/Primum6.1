import translations from './translations.json';

type TranslationKey = keyof typeof translations;

export function translateException(key: string): string {
  const text = translations[key as TranslationKey];
  console.log('translateException', { key, text });
  
  if (!text) {
    return key;
  }
  
  return text;
}