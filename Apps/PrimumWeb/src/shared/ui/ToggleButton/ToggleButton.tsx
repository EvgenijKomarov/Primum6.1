import { useId } from "react";
import styles from "./ToggleButton.module.css";

export type ToggleButtonSize = "sm" | "md" | "lg";
 
export interface ToggleButtonProps {
  /** Current value */
  checked: boolean;
  /** Called with the new value when toggled */
  onChange: (next: boolean) => void;
  /** Disables interaction */
  disabled?: boolean;
  /** Optional visible/aria label */
  label?: string;
  /** Visual size, defaults to "md" */
  size?: ToggleButtonSize;
}
 
/**
 * ToggleButton — iOS-style toggle switch for a boolean value.
 *
 * Usage:
 *   const [enabled, setEnabled] = useState(false);
 *   <ToggleButton checked={enabled} onChange={setEnabled} />
 */
export default function ToggleButton({
  checked,
  onChange,
  disabled = false,
  label,
  size = "md",
}: ToggleButtonProps) {
  const id = useId();
 
  const toggle = (): void => {
    if (disabled) return;
    onChange(!checked);
  };
 
  const sizeClass: string = {
    sm: styles.sizeSm,
    md: styles.sizeMd,
    lg: styles.sizeLg,
  }[size];
 
  const labelClassName = [styles.label, disabled ? styles.labelDisabled : ""]
    .filter(Boolean)
    .join(" ");
 
  const toggleClassName = [
    styles.toggle,
    sizeClass,
    checked ? styles.toggleChecked : "",
  ]
    .filter(Boolean)
    .join(" ");

  console.log(styles);
 
  return (
    <label htmlFor={id} className={labelClassName}>
      <button
        id={id}
        type="button"
        role="switch"
        aria-checked={checked}
        aria-label={label}
        disabled={disabled}
        onClick={toggle}
        className={toggleClassName}
      >
        <span className={styles.knob} />
      </button>
      {label && <span className={styles.text}>{label}</span>}
    </label>
  );
}