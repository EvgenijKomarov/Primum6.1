import { clsx } from 'clsx';
import type { ButtonHTMLAttributes, MouseEventHandler, PropsWithChildren, ReactElement } from 'react';

import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import { Loader } from '@/shared/ui/Loader';

import styles from './styles.module.css';

export interface IButton extends Omit<ButtonHTMLAttributes<HTMLButtonElement>, 'onClick' | 'type'> {
  type?: ButtonHTMLAttributes<HTMLButtonElement>['type'];
  onClick?: MouseEventHandler<HTMLButtonElement>;
  disabled?: boolean;
  variant?: ButtonTypeEnum;
  size?: ButtonSizeEnum;
  icon?: ReactElement;
  title?: string;
  fullWidth?: boolean;
  isLoading?: boolean;
}

const Button = ({
  type = 'button',
  onClick = () => null,
  disabled = false,
  icon,
  size = ButtonSizeEnum.NORMAL,
  variant = ButtonTypeEnum.PRIMARY,
  fullWidth = false,
  children,
  title,
  isLoading = false,
  ...restProps
}: PropsWithChildren<IButton>) => {
  const isButtonDisabled = disabled || isLoading;

  return (
    <button
      type={type}
      className={clsx(
        styles.button,
        // Variants & States
        variant === ButtonTypeEnum.PRIMARY && (isButtonDisabled ? styles['primary-disabled'] : styles['primary']),
        variant === ButtonTypeEnum.SECONDARY && (isButtonDisabled ? styles['secondary-disabled'] : styles['secondary']),
        variant === ButtonTypeEnum.TEXT && (isButtonDisabled ? styles['text-disabled'] : styles['text']),
        variant === ButtonTypeEnum.ICON && (isButtonDisabled ? styles['icon-disabled'] : styles['icon']),
        // Sizes
        size === ButtonSizeEnum.NORMAL && (isButtonDisabled ? styles['size-normal-disabled'] : styles['size-normal']),
        size === ButtonSizeEnum.MINIMUM && styles['size-minimum'],
        size === ButtonSizeEnum.SMALL && (isButtonDisabled ? styles['size-small-disabled'] : styles['size-small']),
        // Modifiers
        fullWidth && 'w-full'
      )}
      disabled={isButtonDisabled}
      onClick={onClick}
      title={title}
      {...restProps}
    >
      {isLoading ? (
        <>
          <Loader position="static" miniLoader />
          {children && <span className={styles.loadingText}>{children}</span>}
        </>
      ) : (
        <>
          {icon}
          {/* Заменил h3 на span для корректной семантики и отсутствия лишних отступов */}
          {children && <span>{children}</span>}
        </>
      )}
    </button>
  );
};

export default Button;