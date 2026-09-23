import { useState, useEffect } from 'react';
import styles from './TypewriterText.module.css'

interface TypewriterTextProps{
    text: string;
    speed?: number;
    className?: string;
    onComplete?: () => void
}
function TypewriterText({ text, speed = 50, onComplete, className }: TypewriterTextProps) {
  const [displayed, setDisplayed] = useState('');
  const [complete, setComplete] = useState(false);


  useEffect(() => {
    setDisplayed('');
    let i = 0;
    const timer = setInterval(() => {
      setDisplayed(text.slice(0, i + 1));
      i++;
      if (i >= text.length) {
        clearInterval(timer);
        onComplete?.();
        setComplete(true);
      }
    }, speed);
    return () => clearInterval(timer);
  }, [text, speed]);

  // Полный текст невидимо занимает итоговое место, набираемый текст рисуется поверх.
  // Так размер блока не меняется во время печати и вёрстка вокруг не прыгает.
  return (
    <span className={`${styles.wrapper} ${className ?? ''}`}>
      <span className={styles.placeholder}>{text}</span>
      <span className={styles.typed} aria-hidden="true">
        {displayed}
        {!complete && <span className={`${styles.cursor} ${className ?? ''}`}>|</span>}
      </span>
    </span>
  );
}

export default TypewriterText;
