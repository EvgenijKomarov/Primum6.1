import { useTeacherEarnings } from "@/entity/teacherEarningCalculation/model/useTeacherEarnings"
import styles from '../styles.module.css';
import { useState } from "react";
import { Popup } from "@/shared/ui/Popup";

export const TeacherEarningInfo = () => {
    const { teacherEarnings } = useTeacherEarnings();
    const [popupOpen, setPopupOpen] = useState(false);

    return (
    <>
        <div className={styles.badge}>
            <div className={styles.displayName} onClick={() => setPopupOpen(true)} style={{ fontSize: '0.8rem'}}>
                {teacherEarnings ? `${teacherEarnings.totalEarningMultiplier * 100}%` : '--'}
            </div>
        </div>
        {popupOpen && (
            <Popup
                title="Доход с урока"
                onClose={() => setPopupOpen(false)}
                width={'70%'}>
                <div className={styles.info}>
                    <div className={styles.rows}>
                        <div className={styles.item}>
                            <div className={styles.row}>
                                <span className={styles.lightLabel}>Базовый процент</span>
                                <span className={styles.value}>30%</span>
                            </div>
                            <p className={styles.hint}>Гарантированная часть Вашего дохода с урока</p>
                        </div>

                        <div className={styles.item}>
                            <div className={styles.row}>
                                <span className={styles.lightLabel}>Процент за уровень</span>
                                <span className={styles.value}>
                                    {teacherEarnings ? `${teacherEarnings.earningMultiplierByRank * 100}%` : '--'}
                                </span>
                            </div>
                            <p className={styles.hint}>Бонус к части дохода за Ваш уровень. Максимум 20%</p>
                        </div>

                        <div className={styles.item}>
                            <div className={styles.row}>
                                <span className={styles.lightLabel}>Процент за конверсию</span>
                                <span className={styles.value}>
                                    {teacherEarnings ? `${teacherEarnings.earningMultiplierByConvertion * 100}%` : '--'}
                                </span>
                            </div>
                            <p className={styles.hint}>
                                Максимум можно получить дополнительные 20% при достижении конверсии в 50% и выше.
                                Успешной конверсией считаются абонементы, у которых есть купленные занятия.
                                Для вычисления берутся последние 10 созданных абонементов, у которых кончились
                                бесплатные занятия, и количество конверсий из них делится на 10. Пока таких абонементов не набралось, бонус фиксируется на 10%
                            </p>
                        </div>

                        <div className={styles.item}>
                            <p className={styles.hint} style={{textDecorationLine: 'underline', fontSize: '0.8rem'}}>
                                Также к вашему доходу добавляется бонус +2.5% за каждый оплаченный урок в абонементе.
                                Максимум 10%
                            </p>
                        </div>

                        <div className={`${styles.item} ${styles.total}`}>
                            <div className={styles.row}>
                                <span className={styles.lightLabel}>Итоговый процент</span>
                                <span className={styles.value}>
                                    {teacherEarnings ? `${teacherEarnings.totalEarningMultiplier * 100}%` : '--'}
                                </span>
                            </div>
                            <p className={styles.hint}>ВНИМАНИЕ! Ваша доля может динамически меняться время от времени</p>
                        </div>

                    </div>
                </div>
            </Popup>)}
    </>);
}