import { Popup } from '@/shared/ui/Popup';
import styles from './EnsurancePopup.module.css';
import Button from '@/shared/ui/Button/Button';
import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';

interface EnsuranceProps {
    title?: string;
    description: string;
    onConfirm: () => void;
    onConfirmString?: string;
    onRejectString?: string;
    setPopupOpen: (open: boolean) => void;
}

export const EnsurancePopup = ({ title, description, onConfirm, onConfirmString, onRejectString, setPopupOpen }: EnsuranceProps) => {
  return (
    <Popup
        title={title ?? ""}
        onClose={() => setPopupOpen(false)}
    >
        <div className={styles.ensurancePopup}>
            <p className={styles.description}>
                {description}
            </p>
            <div className={styles.buttons}>
                <Button
                    onClick={onConfirm}
                    variant={ButtonTypeEnum.PRIMARY}
                    size={ButtonSizeEnum.NORMAL}>
                    {onConfirmString ?? "Да"}
                </Button>
                <Button
                    onClick={() => setPopupOpen(false)}
                    variant={ButtonTypeEnum.SECONDARY}
                    size={ButtonSizeEnum.NORMAL}>
                    {onRejectString ?? "Нет"}
                </Button>
            </div>
        </div>
    </Popup>
  );
};