import { Controller, useForm } from 'react-hook-form';
import useSWRImmutable from 'swr/immutable';

import { createCourse } from '@/entity/course';
import type { CourseInputDto } from '@/entity/course';
import { getPublicThemes } from '@/entity/course-theme';
import { api } from '@/shared/config/api.ts';
import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';
import { Input } from '@/shared/ui/Input';

import styles from './CreateCourseForm.module.css';

interface CreateCourseFormProps {
  onSuccess: () => void;
  onCancel: () => void;
}

interface FormValues {
  name: string;
  description: string;
  price: string;
  freeLessons: string;
  maxLessons: string;
  courseThemeId: string;
}

export const CreateCourseForm = ({ onSuccess, onCancel }: CreateCourseFormProps) => {
  const {
    control,
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<FormValues>({
    defaultValues: {
      name: '',
      description: '',
      price: '0',
      freeLessons: '0',
      maxLessons: '',
      courseThemeId: '',
    },
  });

  const { data: themesResult, isLoading: themesLoading } = useSWRImmutable(
    [api.publicTheme.getThemes],
    async () => (await getPublicThemes()).data,
    { revalidateOnMount: true },
  );

  const themes = themesResult?.items?.filter((t) => t.isActive) ?? [];

  const onSubmit = handleSubmit(async (values) => {
    const dto: CourseInputDto = {
      name: values.name.trim() || null,
      description: values.description.trim() || null,
      price: parseFloat(values.price) || 0,
      freeLessons: parseInt(values.freeLessons, 10) || 0,
      maxLessons: parseInt(values.maxLessons, 10) || 0,
      courseThemeId: parseInt(values.courseThemeId, 10),
    };
    await createCourse(dto);
    onSuccess();
  });

  return (
    <form className={styles.form} onSubmit={onSubmit}>
      <div className={styles.field}>
        <label className={styles.label}>Название *</label>
        <Controller
          name="name"
          control={control}
          rules={{ required: 'Обязательное поле', minLength: { value: 3, message: 'Минимум 3 символа' } }}
          render={({ field }) => (
            <Input {...field} placeholder="Введите название курса" />
          )}
        />
        {errors.name && <span className={styles.error}>{errors.name.message}</span>}
      </div>

      <div className={styles.field}>
        <label className={styles.label}>Описание</label>
        <Controller
          name="description"
          control={control}
          render={({ field }) => (
            <Input {...field} placeholder="Кратко опишите курс" />
          )}
        />
      </div>

      <div className={styles.field}>
        <label className={styles.label}>Тема *</label>
        <select
          className={styles.select}
          disabled={themesLoading}
          {...register('courseThemeId', { required: 'Выберите тему' })}
        >
          <option value="">— Выберите тему —</option>
          {themes.map((t) => (
            <option key={t.id} value={t.id}>
              {t.themeName}
            </option>
          ))}
        </select>
        {errors.courseThemeId && <span className={styles.error}>{errors.courseThemeId.message}</span>}
      </div>

      <div className={styles.row}>
        <div className={styles.field}>
          <label className={styles.label}>Цена (₽) *</label>
          <Controller
            name="price"
            control={control}
            rules={{ required: 'Укажите цену', min: { value: 0, message: 'Не менее 0' } }}
            render={({ field }) => (
              <Input {...field} type="number" placeholder="0" />
            )}
          />
          {errors.price && <span className={styles.error}>{errors.price.message}</span>}
        </div>

        <div className={styles.field}>
          <label className={styles.label}>Уроков всего *</label>
          <Controller
            name="maxLessons"
            control={control}
            rules={{ required: 'Укажите количество', min: { value: 1, message: 'Минимум 1' } }}
            render={({ field }) => (
              <Input {...field} type="number" placeholder="10" />
            )}
          />
          {errors.maxLessons && <span className={styles.error}>{errors.maxLessons.message}</span>}
        </div>
      </div>

      <div className={styles.field}>
        <label className={styles.label}>Бесплатных уроков</label>
        <Controller
          name="freeLessons"
          control={control}
          rules={{ min: { value: 0, message: 'Не менее 0' } }}
          render={({ field }) => (
            <Input {...field} type="number" placeholder="0" />
          )}
        />
        {errors.freeLessons && <span className={styles.error}>{errors.freeLessons.message}</span>}
      </div>

      <div className={styles.actions}>
        <Button
          variant={ButtonTypeEnum.SECONDARY}
          size={ButtonSizeEnum.NORMAL}
          onClick={onCancel}
        >
          Отмена
        </Button>
        <Button
          type="submit"
          variant={ButtonTypeEnum.PRIMARY}
          size={ButtonSizeEnum.NORMAL}
          isLoading={isSubmitting}
        >
          Создать курс
        </Button>
      </div>
    </form>
  );
};
