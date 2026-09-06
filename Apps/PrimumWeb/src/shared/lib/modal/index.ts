export { useModalStore } from './modalStore.ts';
export type { ModalEntry } from './modalStore.ts';

import { useModalStore } from './modalStore.ts';

export const useModal = () => {
  const open = useModalStore((s) => s.open);
  const close = useModalStore((s) => s.close);
  return { open, close };
};
