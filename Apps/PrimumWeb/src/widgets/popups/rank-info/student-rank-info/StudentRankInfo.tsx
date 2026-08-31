import { useEffect, useState } from "react";
import styles from '../styles.module.css';
import type { StudentRankDto } from "@/entity/studentRank/model/types";
import { getStudentRanks } from "@/entity/studentRank/api/studentRank.api";
import { Popup } from "@/shared/ui/Popup";

interface StudentRankInfoProps {
  rankInput: string | null;
}

export const StudentRankInfo = ({ rankInput }: StudentRankInfoProps) => {
    const [ranks, setRanks] = useState<StudentRankDto[]>([]);
    const [popupOpen, setPopupOpen] = useState(false);

    useEffect(() => {
        const fetchRanks = async () => {
            const response = await getStudentRanks();
            setRanks(response.data.items || []);
        };
        fetchRanks();
    }, []);

    return (
        <div>
            <div className={styles.displayName} onClick={() => setPopupOpen(true)}>
                {rankInput}
            </div>
            {popupOpen && (<Popup
                title="Таблица рангов учеников"
                onClose={() => setPopupOpen(false)}>
                <table className={styles.table}>
                    <thead>
                        <tr>
                            <th>Ранг</th>
                            <th>Уровень</th>
                            <th>Требуемый опыт</th>
                            <th>Скидка монет</th>
                        </tr>
                    </thead>
                    <tbody>
                    {ranks.map((rank, index) => (
                        <tr key={`${rank.level}-${index}`} className={rank.rank === rankInput ? styles.highlightedRow : ''}>
                            <td className={styles.td}>{rank.rank}</td>
                            <td className={styles.td}>{rank.level}</td>
                            <td className={styles.td}>{rank.requiredExperience}</td>
                            <td className={styles.td}>{(rank.coinDiscount * 100).toFixed(0)}%</td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            </Popup>)}
        </div>
    );
};