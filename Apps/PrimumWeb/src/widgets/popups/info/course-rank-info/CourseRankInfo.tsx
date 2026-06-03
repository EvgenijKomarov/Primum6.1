import { Popup } from "@/shared/ui/Popup";
import { useEffect, useState } from "react";
import styles from '../styles.module.css';
import { getCourseRanks } from "@/entity/courseRank/api/courseRank.api";
import type { CourseRankDto } from "@/entity/courseRank/model/types";

interface CourseRankInfoProps {
  rankInput: string | null;
}

export const CourseRankInfo = ({ rankInput }: CourseRankInfoProps) => {
    const [ranks, setRanks] = useState<CourseRankDto[]>([]);
    const [popupOpen, setPopupOpen] = useState(false);

    useEffect(() => {
        const fetchRanks = async () => {
            const response = await getCourseRanks();
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
                    title="Таблица рангов курсов"
                    onClose={() => setPopupOpen(false)}>
                    <table className={styles.table}>
                        <thead>
                            <tr>
                                <th>Ранг</th>
                                <th>Уровень</th>
                                <th>Требуемый опыт</th>
                            </tr>
                        </thead>
                        <tbody>
                        {ranks.map((rank, index) => (
                            rank.rank === rankInput ? (
                                <tr key={`${rank.level}-${index}`} className={styles.highlightedRow}>
                                    <td className={styles.td}>{rank.rank}</td>
                                    <td className={styles.td}>{rank.level}</td>
                                    <td className={styles.td}>{rank.requiredExperience}</td>
                                </tr>
                            ) : (
                                <tr key={`${rank.level}-${index}`}>
                                    <td className={styles.td}>{rank.rank}</td>
                                    <td className={styles.td}>{rank.level}</td>
                                    <td className={styles.td}>{rank.requiredExperience}</td>
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