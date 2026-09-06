import { BadgeTypeEnum } from "@/shared/enums/badge";
import { Badge } from "@/shared/ui/Badge/Badge";
import { Popup } from "@/shared/ui/Popup/Popup";
import { useState } from "react";
import styles from '../styles.module.css';
import { translateGrade } from "@/features/translation/translation";

interface GradingRowProps{
    title: string;
    grade: number
}
const GradingRow = ({title, grade}: GradingRowProps) => {
    const {label, value} = translateGrade(grade);
    return (
        <div className={styles.row}>
            <span className={styles.label}>{title}</span>
            {value === 0 ? (
                <span className={styles.value}>{label}</span>
            ) : (
                <span className={styles.value}>{`(${value}) ${label}`}</span>
            )}
        </div>
    );
}

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
                BadgeTypeEnum.Negative} onClick={() => setPopupOpen(true)} hideDot={true} />
        {popupOpen && (
                <Popup 
                    title="Оценка занятия"
                    onClose={() => setPopupOpen(false)}>
                    <div className={styles.info}>
                        <div className={styles.rows}>
                            <GradingRow title={'Домашнее задание: '} grade={input.homeworkGrade ?? 0}/>
                            <GradingRow title={'Активность на уроке: '} grade={input.lessonActivityGrade ?? 0}/>
                            <GradingRow title={'Повторение материала: '} grade={input.repetitionOfMaterialGrade ?? 0}/>
                            <GradingRow title={'Инициатива в обучении: '} grade={input.studyInitiativeGrade ?? 0}/>
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