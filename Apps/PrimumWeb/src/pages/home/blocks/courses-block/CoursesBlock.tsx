import { useState } from 'react';
import { Block } from '../../common-elements/Block/Block';
import styles from './CoursesBlock.module.css'
import { usePublicThemes } from '@/entity/course-theme/model/usePublicThemes';
import { usePublicCourses, type CourseDtoLite } from '@/entity/course';
import { TeacherInfo } from '@/widgets/popups/info/teacher-info/TeacherInfo';
import { CourseRankInfo } from '@/widgets/popups/rank-info/course-rank-info/CourseRankInfo';
import { EmptyIcon } from '@/shared/icons/types';

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

export const CoursesBlock = () => {
    const [selectedThemeId, setSelectedThemeId] = useState<number | null>(null);
    const { data: themesResult, isLoading: themesLoading } = usePublicThemes();
    const themes = themesResult?.items?.filter((t) => t.isActive) ?? [];
    const { courses, isLoading: coursesLoading } = usePublicCourses(selectedThemeId, 0, 3);
    const isLoading = themesLoading || coursesLoading;

  return (
    <Block title='Наши курсы'>
        <div className={styles.coursesContent}>
            <div className={styles.filterBar}>
                <button
                className={`${styles.chip} ${selectedThemeId === null ? styles.chipActive : ''}`}
                onClick={() => setSelectedThemeId(null)}
                >
                Все
                </button>
                {themes.map((theme) => (
                <button
                    key={theme.id}
                    className={`${styles.chip} ${selectedThemeId === theme.id ? styles.chipActive : ''}`}
                    onClick={() => setSelectedThemeId(theme.id)}
                >
                    {theme.themeName}
                </button>
                ))}
            </div>
            {isLoading || courses.length === 0 ? (
                <div className={styles.empty}>
                    <EmptyIcon />
                    <p className={styles.emptyText}>
                        {selectedThemeId ? 'Курсов по этой теме пока нет' : 'Курсов пока нет'}
                    </p>
                </div>
            ) : (
                <div className={styles.columns}>
                    {courses.map((course) => (
                        <CourseCard
                        key={course.id}
                        course={course}
                        />
                    ))}
                </div>
            )}
        </div>
    </Block>
  );
}
