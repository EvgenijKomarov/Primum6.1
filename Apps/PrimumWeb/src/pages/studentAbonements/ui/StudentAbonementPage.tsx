import { useAbonementSchedules, useStudentAbonements } from '@/entity/abonement/model/useStudentAbonements';
import styles from './StudentAbonementPage.module.css';
import { EmptyIcon } from '@/shared/icons/types';
import { AbonementInputStatus, AbonementStatus, type AbonementDto } from '@/entity/abonement/model/types';
import { Card } from '@/shared/ui/Card/Card';
import { TeacherInfo } from '@/widgets/popups/info/teacher-info/TeacherInfo';
import { translateAbonementStatus, translateDayOfWeek } from '@/features/translation/translation';
import { deleteStudentSchedule, type DayOfWeek } from '@/entity/schedule';
import { EnsurancePopup } from '@/widgets/popups/ensurance-popup/ui/EnsurancePopup';
import { useEffect, useState } from 'react';
import { DeleteStudentAbonementAsync, StudentAbonementChangeStatusAsync } from '@/entity/abonement/api/abonement.api';
import { Badge } from '@/shared/ui/Badge/Badge';
import { getPublicCourse, type CourseDtoLite } from '@/entity/course';
import { CourseScheduleSubscribe } from '@/widgets/popups/select-shedule/ui/CourseScheduleSubscribe';
import { BadgeTypeEnum } from '@/shared/enums/badge';

interface SheduleBadgeProps {
    dow: DayOfWeek;
    time: number;
    id: number;
    onMutate: () => void
}

const SheduleBadge = ({dow, time, id, onMutate}: SheduleBadgeProps) => {
    const [ensurancePopupOpen, setEnsurancePopupOpen] = useState(false);

    const dowString = translateDayOfWeek(dow);
    const timeString = `${time}:00-${time+1}:00`;

    const handleDeleteShedule = async () => {
        await deleteStudentSchedule(id);
        await onMutate();
    }

    return (
        <div className={styles.scheduleBadge}>
            <div className={styles.scheduleBadgeTop}>
                <span className={styles.dowString}>{dowString}</span>
                <span 
                    className={styles.miniButton}
                    onClick={() => {setEnsurancePopupOpen(true)}}>X</span>
            </div>
            <div>
                <span className={styles.timeString}>{timeString}</span>
            </div>
        {ensurancePopupOpen && <EnsurancePopup
                      setPopupOpen={setEnsurancePopupOpen}
                      onConfirm={handleDeleteShedule}
                      description={`Вы уверены, что хотите удалить расписание на ${dowString} ${timeString}?
                      Занятия перестанут создаваться на это время, однако уже существующие не удалятся`}
                    />}
        </div>
    );
}

interface AbonementCardProps {
  abonement: AbonementDto;
  mutateAbonements: () => void;
}
const AbonementCard = ({ abonement, mutateAbonements }: AbonementCardProps) => {
    const { schedules, mutate } = useAbonementSchedules(abonement.id);
    const [ensurancePopupOpen, setEnsurancePopupOpen] = useState(false);
    const [changeStatusPopupOpen, setChangeStatusPopupOpen] = useState(false);
    const [subscribePopupOpen, setSubscribePopupOpen] = useState(false);

    const [course, setCourse] = useState<CourseDtoLite | null>(null);

    useEffect(() => {
        if (!abonement.courseId) return;
        
        getPublicCourse(abonement.courseId).then(res => setCourse(res.data));
    }, [abonement.courseId]);

    const {label: statusLabel, badgeType} = translateAbonementStatus(abonement.abonementStatus);

    const hadleDeleteAbonement = async () => {
        await DeleteStudentAbonementAsync(abonement.id);
        await mutateAbonements();
        await mutate();
    }

    const hadleActivateAbonement = async () => {
        await StudentAbonementChangeStatusAsync(abonement.id, AbonementInputStatus.Activate);
        await mutateAbonements();
    }
    const hadleFreezeAbonement = async () => {
        await StudentAbonementChangeStatusAsync(abonement.id, AbonementInputStatus.Freeze);
        await mutateAbonements();
    }

    const ACTIVATION_CONFIG: Record<AbonementStatus, {popupDescription: string, handler: () => void}> = {
        [AbonementStatus.Active]: { popupDescription: 'Вы уверены, что хотите заморозить абонемент? Занятия будут создаваться, но ссылка приходить перестанет и не будут списываться средства за урок', handler: hadleFreezeAbonement },
        [AbonementStatus.Freezed]: { popupDescription: 'Вы уверены, что хотите активировать абонемент?', handler: hadleActivateAbonement },
        [AbonementStatus.Deleted]: { popupDescription: 'Вы уверены, что хотите активировать абонемент?', handler: hadleActivateAbonement },
    }
    const status_config = ACTIVATION_CONFIG[abonement.abonementStatus];

    return (
        <Card hoverable={true} min_width={'30rem'}>
            <div className={styles.abonementCard}>
                <div className={styles.cardHeader}>
                    <div className={styles.cardTop}>
                        <span className={styles.subtitle}>
                            {abonement.courseThemeName}
                        </span>
                        <span 
                            className={styles.middleButton}
                            onClick={() => {setEnsurancePopupOpen(true)}}>X</span>
                        {ensurancePopupOpen && <EnsurancePopup
                            setPopupOpen={setEnsurancePopupOpen}
                            onConfirm={hadleDeleteAbonement}
                            description={`Вы уверены, что хотите удалить абонемент по курсу ${abonement.courseName}?`}
                            />}
                    </div>
                    <span className={styles.title}>
                        {abonement.courseName}
                    </span>
                    <div className={styles.cardBadges}>
                        <Badge text={statusLabel} badgeType={badgeType} className={styles.cursorBadge} onClick={
                            () => {setChangeStatusPopupOpen(true) 
                        }} />
                        {abonement.isReferal ? <Badge text='Реферальный' badgeType={BadgeTypeEnum.Positive}/> : <></>}
                        {changeStatusPopupOpen && <EnsurancePopup
                            setPopupOpen={setChangeStatusPopupOpen}
                            onConfirm={status_config.handler}
                            description={status_config.popupDescription}
                            />}
                    </div>
                </div>
                <div className={styles.cardContent}>
                    <div className={styles.cardContentColumn}>
                        <div className={styles.stat}>
                            <span className={styles.statLabel}>Преподаватель: </span>
                            <span className={styles.statValue}>
                                <TeacherInfo teacherId={abonement.teacherId}/>
                            </span>
                        </div>
                        <div className={styles.stat}>
                            <span className={styles.statLabel}>Рейтинг: </span>
                            <span className={styles.statValue}>{abonement.rating ?? '--'}</span>
                        </div>
                        <div className={styles.stat}>
                            <span className={styles.statLabel}>Цена за урок: </span>
                            <span className={styles.statValue}>{abonement.pricePerLesson}</span>
                        </div>
                    </div>
                    <div className={styles.cardContentColumn}>
                        {schedules.map((schedule, index) => (
                            <SheduleBadge 
                                key={`${schedule.id}-${index}`}
                                dow={schedule.dayOfWeek}
                                time={schedule.time}
                                id={schedule.id}
                                onMutate={mutate} />
                        ))}
                        {course?.maxLessons && course?.maxLessons > schedules.length && (
                            <div 
                                className={styles.scheduleAddBadge}
                                onClick={()=>{setSubscribePopupOpen(true)}}>
                                <span className={styles.scheduleAddBadgePlus}>+</span>
                            </div>
                        )}
                    </div>
                    {subscribePopupOpen && course && (
                        <CourseScheduleSubscribe 
                            setSubscribePopupOpen={setSubscribePopupOpen}
                            course={course}
                            onSubscribe={mutate}/>
                    )}
                </div>
            </div>
        </Card>
    );
}

export const StudentAbonementPage = () => {
    const { abonements, isLoading, mutate } = useStudentAbonements();

    return (
        <div className={styles.page}>
            <h1 className={styles.title}>Мои абонементы</h1>
            <p className={styles.subtitle}>Управляйте своим обучением</p>
            <p className={styles.line}></p>
            <div className={styles.grid}>
                {isLoading ? (
                    <div className={styles.empty}>
                        <EmptyIcon />
                        <p className={styles.emptyText}>
                            Загрузка...
                        </p>
                    </div>
                ) : abonements && abonements.length > 0 ? (
                    abonements.map((abonement, index) => (
                        <AbonementCard
                            key={`${abonement.id}-${index}`}
                            abonement={abonement}
                            mutateAbonements={mutate}
                        />
                    ))
                ) : (
                    <div className={styles.empty}>
                        <EmptyIcon />
                        <p className={styles.emptyText}>
                            Абонементов пока нет
                        </p>
                    </div>
                )}
            </div>
        </div>
    );
};