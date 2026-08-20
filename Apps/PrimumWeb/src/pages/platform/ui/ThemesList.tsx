import type { AdminProfileDto } from "@/entity/admin/model/types";
import { useThemesByAdmin } from "@/entity/course-theme/model/usePublicThemes";
import styles from './PlatformConfigPage.module.css';
import { useState } from "react";
import Button from "@/shared/ui/Button/Button";
import { ButtonSizeEnum, ButtonTypeEnum } from "@/shared/enums";
import type { CourseThemeDto, CourseThemeInputDto } from "@/entity/course-theme";
import { Controller, useForm } from "react-hook-form";
import { Popup } from "@/shared/ui/Popup";
import { Input } from "@/shared/ui/Input";
import { createCourseTheme, editCourseTheme } from "@/entity/course-theme/api/course-theme.byAdmin";
import { EmptyIcon } from "@/shared/icons/types";
import { Badge } from "@/shared/ui/Badge/Badge";
import { BadgeTypeEnum } from "@/shared/enums/badge";

const ThemeInteractionPopup = ({onClose, onMutate, courseThemeDto}: {onClose:() => void, onMutate: () => void, courseThemeDto: CourseThemeDto | null}) => {
    const {
        control,
        handleSubmit,
        formState: { errors, isSubmitting },
    } = useForm<CourseThemeInputDto>({
        defaultValues: {
            themeName: courseThemeDto ? (courseThemeDto.themeName ?? '') : '',
            isActive: courseThemeDto ? courseThemeDto.isActive : true,
        },
    });
    
    const onSubmit = handleSubmit(async (values) => {
        const dto: CourseThemeInputDto = {
              themeName: values.themeName.trim(),
              isActive: values.isActive,
        };

        if (courseThemeDto){
            await editCourseTheme(courseThemeDto.id, dto);
        }
        else {
            await createCourseTheme(dto);
        }
        await onMutate();
        await onClose();
    });

    return (
        <Popup
            title={courseThemeDto ? 'Редактирование темы' : 'Создание темы'}
            onClose={onClose}>
            <form className={styles.form} onSubmit={onSubmit}>
                <div className={styles.field}>
                    <label className={styles.label}>Название</label>
                    <Controller
                        name="themeName"
                        control={control}
                        rules={{ required: 'Обязательное поле' }}
                        render={({ field }) => (
                            <Input
                                {...field}
                                type="string"
                                placeholder={courseThemeDto ? (courseThemeDto.themeName ?? '') : ''}
                            />
                        )}
                    />
                    {errors.themeName && <span className={styles.error}>{errors.themeName.message}</span>}
                </div>
                <div className={styles.splitField}>
                    <label className={styles.label}>Активен</label>
                    <Controller
                        name="isActive"
                        control={control}
                        defaultValue={courseThemeDto ? courseThemeDto.isActive : true}
                        render={({ field: { value, onChange, ref, ...rest } }) => (
                            <input
                                {...rest}
                                ref={ref}
                                type="checkbox"
                                checked={value}
                                onChange={(e) => onChange(e.target.checked)}
                                className={styles.checkbox}
                            />
                        )}
                    />
                    {errors.isActive && <span className={styles.error}>{errors.isActive.message}</span>}
                </div>
                <Button
                    type="submit"
                    variant={ButtonTypeEnum.PRIMARY}
                    size={ButtonSizeEnum.NORMAL}
                    isLoading={isSubmitting}>
                    Добавить тему
                </Button>
            </form>
        </Popup>)
}

const ThemeCard = ({courseTheme, admin, onMutate} : {courseTheme: CourseThemeDto, admin: AdminProfileDto | undefined, onMutate: () => void}) => {
    const [editPopupOpen, setEditPopupOpen] = useState(false);
    
    return (
    <div className={styles.rowSmall}>
        <div className={styles.rowInformation}>
            <h2 className={styles.rowTitle}>{courseTheme.themeName}</h2>
            {courseTheme.isActive ? 
                <Badge text='Активен' badgeType={BadgeTypeEnum.Positive}/> : 
                <Badge text='Неактивен' badgeType={BadgeTypeEnum.Negative}/>}
        </div>
        <Button 
            disabled={!admin?.permissions['EditCourseThemes']}
            variant={ButtonTypeEnum.PRIMARY}
            onClick={async () => {setEditPopupOpen(true)}}>
            Добавить тему
        </Button>
        {editPopupOpen && <ThemeInteractionPopup 
                    onClose={() => setEditPopupOpen(false)}
                    onMutate={async () => await onMutate()} 
                    courseThemeDto={courseTheme}/>}
    </div>
    )
}

export const ThemesList = (admin: AdminProfileDto | undefined) => {
    const { themes, mutate } = useThemesByAdmin()
    const [createPopupOpen, setCreatePopupOpen] = useState(false);

    return (
        <div className={styles.promocodesPage}>
            <div className={styles.listHeader}>
                <Button 
                        disabled={!admin?.permissions['EditCourseThemes']}
                        variant={ButtonTypeEnum.PRIMARY}
                        onClick={async () => {setCreatePopupOpen(true)}}>
                        Добавить тему
                </Button>
                {createPopupOpen && <ThemeInteractionPopup 
                    onClose={() => setCreatePopupOpen(false)}
                    onMutate={async () => await mutate()} 
                    courseThemeDto={null}/>}
            </div>
            <div className={styles.themesList}>
                {themes?.length > 0 ? themes.map((theme) => (
                    <ThemeCard
                        courseTheme={theme}
                        admin={admin}
                        onMutate={async () => await mutate()}
                    />
                )) : <div className={styles.empty}>
                            <EmptyIcon />
                            <p className={styles.emptyText}>
                                Темы не найдены
                            </p>
                        </div>
                    }
            </div>
        </div>
    )
}