import { Controller, useForm } from 'react-hook-form';
import styles from './ConsultationBlock.module.css'
import type { ConsultationRequestInput } from '@/entity/consultation-request/model/types';
import { requestConsultation } from '@/entity/consultation-request/api/consultation-request.api';
import { useState } from 'react';
import { Input } from '@/shared/ui/Input';

export const ConsultationBlock = () => {
    const [isSent, setIsSent] = useState(false);
    const {
            control,
            handleSubmit,
            formState: { errors },
          } = useForm<ConsultationRequestInput>({
            defaultValues: {
                displayName: '',
                phoneNumber: '',
                email: '',
            },
          });
    
    const onSubmit = handleSubmit(async (values) => {
        const dto: ConsultationRequestInput = {
            displayName: values.displayName.trim(),
            phoneNumber: values.phoneNumber.trim(),
            email: values.email.trim(),
        };
        await requestConsultation(dto);
        await setIsSent(true);
    });

    return (<>
        <form className={styles.form} onSubmit={onSubmit}>
            <div className={styles.field}>
                <label className={styles.label}>ФИО</label>
                <Controller
                    name="displayName"
                    control={control}
                    rules={{ required: 'Обязательное поле'}}
                    render={({ field }) => (
                    <Input {...field} placeholder="Как к Вам обращаться?" />
                    )}
                />
                {errors.displayName && <span className={styles.error}>{errors.displayName.message}</span>}
                </div>
                <div className={styles.field}>
                    <label className={styles.label}>Номер телефона</label>
                    <Controller
                        name="phoneNumber"
                        control={control}
                        rules={{ required: 'Обязательное поле' }}
                        render={({ field }) => (
                        <Input {...field} placeholder="Как с Вами связаться?" />
                        )}
                    />
                    {errors.phoneNumber && <span className={styles.error}>{errors.phoneNumber.message}</span>}
                </div>
                <div className={styles.field}>
                    <label className={styles.label}>Почта</label>
                    <Controller
                        name="email"
                        control={control}
                        rules={{ required: 'Обязательное поле' }}
                        render={({ field }) => (
                        <Input {...field} placeholder="Если не дозвонимся" />
                        )}
                    />
                    {errors.email && <span className={styles.error}>{errors.email.message}</span>}
                </div>
                <button
                type="submit"
                disabled={isSent}>
                    {isSent ? 'Отправлено!' :  'Оставить заявку'}
                </button>
        </form>
        </>
    );
}