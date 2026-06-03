import { getTeacherProfileById, type TeacherProfileDto } from "@/entity/teacher";
import { Popup } from "@/shared/ui/Popup";
import { useEffect, useState } from "react";
import styles from '../styles.module.css';

interface TeacherInfoProps {
  teacherId: number;
}

export const TeacherInfo = ({ teacherId }: TeacherInfoProps) => {
    const [teacher, setTeacher] = useState<TeacherProfileDto | null>(null);
    const [popupOpen, setPopupOpen] = useState(false);

    useEffect(() => {
            const fetchTeacher = async () => {
                const response = await getTeacherProfileById(teacherId);
                setTeacher(response.data || null);
            };
            fetchTeacher();
        }, []);

    return (
        <div>
            <div className={styles.displayName} onClick={() => setPopupOpen(true)}>
                {teacher?.displayName}
            </div>
            {popupOpen && (
                <Popup
                    title="Информация о преподавателе"
                    onClose={() => setPopupOpen(false)}>
                    <div className={styles.info}>
                        <div className={styles.rows}>

                            <div className={styles.row}>
                                <span className={styles.label}>ФИО: </span>
                                <span className={styles.value}>{teacher?.displayName}</span>
                            </div>
                            <div className={styles.row}>
                                <span className={styles.label}>Уровень: </span>
                                <span className={styles.value}>{teacher?.level}</span>
                            </div>
                            <div className={styles.row}>
                                <span className={styles.label}>Ранг: </span>
                                <span className={styles.value}>{teacher?.rank}</span>
                            </div>
                        </div>
                        <span className={styles.description}>
                            {teacher?.about}
                        </span>
                    </div>
                </Popup>
            )}
        </div>
    );
};