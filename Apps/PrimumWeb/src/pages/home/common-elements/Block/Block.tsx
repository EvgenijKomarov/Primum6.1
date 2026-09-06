import { Card } from '@/shared/ui/Card/Card';
import styles from './Block.module.css'
import { useInView } from './useInView';

interface BlockProps {
    title?: string;
    plainText?: string;
    imageUrl?: string;
    children?: React.ReactNode;
    isReversed?: boolean
}
export const Block = ({title, imageUrl, children, plainText, isReversed}: BlockProps) => {
    const { ref, isVisible } = useInView({ threshold: 0.15, triggerOnce: true });

  return (
    <div 
      ref={ref} 
      className={`${styles.blockWrapper} ${isVisible ? styles.visible : ''}`}
    >
        <Card hoverable={true} width='100%'>
            <div className={styles.cardContent}>
                {isReversed ? (
                    <>
                        <img 
                            src={imageUrl}
                            width="50%"
                        />
                        <div className={styles.cardChild} style={{width: imageUrl ? '50%' : '100%'}}>
                            {title && <span className={styles.cardTitle}>{title}</span>}
                            {plainText && <span className={styles.cardText}>{plainText}</span>}
                            {children}
                        </div>
                    </>
                ) : (
                    <>
                        <div className={styles.cardChild} style={{width: imageUrl ? '50%' : '100%'}}>
                            {title && <span className={styles.cardTitle}>{title}</span>}
                            {plainText && <span className={styles.cardText}>{plainText}</span>}
                            {children}
                        </div>
                        {imageUrl && <img 
                            src={imageUrl}
                            width="50%"
                        />}
                    </>
                )}
            </div>
        </Card>
    </div>
  );
}