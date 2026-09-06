import { ButtonSizeEnum, ButtonTypeEnum } from '@/shared/enums';
import Button from '@/shared/ui/Button/Button.tsx';
import styles from '../ui/ProfilePage.module.css';
import { useTeacherProfile } from '@/entity/teacher';
import { Badge } from '@/shared/ui/Badge/Badge';
import { BadgeTypeEnum } from '@/shared/enums/badge';
import { Card } from '@/shared/ui/Card/Card';
import { StatCard } from '@/shared/ui/StatCard/StatCard';
import { TeacherRankInfo } from '@/widgets/popups/rank-info/teacher-rank-info/TeacherRankInfo';
import { Popup } from '@/shared/ui/Popup';
import { useState } from 'react';
import { createTeacherProfile, type CreateTeacherProfileRequest, type UserDto } from '@/entity/user';
import { Controller, useForm } from 'react-hook-form';
import { Input } from '@/shared/ui/Input';
import { TeacherEarningInfo } from '@/widgets/popups/info/teacher-earning-info/TeacherEarningInfo';

const TeacherRegistrationPopup = ({onClose, mutateUser}: {onClose: () => void, mutateUser: () => void}) => {
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
      await onClose();
      await mutateUser();
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

interface Props {
  user: UserDto;
  mutateUser: () => void;
}

export const TeacherCard = ({
  user,
  mutateUser,
}: Props) => {

  const { teacherProfile, isLoading: teacherLoading } = useTeacherProfile(
    user.isApprovedTeacher === true &&
      user.isApprovedStudent !== undefined &&
      user.isAvailable === true,
  );

  const [regPopupOpen, setRegPopupOpen] = useState(false);
  if (user.isApprovedTeacher === undefined) return null;

  return (
    <Card title="Профиль преподавателя"  width={'40rem'}>

      {/* Pending approval */}
      {user.isApprovedTeacher === false && (
        <p className={styles.warning}>
          Ваш профиль преподавателя находится на рассмотрении. Возможно, с Вами свяжутся через почту или привязанные мессенджеры
        </p>
      )}

      {/* Not created yet */}
      {user.isApprovedTeacher === null && (
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
              onClose={() => {setRegPopupOpen(false)}}
              mutateUser={mutateUser}
            />}
        </>
      )}

      {/* Approved — loading */}
      {user.isApprovedTeacher === true && (teacherLoading || !teacherProfile) && <div style={{ height: '4rem' }} />}

      {/* Approved — loaded */}
      {user.isApprovedTeacher === true && teacherProfile && (
        <>
          {teacherProfile.isAvailable === true ? (
            <Badge text="Доступен" badgeType={BadgeTypeEnum.Positive} />
          ): (
            <Badge text="Недоступен" badgeType={BadgeTypeEnum.Negative} />
          )}

          <div className={styles.stats}>
            {[
              { label: 'Уровень', value: teacherProfile.level },
              { label: 'Ранг', value: <TeacherRankInfo rankInput={teacherProfile.rank} /> },
              { label: 'Опыт', value: teacherProfile.experience },
              { label: 'Конверсия', value: teacherProfile.convertionIndex ? teacherProfile.convertionIndex * 100 : '--'},
              { label: 'Доход с урока', value: <TeacherEarningInfo/> },
            ].map(({ label, value }) => (
              <StatCard
                key={label}
                title={label}
                value={value}
              />
            ))}
          </div>
          {teacherProfile.about && <p className={styles.about}>{teacherProfile.about}</p>}
        </>
      )}
    </Card>
  );
};
