import type { ReactNode } from 'react';
import { create } from 'zustand';

export interface ModalEntry {
  id: string;
  title?: string;
  content: ReactNode;
}

interface ModalStore {
  stack: ModalEntry[];
  open: (entry: ModalEntry) => void;
  close: (id: string) => void;
}

export const useModalStore = create<ModalStore>((set) => ({
  stack: [],
  open: (entry) =>
    set((s) => ({
      stack: [...s.stack.filter((m) => m.id !== entry.id), entry],
    })),
  close: (id) =>
    set((s) => ({ stack: s.stack.filter((m) => m.id !== id) })),
}));
