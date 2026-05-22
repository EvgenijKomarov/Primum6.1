export const resolveDisplayName = (user: { displayName?: string | null; name?: string | null; surname?: string | null }): string => {
  if (user.displayName) return user.displayName;
  const parts = [user.name, user.surname].filter(Boolean);
  return parts.length > 0 ? parts.join(' ') : 'Profile';
};