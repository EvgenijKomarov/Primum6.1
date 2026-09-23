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

  return (
    <span className={className}>
      {displayed}
      {!complete && <span className={`${styles.cursor} ${className ?? ''}`}>|</span>}
    </span>
  );
}

export default TypewriterText;