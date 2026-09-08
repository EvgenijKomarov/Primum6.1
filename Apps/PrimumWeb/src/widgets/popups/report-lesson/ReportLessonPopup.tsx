import { LessonReportStatus, studentReportLesson, teacherReportLesson } from "@/entity/lesson"
import { Popup } from "@/shared/ui/Popup";
import styles from './ReportLessonPopup.module.css';
import { useState } from "react";
import { translateReportStatus } from "@/features/translation/translation";
import Button from "@/shared/ui/Button/Button";
import { ButtonSizeEnum, ButtonTypeEnum } from "@/shared/enums";
import { useToast } from "@/shared/ui/Toast/useToast";

const StudentLessonReportStatuses = [
    LessonReportStatus.TeacherBadBehavior,
    LessonReportStatus.TeacherHaventConnected,
    LessonReportStatus.TeacherInappropriateContent
];

const TeacherLessonReportStatuses = [
    LessonReportStatus.StudentBadBehavior,
    LessonReportStatus.StudentHaventConnected,
    LessonReportStatus.StudentInappropriateContent
]

interface Props{
    lessonId: number,
    isStudentReporting: boolean,
    onReport: () => void,
    onClose: () => void
}
export const ReportLessonPopup = ({ lessonId, isStudentReporting, onReport, onClose }: Props) => {
    const [reportStatus, setReportStatus] = useState<LessonReportStatus>(LessonReportStatus.Ok);
    const { showToast } = useToast();
    const statuses = isStudentReporting ? StudentLessonReportStatuses : TeacherLessonReportStatuses;

    return (
    <Popup
        title="Пожаловаться на занятие"
        onClose={() => onClose()}>
        <div className={styles.content}>
            <div className={styles.field}>
                <label className={styles.label}>Причина жалобы</label>
                <select
                    className={styles.select}
                    value={reportStatus}
                    onChange={(e) => setReportStatus(Number(e.target.value) as LessonReportStatus)}
                >
                    <option value="">— Выберите причину —</option>
                        {statuses.map((status) => (
                        <option key={status} value={status}>
                            {translateReportStatus(status)}
                        </option>
                    ))}
                </select>
            </div>
            <Button
                type="submit"
                disabled={reportStatus === LessonReportStatus.Ok}
                variant={ButtonTypeEnum.PRIMARY}
                size={ButtonSizeEnum.NORMAL}
                onClick={async () => 
                {
                    if (isStudentReporting) {
                        await studentReportLesson(lessonId, reportStatus);
                    }
                    else {
                        await teacherReportLesson(lessonId, reportStatus);
                    }

                    await onReport();
                    await onClose();
                    showToast(
                        'Жалоба передана администрации. С Вами свяжутся в скором времени для уточнения деталей',
                        'success',
                        5000
                    );
                }}>
                Пожаловаться
            </Button>
        </div>
    </Popup>);
}