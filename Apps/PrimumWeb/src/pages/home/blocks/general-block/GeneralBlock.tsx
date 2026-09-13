import { useCurrentUser } from "@/entity/user";
import TypewriterText from "../../common-elements/TypewriterText/TypewriterText";
import styles from './GeneralBlock.module.css'
import { Card } from "@/shared/ui/Card/Card";
import { Controller, useForm } from "react-hook-form";
import type { ConsultationRequestInput } from "@/entity/consultation-request/model/types";
import { requestConsultation } from "@/entity/consultation-request/api/consultation-request.api";
import Button from "@/shared/ui/Button/Button";
import { ButtonSizeEnum, ButtonTypeEnum } from "@/shared/enums";
import { Input } from "@/shared/ui/Input";
import { useNavigate } from "react-router";
import { useState } from "react";

const ConsultationForm = ({setIsSent}: {setIsSent: () => void}) => {
    const {
        control,
        handleSubmit,
        formState: { errors, isSubmitting },
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
        await setIsSent();
    });

    return (
        <div className={styles.requestConsultationForm}>
            <Card title='Нужна консультация?'>
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
                    <Button
                    type="submit"
                    variant={ButtonTypeEnum.PRIMARY}
                    size={ButtonSizeEnum.NORMAL}
                    isLoading={isSubmitting}>
                        Оставить заявку
                    </Button>
                </form>
            </Card>
        </div>
    );
}

export const GeneralBlock = () => {
    const { user, isLoading } = useCurrentUser();
    const navigate = useNavigate();

    const [isSent, setIsSent] = useState(false);

    return (
    <div className={styles.generalBlock}>
        <div className={styles.headerTitle}>
            <TypewriterText className={styles.title} text='PrimumCode'/>
            <TypewriterText className={styles.subtitle} text='Там, где идеи становятся кодом'/>
        </div>
        <div className={styles.rightBlock}>
            {(user === undefined || isLoading) ? (
                <>
                    {!isSent ? (
                        <>
                            <ConsultationForm setIsSent={() => setIsSent(true)}/>
                            <p className={styles.defText}>или</p>
                        </>) : (
                        <>
                            <p 
                                className={styles.defText}
                                style={{color: 'var(--color-primary-base)'}}>Заявка отправлена! С Вами скоро свяжутся</p>
                        </>)}
                    <Button 
                        variant={ButtonTypeEnum.PRIMARY} 
                        size={ButtonSizeEnum.SMALL} 
                        onClick={() => navigate('/auth')}>
                        Войти/Зарегистрироваться
                    </Button>
                </>
            ) : (
                <>
                    <button 
                        className={styles.bigButton}
                        onClick={() => navigate('/profile')}>
                    {`Войти как ${user.name}`}
                    </button>
                </>
            )}
        </div>
    </div>
    );
}