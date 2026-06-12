import { AbonementStatus, type AbonementDto } from '@/entity/abonement/model/types';
import styles from '../styles.module.css';
import { useEffect, useState } from 'react';
import { getTeacherAbonementById } from '@/entity/abonement/api/abonement.api';
import { Popup } from '@/shared/ui/Popup';
import { translateAbonementStatus } from '@/features/translation/translation';

interface AbonementInfoProps {
  abonementId: number;
  badgeStyle?: string;
}

export const AbonementInfo = ({ abonementId, badgeStyle }: AbonementInfoProps) => {
    const [abonement, setAbonement] = useState<AbonementDto | null>(null);
    const [popupOpen, setPopupOpen] = useState(false);

    useEffect(() => {
                const fetchTeacher = async () => {
                    const response = await getTeacherAbonementById(abonementId);
                    setAbonement(response.data);
                };
                fetchTeacher();
            }, []);

    const cfg = translateAbonementStatus(abonement?.abonementStatus ?? AbonementStatus.Deleted) ?? 
        translateAbonementStatus(AbonementStatus.Deleted);

    
    return (
        <div>
            <div 
                className={`${badgeStyle ?? styles.abonementBadge} ${cfg.cls}`} 
                onClick={() => setPopupOpen(true)}>
                {abonement?.studentDisplayName ?? ""}
            </div>
        {popupOpen && (
                <Popup
                    title="Информация об абонементе"
                    onClose={() => setPopupOpen(false)}>
                    <div className={styles.info}>
                        <div className={styles.rows}>
                            <div className={styles.row}>
                                <span className={styles.label}>ФИО ученика: </span>
                                <span className={styles.value}>{abonement?.studentDisplayName}</span>
                            </div>
                            <div className={styles.row}>
                                <span className={styles.label}>Курс: </span>
                                <span className={styles.value}>{abonement?.courseName}</span>
                            </div>
                            <div className={styles.row}>
                                <span className={styles.label}>Тема курса: </span>
                                <span className={styles.value}>{abonement?.courseThemeName}</span>
                            </div>
                            <div className={styles.row}>
                                <span className={styles.label}>Цена за урок: </span>
                                <span className={styles.value}>{abonement?.pricePerLesson}</span>
                            </div>
                            <div className={styles.row}>
                                <span className={styles.label}>Рейтинг: </span>
                                <span className={styles.value}>{abonement?.rating ?? '--'}</span>
                            </div>
                            <div className={styles.row}>
                                <span className={styles.label}>Статус: </span>
                                <span className={styles.value}>{cfg.label}</span>
                            </div>
                        </div>
                    </div>
                </Popup>
            )}
        </div>
    );
};