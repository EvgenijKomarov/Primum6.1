import { getTeacherRanks } from "@/entity/teacherRank/api/teacherRank";
import type { TeacherRankDto } from "@/entity/teacherRank/model/types";
import { useEffect, useState } from "react";
import styles from './TeacherRankInfo.module.css';
import { Popup } from "@/shared/ui/Popup";

interface TeacherRankInfoProps {
  rankInput: string | null;
}

export const TeacherRankInfo = ({ rankInput }: TeacherRankInfoProps) => {
    const [ranks, setRanks] = useState<TeacherRankDto[]>([]);
    const [popupOpen, setPopupOpen] = useState(false);

    useEffect(() => {
        const fetchRanks = async () => {
            const response = await getTeacherRanks();
            setRanks(response.data.items || []);
        };
        fetchRanks();
    }, []);

    return (
        <div>
            <div className={styles.displayName} onClick={() => setPopupOpen(true)}>
                {rankInput}
            </div>
            {popupOpen && (
                <Popup
                title="Таблица рангов преподавателей"
                onClose={() => setPopupOpen(false)}>
                    <table className={styles.table}>
                        <thead>
                            <tr>
                                <th>Ранг</th>
                                <th>Уровень</th>
                                <th>Требуемый опыт</th>
                                <th>Комиссия преподавателя</th>
                            </tr>
                        </thead>
                        <tbody>
                        {ranks.map((rank, index) => (
                            rank.rank === rankInput ? (
                                <tr key={`${rank.level}-${index}`} className={styles.highlightedRow}>
                                    <td className={styles.td}>{rank.rank}</td>
                                    <td className={styles.td}>{rank.level}</td>
                                    <td className={styles.td}>{rank.requiredExperience}</td>
                                    <td className={styles.td}>{rank.earningMultiplier * 100}%</td>
                                </tr>
                            ) : (
                                <tr key={`${rank.level}-${index}`}>
                                    <td className={styles.td}>{rank.rank}</td>
                                    <td className={styles.td}>{rank.level}</td>
                                    <td className={styles.td}>{rank.requiredExperience}</td>
                                    <td className={styles.td}>{rank.earningMultiplier * 100}%</td>
                                </tr>
                            )
                        ))}
                        </tbody>
                    </table>
                </Popup>
            )}
        </div>
    );
};