import { useModalStore } from '@/shared/lib/modal';
import { Modal } from '@/shared/ui/Modal';

export const ModalRoot = () => {
  const stack = useModalStore((s) => s.stack);
  const close = useModalStore((s) => s.close);

  return (
    <>
      {stack.map(({ id, title, content }) => (
        <Modal key={id} open onClose={() => close(id)} title={title}>
          {content}
        </Modal>
      ))}
    </>
  );
};
