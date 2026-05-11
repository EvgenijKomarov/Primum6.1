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
  title?: string | undefined;
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
  title = undefined,
  isLoading = false,
  ...restProps
}: PropsWithChildren<IButton>) => {
  const isButtonDisabled = disabled || isLoading;

  return (
    <button
      type={type}
      className={clsx([
        styles.button,
        variant === ButtonTypeEnum.PRIMARY && !isButtonDisabled && styles['primary'],
        variant === ButtonTypeEnum.SECONDARY && !isButtonDisabled && styles['secondary'],
        variant === ButtonTypeEnum.TEXT && !isButtonDisabled && styles['text'],
        variant === ButtonTypeEnum.ICON && !isButtonDisabled && styles['icon'],
        variant === ButtonTypeEnum.PRIMARY && isButtonDisabled && styles['primary-disabled'],
        variant === ButtonTypeEnum.SECONDARY && isButtonDisabled && styles['secondary-disabled'],
        variant === ButtonTypeEnum.TEXT && isButtonDisabled && styles['text-disabled'],
        variant === ButtonTypeEnum.ICON && isButtonDisabled && styles['icon-disabled'],
        size === ButtonSizeEnum.NORMAL && !isButtonDisabled && styles['size-normal'],
        size === ButtonSizeEnum.MINIMUM && !isButtonDisabled && styles['size-minimum'],
        size === ButtonSizeEnum.SMALL && !isButtonDisabled && styles['size-small'],
        size === ButtonSizeEnum.NORMAL && isButtonDisabled && styles['size-normal-disabled'],
        size === ButtonSizeEnum.SMALL && isButtonDisabled && styles['size-small-disabled'],
        fullWidth && 'w-full',
      ])}
      disabled={isButtonDisabled}
      onClick={onClick}
      title={title && title}
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
          {children}
        </>
      )}
    </button>
  );
};

export default Button;
