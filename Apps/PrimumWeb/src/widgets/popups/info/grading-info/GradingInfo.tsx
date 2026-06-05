import { BadgeTypeEnum } from "@/shared/enums/badge";
import { Badge } from "@/shared/ui/Badge/Badge";
import { Popup } from "@/shared/ui/Popup/Popup";
import { useState } from "react";
import styles from '../styles.module.css';

interface GradingInfoProps{
    homeworkGrade: number | null;
    lessonActivityGrade: number | null;
    repetitionOfMaterialGrade: number | null;
    studyInitiativeGrade: number | null;
    finalGrade: number | null;
}
export const Gradinginfo = (input: GradingInfoProps) => {
    const [popupOpen, setPopupOpen] = useState(false);

    if(input.finalGrade === null) {
        return null;
    }

    return (
        <div>
            <Badge text={input.finalGrade.toString()} badgeType={
                input.finalGrade >= 4 ? BadgeTypeEnum.Positive : 
                input.finalGrade >= 2.5 ? BadgeTypeEnum.Warning : 
                BadgeTypeEnum.Negative} onClick={() => setPopupOpen(true)} hideDot={true} pointingCursor={true} />
        {popupOpen && (
                <Popup 
                    title="Оценка занятия"
                    onClose={() => setPopupOpen(false)}>
                    <div className={styles.info}>
                        <div className={styles.rows}>
                            <div className={styles.row}>
                                <span className={styles.label}>Домашнее задание: </span>
                                <span className={styles.value}>{input.homeworkGrade}</span>
                            </div>
                            <div className={styles.row}>
                                <span className={styles.label}>Активность на уроке: </span>
                                <span className={styles.value}>{input.lessonActivityGrade}</span>
                            </div>
                            <div className={styles.row}>
                                <span className={styles.label}>Повторение материала: </span>
                                <span className={styles.value}>{input.repetitionOfMaterialGrade}</span>
                            </div>
                            <div className={styles.row}>
                                <span className={styles.label}>Инициатива в обучении: </span>
                                <span className={styles.value}>{input.studyInitiativeGrade}</span>
                            </div>
                            <div className={styles.row}>
                                <span className={styles.label}>Общая оценка: </span>
                                <span className={styles.value}>{input.finalGrade}</span>
                            </div>
                        </div>
                    </div>
                </Popup>
        )}
        </div>
    )
}