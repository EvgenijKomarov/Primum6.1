// Упрощённая проверка формата: окончательную проверку делает сервер
export const EMAIL_PATTERN = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

// Телефоны пишут первую букву заглавной и добавляют пробел после автоподстановки
export const normalizeEmail = (email: string) => email.trim().toLowerCase();

// Атрибуты поля почты: без автозаглавной буквы и автокоррекции на мобильных клавиатурах
export const emailInputProps = {
  type: 'email',
  inputMode: 'email',
  autoCapitalize: 'none',
  autoCorrect: 'off',
  spellCheck: false,
} as const;
