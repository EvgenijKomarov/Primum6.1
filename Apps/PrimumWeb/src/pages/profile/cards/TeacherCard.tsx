import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';
import styles from '../ui/ProfilePage.module.css';
import type { TeacherProfileDto } from '@/entity/teacher';
import { Badge } from '@/shared/ui/Badge/Badge';
import { BadgeTypeEnum } from '@/shared/enums/badge';
import { Card } from '@/shared/ui/Card/Card';
import { StatCard } from '@/shared/ui/StatCard/StatCard';
import { TeacherRankInfo } from '@/widgets/popups/rank-info/teacher-rank-info/TeacherRankInfo';
import { Popup } from '@/shared/ui/Popup';
import { useState } from 'react';
import { createTeacherProfile, type CreateTeacherProfileRequest } from '@/entity/user';
import { Controller, useForm } from 'react-hook-form';
import { Input } from '@/shared/ui/Input';
import { TeacherEarningInfo } from '@/widgets/popups/info/teacher-earning-info/TeacherEarningInfo';

interface Props {
  /** true = approved, false = pending, null = not created, undefined = not a teacher */
  isApproved: boolean | null | undefined;
  profile: TeacherProfileDto | undefined;
  isLoading: boolean;
}

const TeacherRegistrationPopup = ({onClose}: {onClose: () => void}) => {
  
  const {
          control,
          handleSubmit,
          formState: { errors, isSubmitting },
        } = useForm<CreateTeacherProfileRequest>({
          defaultValues: {
            about: "",
            inn: "",
            phone: "",
            accountNumber: "",
            bankBIC: ""
          },
        });

  const onSubmit = handleSubmit(async (values) => {
      const dto: CreateTeacherProfileRequest = {
        about: values.about.trim(),
        inn: values.inn.trim(),
        phone: values.phone.trim(),
        accountNumber: values.accountNumber.trim(),
        bankBIC: values.bankBIC.trim()
      };
      await createTeacherProfile(dto);
      onClose();
    });

  return (
  <Popup 
    title="Создание профиля преподавателя"
    onClose={onClose}>
    <form className={styles.form} onSubmit={onSubmit}>
      <div className={styles.field}>
        <label className={styles.label}>ИНН</label>
        <Controller
          name="inn"
          control={control}
          rules={{ required: 'Обязательное поле' }}
          render={({ field }) => (
            <Input {...field} type="string" placeholder="" />
          )}
        />
        {errors.inn && <span className={styles.error}>{errors.inn.message}</span>}
      </div>
      <div className={styles.field}>
        <label className={styles.label}>Номер телефона</label>
        <Controller
          name="phone"
          control={control}
          rules={{ required: 'Обязательное поле' }}
          render={({ field }) => (
            <Input {...field} type="string" placeholder="" />
          )}
        />
        {errors.phone && <span className={styles.error}>{errors.phone.message}</span>}
      </div>
      <div className={styles.field}>
        <label className={styles.label}>Номер счета в банке</label>
        <Controller
          name="accountNumber"
          control={control}
          rules={{ required: 'Обязательное поле' }}
          render={({ field }) => (
            <Input {...field} type="string" placeholder="" />
          )}
        />
        {errors.accountNumber && <span className={styles.error}>{errors.accountNumber.message}</span>}
      </div>
      <div className={styles.field}>
        <label className={styles.label}>БИК банка</label>
        <Controller
          name="bankBIC"
          control={control}
          rules={{ required: 'Обязательное поле' }}
          render={({ field }) => (
            <Input {...field} type="string" placeholder="" />
          )}
        />
        {errors.bankBIC && <span className={styles.error}>{errors.bankBIC.message}</span>}
      </div>
      <div className={styles.field}>
        <label className={styles.label}>О преподавателе</label>
        <Controller
          name="about"
          control={control}
          rules={{ required: 'Обязательное поле' }}
          render={({ field }) => (
            <textarea {...field} placeholder="Расскажите о себе, своем подходе, образовании, и т.д." 
            className={styles.textarea}/>
          )}
        />
        {errors.about && <span className={styles.error}>{errors.about.message}</span>}
      </div>
      <Button
                    type="submit"
                    variant={ButtonTypeEnum.PRIMARY}
                    size={ButtonSizeEnum.NORMAL}
                    isLoading={isSubmitting}>
                    Создать профиль
                </Button>
    </form>
  </Popup>)
}

export const TeacherCard = ({
  isApproved,
  profile,
  isLoading,
}: Props) => {

  const [regPopupOpen, setRegPopupOpen] = useState(false);
  if (isApproved === undefined) return null;

  return (
    <Card title="Профиль преподавателя"  width={'40rem'}>

      {/* Pending approval */}
      {isApproved === false && (
        <p className={styles.warning}>
          Ваш профиль преподавателя находится на рассмотрении. Возможно, с Вами свяжутся через почту или привязанные мессенджеры
        </p>
      )}

      {/* Not created yet */}
      {isApproved === null && (
        <>
          <p className={styles.cardDescription}>
            Создайте профиль преподавателя, чтобы вести курсы и работать с учениками.
          </p>
          <Button
            variant={ButtonTypeEnum.PRIMARY}
            size={ButtonSizeEnum.NORMAL}
            onClick={() => setRegPopupOpen(true)}
          >
            Создать профиль преподавателя
          </Button>
          {regPopupOpen && 
            <TeacherRegistrationPopup
              onClose={() => setRegPopupOpen(false)}
            />}
        </>
      )}

      {/* Approved — loading */}
      {isApproved === true && (isLoading || !profile) && <div style={{ height: '4rem' }} />}

      {/* Approved — loaded */}
      {isApproved === true && profile && (
        <>
          {profile.isAvailable === true ? (
            <Badge text="Доступен" badgeType={BadgeTypeEnum.Positive} />
          ): (
            <Badge text="Недоступен" badgeType={BadgeTypeEnum.Negative} />
          )}

          <div className={styles.stats}>
            {[
              { label: 'Уровень', value: profile.level },
              { label: 'Ранг', value: <TeacherRankInfo rankInput={profile.rank} /> },
              { label: 'Опыт', value: profile.experience },
              { label: 'Конверсия', value: profile.convertionIndex ? profile.convertionIndex * 100 : '--'},
              { label: 'Доход с урока', value: <TeacherEarningInfo/> },
            ].map(({ label, value }) => (
              <StatCard
                key={label}
                title={label}
                value={value}
              />
            ))}
          </div>
          {profile.about && <p className={styles.about}>{profile.about}</p>}
        </>
      )}
    </Card>
  );
};
