import { BadgeTypeEnum } from '@/shared/enums/badge';
import styles from './styles.module.css';

interface BadgeProps {
    text: string;
    badgeType: BadgeTypeEnum;
}

const variantStyles: Record<BadgeTypeEnum, string> = {
    [BadgeTypeEnum.Positive]: styles.Positive,
    [BadgeTypeEnum.Warning]: styles.Warning,
    [BadgeTypeEnum.Negative]: styles.Negative,
};

export const Badge = ({ text, badgeType }: BadgeProps) => {
    return (
        <div className={`${styles.badge} ${variantStyles[badgeType]}`} >
            <span className={styles.dot} />
            {text}
        </div>
    );
};