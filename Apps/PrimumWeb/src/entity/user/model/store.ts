import { create } from 'zustand';
import { persist } from 'zustand/middleware';
import type { UserDto } from './types.ts';

interface UserStore {
  token: string | null;
  user: UserDto | null;
  setToken: (token: string) => void;
  setUser: (user: UserDto) => void;
  clear: () => void;
}

export const useUserStore = create<UserStore>()(
  persist(
    (set) => ({
      token: null,
      user: null,
      setToken: (token) => set({ token }),
      setUser: (user) => set({ user }),
      clear: () => set({ token: null, user: null }),
    }),
    {
      name: 'auth',
      partialize: (state) => ({ token: state.token }),
    }
  )
);
