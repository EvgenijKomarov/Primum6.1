import React, {
  type ChangeEvent,
  type FocusEvent,
  type FormEvent,
  type HTMLInputTypeAttribute,
  type InputHTMLAttributes,
  type KeyboardEventHandler,
  type ReactElement,
  forwardRef,
  useEffect,
  useId,
  useImperativeHandle,
  useRef,
} from 'react';

// import CloseIcon from '@/shared/assets/icons/CloseIcon';
// import { Label } from '@/shared/ui/formFields/Label';

interface InputProps extends Omit<
  InputHTMLAttributes<HTMLInputElement>,
  'value' | 'onChange' | 'onFocus' | 'onBlur' | 'type'
> {
  name?: string;
  id?: string;
  height?: string;
  width?: string;
  value: string | number;
  label?: string;
  type?: HTMLInputTypeAttribute;
  placeholder?: string;
  disabled?: boolean;
  autoFocus?: boolean;
  clearNone?: boolean;
  onChange: (value: string) => void;
  onFocus?: (event: FocusEvent<HTMLInputElement>) => void;
  onBlur?: (event: FocusEvent<HTMLInputElement>) => void;
  onKeyDown?: KeyboardEventHandler<HTMLInputElement>;
  icon?: ReactElement;
  constantText?: string;
  max?: number;
  min?: number;
  minLength?: number;
  maxLength?: number;
  onPaste?: React.ClipboardEventHandler<HTMLInputElement>;
  isValid?: boolean;
  onBeforeInput?: (event: FormEvent<HTMLInputElement>) => void;
  dataAutomationId?: string;
  error?: string;
}

export const Input = forwardRef<HTMLInputElement, InputProps>(
  (
    {
      name,
      id,
      height = '40px',
      width = 'auto',
      value,
      type = 'text',
      placeholder,
      disabled,
      autoFocus,
      onChange,
      onFocus,
      onBlur,
      onKeyDown,
      label,
      icon,
      constantText,
      min,
      max,
      minLength,
      maxLength,
      onPaste,
      onBeforeInput,
      dataAutomationId,
      error,
      ...restProps
    }: InputProps,
    ref,
  ) => {
    const inputRef = useRef<HTMLInputElement | null>(null);
    const generatedId = useId();
    const inputId = id ?? generatedId;
    const errorId = `${inputId}-error`;

    useImperativeHandle(ref, () => inputRef.current as HTMLInputElement);

    useEffect(() => {
      if (autoFocus && inputRef.current) inputRef.current.focus();
    }, [autoFocus]);

    const onChangeHandler = (e: ChangeEvent<HTMLInputElement>) => {
      onChange(e.target.value);
    };

    const onFocusHandler = (event: FocusEvent<HTMLInputElement>) => {
      if (onFocus) onFocus(event);
    };

    const onBlurHandler = (event: FocusEvent<HTMLInputElement>) => {
      if (onBlur) onBlur(event);
    };

    // TODO: Label, reset
    return (
      <div
        style={{ width: `${width}` }}
      >
        {label && <label htmlFor={inputId}>{label}</label>}
        {/*{label && <Label text={label} required={required} />}*/}
        {icon && <div>{icon}</div>}
        {constantText && (
          <div>{constantText}</div>
        )}
        <input
          ref={inputRef}
          name={name}
          id={inputId}
          height={height}
          value={value}
          type={type}
          disabled={disabled}
          placeholder={placeholder}
          onChange={onChangeHandler}
          onFocus={onFocusHandler}
          onBlur={onBlurHandler}
          onKeyDown={onKeyDown}
          max={max}
          min={min}
          minLength={minLength}
          maxLength={maxLength}
          onPaste={onPaste}
          onBeforeInput={onBeforeInput}
          data-automationid={dataAutomationId}
          aria-invalid={error ? true : undefined}
          aria-describedby={error ? errorId : undefined}
          {...restProps}
        />
        {error && (
          <span id={errorId} role="alert" style={{ color: 'var(--color-functional-error)' }}>
            {error}
          </span>
        )}
      </div>
    );
  },
);
