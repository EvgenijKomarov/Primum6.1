import { useEffect, useState } from "react";
import { Popup } from "../../popup/ui/Popup";
import styles from './StudentRanks.module.css';
import type { StudentRankDto } from "@/entity/studentRank/model/types";
import { getStudentRanks } from "@/entity/studentRank/api/studentRank.api";

interface StudentRanksProps {
  setRankPopupOpen: (open: boolean) => void;
}

export const StudentRanks = ({ setRankPopupOpen }: StudentRanksProps) => {
    const [ranks, setRanks] = useState<StudentRankDto[]>([]);

    useEffect(() => {
        const fetchRanks = async () => {
            const response = await getStudentRanks();
            setRanks(response.data.items || []);
        };
        fetchRanks();
    }, []);

    return (
        <Popup
            title="Таблица рангов учеников"
            onClose={() => setRankPopupOpen(false)}
        >
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
                    <tr key={`${rank.level}-${index}`}>
                        <td className={styles.td}>{rank.rank}</td>
                        <td className={styles.td}>{rank.level}</td>
                        <td className={styles.td}>{rank.requiredExperience}</td>
                        <td className={styles.td}>{rank.coinDiscount * 100}%</td>
                    </tr>
                ))}
                </tbody>
            </table>
        </Popup>
    );
};