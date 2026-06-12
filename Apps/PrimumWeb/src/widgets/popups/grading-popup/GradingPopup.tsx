import { BadgeTypeEnum } from "@/shared/enums/badge";
import { Badge } from "@/shared/ui/Badge/Badge";
import { Popup } from "@/shared/ui/Popup";
import { useState } from "react";
import styles from './GradingPopup.module.css';
import { useForm } from "react-hook-form";
import { gradeLesson, type GradingInputDto } from "@/entity/lesson";
import { GRADES_TRANSLATION } from "@/features/translation/translation";
import Button from "@/shared/ui/Button/Button";
import { ButtonSizeEnum, ButtonTypeEnum } from "@/shared/enums";
import { useToast } from '@/shared/ui/Toast/useToast';

interface GradingPopupProps{
    lessonId: number,
    onSubmit: () => void
}
export const GradingPopup = ({ lessonId }: GradingPopupProps) => {
    const [popupOpen, setPopupOpen] = useState(false);
    const { showToast } = useToast();

    const gradingHints = GRADES_TRANSLATION.map((grade)=>{
        return { hint: grade.hint, value: grade.value };
    })

    const {
        register,
        handleSubmit,
      } = useForm<GradingInputDto>({
        defaultValues: {
          homeworkGrade: 0,
          lessonActivityGrade: 0,
          repetitionOfMaterialGrade: 0,
          studyInitiativeGrade: 0,
        },
      });
    
    const onSubmit = handleSubmit(async (values) => {
        const dto: GradingInputDto = {
            homeworkGrade: Number(values.homeworkGrade) || 0,
            lessonActivityGrade: Number(values.lessonActivityGrade) || 0,
            repetitionOfMaterialGrade: Number(values.repetitionOfMaterialGrade) || 0,
            studyInitiativeGrade: Number(values.studyInitiativeGrade) || 0,
        };
        await gradeLesson(lessonId, dto);
        showToast("Успешно оценено", 'success')
        setPopupOpen(false);
        onSubmit();
      });

    return(
        <div>
            <Badge text="Не оценено"
                badgeType={BadgeTypeEnum.Negative} 
                onClick={() => setPopupOpen(true)} 
                hideDot={true} 
                pointingCursor={true} />
            {popupOpen && (
                <Popup
                    title="Оценить занятие"
                    onClose={() => setPopupOpen(false)}>
                    <div className={styles.content}>
                        <span className={styles.hint}>
                            Оцените занятие, чтобы получить опыт за занятие. 
                            Не оцененные категории не влияют на общую оценку. 
                            Должна быть оценена хотя бы одна категория.</span>
                        <form className={styles.form} onSubmit={onSubmit}>
                            <div className={styles.field}>
                                <label className={styles.label}>Оценка домашнего задания: </label>
                                <select
                                    className={styles.select}
                                    {...register('homeworkGrade')}
                                >
                                    {gradingHints.map((t, index) => (
                                    <option key={`${t.value}-${index}`} value={t.value}>
                                        {`(${t.value}) ${t.hint}`}
                                    </option>
                                    ))}
                                </select>
                            </div>
                            <div className={styles.field}>
                                <label className={styles.label}>Оценка активности на уроке: </label>
                                <select
                                    className={styles.select}
                                    {...register('lessonActivityGrade')}
                                >
                                    {gradingHints.map((t, index) => (
                                    <option key={`${t.value}-${index}`} value={t.value}>
                                        {`(${t.value}) ${t.hint}`}
                                    </option>
                                    ))}
                                </select>
                            </div>
                            <div className={styles.field}>
                                <label className={styles.label}>Оценка оставшихся знаний с прошлого урока: </label>
                                <select
                                    className={styles.select}
                                    {...register('repetitionOfMaterialGrade')}
                                >
                                    {gradingHints.map((t, index) => (
                                    <option key={`${t.value}-${index}`} value={t.value}>
                                        {`(${t.value}) ${t.hint}`}
                                    </option>
                                    ))}
                                </select>
                            </div>
                            <div className={styles.field}>
                                <label className={styles.label}>Оценка инциативы в учебе: </label>
                                <select
                                    className={styles.select}
                                    {...register('studyInitiativeGrade')}
                                >
                                    {gradingHints.map((t, index) => (
                                    <option key={`${t.value}-${index}`} value={t.value}>
                                        {`(${t.value}) ${t.hint}`}
                                    </option>
                                    ))}
                                </select>
                            </div>

                            <Button
                                type="submit"
                                variant={ButtonTypeEnum.PRIMARY}
                                size={ButtonSizeEnum.NORMAL}
                            >
                                Оценить
                            </Button>
                        </form>
                    </div>
                </Popup>
            )}
            
        </div>
    );
}
