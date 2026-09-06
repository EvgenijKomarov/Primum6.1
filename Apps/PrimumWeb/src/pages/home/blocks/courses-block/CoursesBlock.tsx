import { useState } from 'react';
import { Block } from '../../common-elements/Block/Block';
import styles from './CoursesBlock.module.css'
import { usePublicThemes } from '@/entity/course-theme/model/usePublicThemes';
import { usePublicCourses, type CourseDtoLite } from '@/entity/course';
import { TeacherInfo } from '@/widgets/popups/info/teacher-info/TeacherInfo';
import { CourseRankInfo } from '@/widgets/popups/rank-info/course-rank-info/CourseRankInfo';
import { EmptyIcon } from '@/shared/icons/types';
import { Tabs, type TabItem } from '@/shared/ui/TabBar/TabBar';

interface CourseCardProps {
  course: CourseDtoLite;
}
const CourseCard = ({ course }: CourseCardProps) => {
    const isFree = course.price === 0;

    return (
        <div className={styles.card}>
            <div>
                <h3 className={styles.cardTitle}>{course.name ?? '—'}</h3>
                {course.about && <p className={styles.cardAbout}>{course.about}</p>}
            </div>

            <div className={styles.cardMeta}>
                <div className={styles.cardMetaItem}>
                    <span className={styles.cardMetaLabel}>Уровень</span>
                    <span className={styles.cardMetaValue}>{course.level}</span>
                </div>
                {course.freeLessons > 0 && (
                <div className={styles.cardMetaItem}>
                    <span className={styles.cardMetaLabel}>Бесплатных уроков</span>
                    <span className={styles.cardMetaValue}>{course.freeLessons}</span>
                </div>
                )}
                {course.rank && (
                <div className={styles.cardMetaItem}>
                    <span className={styles.cardMetaLabel}>Ранг</span>
                    <CourseRankInfo rankInput={course.rank} />
                </div>
                )}
            </div>

            <div className={styles.cardFooter}>
                <span className={styles.cardTeacher}>
                    <TeacherInfo teacherId={course.teacherId} />
                </span>
                <div className={styles.cardFooterRight}>
                    <span className={`${styles.cardPrice} ${isFree ? styles.cardPriceFree : ''}`}>
                        {isFree ? 'Бесплатно' : `${course.price.toFixed(0)} ₽`}
                    </span>
                </div>
            </div>
        </div>
    );
}

const ALL_TAB_VALUE = 'all';

export const CoursesBlock = () => {
    const [selectedThemeId, setSelectedThemeId] = useState<number | null>(null);
    const { data: themesResult, isLoading: themesLoading } = usePublicThemes();
    const themes = themesResult?.items?.filter((t) => t.isActive) ?? [];
    const { courses, isLoading: coursesLoading } = usePublicCourses(selectedThemeId, 0, 3);
    const isLoading = themesLoading || coursesLoading;

    const coursesContent = (
        isLoading || courses.length === 0 ? (
            <div className={styles.empty}>
                <EmptyIcon />
                <p className={styles.emptyText}>
                    {selectedThemeId ? 'Курсов по этой теме пока нет' : 'Курсов пока нет'}
                </p>
            </div>
        ) : (
            <div className={styles.columns}>
                {courses.map((course) => (
                    <CourseCard key={course.id} course={course} />
                ))}
            </div>
        )
    );

    const activeTabValue = selectedThemeId === null ? ALL_TAB_VALUE : String(selectedThemeId);

    const tabs: TabItem<string>[] = [
        {
            value: ALL_TAB_VALUE,
            label: 'Все',
            content: activeTabValue === ALL_TAB_VALUE ? coursesContent : null,
        },
        ...themes.map((theme): TabItem<string> => ({
            value: String(theme.id),
            label: theme.themeName ?? "",
            content: activeTabValue === String(theme.id) ? coursesContent : null,
        })),
    ];

    const handleTabChange = (value: string) => {
        setSelectedThemeId(value === ALL_TAB_VALUE ? null : Number(value));
    };

  return (
    <Block title='Наши курсы'>
        <div className={styles.coursesContent}>
            <Tabs tabs={tabs} value={activeTabValue} onChange={handleTabChange} />
        </div>
    </Block>
  );
}
