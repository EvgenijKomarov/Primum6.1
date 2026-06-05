import { BadgeTypeEnum } from '@/shared/enums/badge';
import styles from './styles.module.css';

interface BadgeProps {
    text: string;
    badgeType: BadgeTypeEnum;
    onClick?: React.MouseEventHandler<HTMLDivElement>; 
    className?: string;
    hideDot?: boolean;
    pointingCursor?: boolean;
}

const variantStyles: Record<BadgeTypeEnum, string> = {
    [BadgeTypeEnum.Positive]: styles.Positive,
    [BadgeTypeEnum.Warning]: styles.Warning,
    [BadgeTypeEnum.Negative]: styles.Negative,
};

export const Badge = ({ text, badgeType, onClick, className, hideDot, pointingCursor }: BadgeProps) => {
    return (
        <div className={`${styles.badge} ${variantStyles[badgeType]} ${pointingCursor ? styles.pointingCursor : ''}`} onClick={onClick}>
            {!hideDot && <span className={styles.dot} />}
            <span className={className}>{text}</span>
        </div>
    );
};