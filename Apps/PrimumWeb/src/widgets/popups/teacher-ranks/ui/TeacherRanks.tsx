import { getTeacherRanks } from "@/entity/teacherRank/api/teacherRank";
import type { TeacherRankDto } from "@/entity/teacherRank/model/types";
import { useEffect, useState } from "react";
import { Popup } from "../../popup/ui/Popup";
import styles from './TeacherRanks.module.css';

interface TeacherRanksProps {
  setRankPopupOpen: (open: boolean) => void;
}

export const TeacherRanks = ({ setRankPopupOpen }: TeacherRanksProps) => {
    const [ranks, setRanks] = useState<TeacherRankDto[]>([]);

    useEffect(() => {
        const fetchRanks = async () => {
            const response = await getTeacherRanks();
            setRanks(response.data.items || []);
        };
        fetchRanks();
    }, []);

    return (
        <Popup
            title="Таблица рангов преподавателей"
            onClose={() => setRankPopupOpen(false)}
        >
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
                    <tr key={`${rank.level}-${index}`}>
                        <td className={styles.td}>{rank.rank}</td>
                        <td className={styles.td}>{rank.level}</td>
                        <td className={styles.td}>{rank.requiredExperience}</td>
                        <td className={styles.td}>{rank.earningMultiplier * 100}%</td>
                    </tr>
                ))}
                </tbody>
            </table>
        </Popup>
    );
};