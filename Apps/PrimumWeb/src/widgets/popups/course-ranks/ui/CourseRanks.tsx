import { Popup } from "@/shared/ui/Popup";
import { useEffect, useState } from "react";
import styles from './CourseRanks.module.css';
import { getCourseRanks } from "@/entity/courseRank/api/courseRank.api";
import type { CourseRankDto } from "@/entity/courseRank/model/types";

interface CourseRanksProps {
  setRankPopupOpen: (open: boolean) => void;
}

export const CourseRanks = ({ setRankPopupOpen }: CourseRanksProps) => {
    const [ranks, setRanks] = useState<CourseRankDto[]>([]);

    useEffect(() => {
        const fetchRanks = async () => {
            const response = await getCourseRanks();
            setRanks(response.data.items || []);
        };
        fetchRanks();
    }, []);

    return (
        <Popup
            title="Таблица рангов курсов"
            onClose={() => setRankPopupOpen(false)}>
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
                    <tr key={`${rank.level}-${index}`}>
                        <td className={styles.td}>{rank.rank}</td>
                        <td className={styles.td}>{rank.level}</td>
                        <td className={styles.td}>{rank.requiredExperience}</td>
                    </tr>
                ))}
                </tbody>
            </table>
        </Popup>
    );
};