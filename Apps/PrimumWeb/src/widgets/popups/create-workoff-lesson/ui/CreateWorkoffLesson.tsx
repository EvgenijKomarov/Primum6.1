import type { AbonementDto } from "@/entity/abonement/model/types";
import { ButtonSizeEnum } from "@/shared/enums";
import Button from "@/shared/ui/Button/Button";
import { Popup } from "@/shared/ui/Popup";
import { useState } from "react";
import styles from './CreateWorkoffLesson.module.css';
import { useTeacherFreeTime } from "@/entity/teacher/model/useTeacherFreeTime";
import { formatDateTime } from "@/shared/format/format-config";
import { createWorkoffLesson } from "@/entity/lesson";


interface Props {
  abonement: AbonementDto;
  mutateAbonements: () => void;
}
export const CreateWorkoffLesson = ({ abonement, mutateAbonements }: Props) => {
    const [createPopupOpen, setCreatePopupOpen] = useState(false);
    const { teacherFreeTime } = useTeacherFreeTime(abonement.teacherId);

    return (
        <>
            <Button
                size={ButtonSizeEnum.SMALL}
                disabled={abonement.cancelledLessons === 0}
                onClick={() => setCreatePopupOpen(true)}
                >
                Отработать занятия
            </Button>
            {createPopupOpen && 
                <Popup
                    title='Создать занятие-отработку'
                    onClose={() => setCreatePopupOpen(false)}
                >
                    <div className={styles.buttons}>
                        {teacherFreeTime.map((time) => (
                            <Button
                                onClick={async () => {
                                    await createWorkoffLesson(time, abonement.id);
                                    await mutateAbonements();
                                    setCreatePopupOpen(false);
                                }}>
                                {formatDateTime(time)}
                            </Button>
                        ))}
                    </div>
                </Popup>}
        </>
    );
}