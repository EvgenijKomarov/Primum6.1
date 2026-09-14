import type { ReactNode } from 'react'
import styles from './Button.module.css'

interface ButtonProps {
  href: string
  children: ReactNode
  variant?: 'primary' | 'outline'
}

export default function Button({ href, children, variant = 'primary' }: ButtonProps) {
  const variantClass = variant === 'primary' ? styles.primary : styles.outline

  return (
    <a href={href} className={`${styles.btn} ${variantClass}`}>
      {children}
    </a>
  )
}
