import type { AdminProfileDto } from "@/entity/admin/model/types";
import styles from './PlatformConfigPage.module.css';
import { usePromocodes } from "@/entity/promocode/model/useAdminPromocodes";
import { useDebouncedValue } from "@/shared/lib/debounced/debounced";
import Button from "@/shared/ui/Button/Button";
import { ButtonSizeEnum, ButtonTypeEnum } from "@/shared/enums";
import { useState } from "react";
import { Popup } from "@/shared/ui/Popup";
import type { PromocodeDto, PromocodeInputDto } from "@/entity/promocode/model/types";
import { Controller, useForm } from "react-hook-form";
import { Input } from "@/shared/ui/Input";
import { addPromocode, deletePromocode } from "@/entity/promocode/api/promocodesByAdmin.api";
import { DeleteIcon, EmptyIcon, PlusIcon } from "@/shared/icons/types";

const CreatePromocodePopup = ({onClose, onMutate}: {onClose:() => void, onMutate: () => void}) => {
    const {
        control,
        handleSubmit,
        formState: { errors, isSubmitting },
      } = useForm<PromocodeInputDto>({
        defaultValues: {
          code: '',
          description: '',
          title: '',
          coinsPrice: 100,
        },
      });

    const onSubmit = handleSubmit(async (values) => {
        const dto: PromocodeInputDto = {
          title: values.title.trim(),
          description: values.description.trim(),
          coinsPrice: values.coinsPrice,
          code: values.code.trim()
        };
        await addPromocode(dto);
        onMutate();
      });
    

    return (
        <Popup
            title={'Создание промокода'}
            onClose={onClose}>
            <form className={styles.form} onSubmit={onSubmit}>
                <div className={styles.field}>
                    <label className={styles.label}>Название</label>
                    <Controller
                        name="title"
                        control={control}
                        rules={{ required: 'Обязательное поле' }}
                        render={({ field }) => (
                        <Input {...field} type="string" placeholder="" />
                        )}
                    />
                    {errors.title && <span className={styles.error}>{errors.title.message}</span>}
                </div>
                <div className={styles.field}>
                    <label className={styles.label}>Цена</label>
                    <Controller
                        name="coinsPrice"
                        control={control}
                        rules={{ min: { value: 0, message: 'Не менее 0' }, required: 'Обязательное поле' }}
                        render={({ field }) => (
                        <Input {...field} type="number" placeholder="100" />
                        )}
                    />
                    {errors.coinsPrice && <span className={styles.error}>{errors.coinsPrice.message}</span>}
                </div>
                <div className={styles.field}>
                    <label className={styles.label}>Описание</label>
                    <Controller
                        name="description"
                        control={control}
                        rules={{ required: 'Обязательное поле' }}
                        render={({ field }) => (
                        <Input {...field} type="string" placeholder=""/>
                        )}
                    />
                    {errors.description && <span className={styles.error}>{errors.description.message}</span>}
                </div>
                <div className={styles.field}>
                    <label className={styles.label}>Код</label>
                    <Controller
                        name="code"
                        control={control}
                        rules={{ required: 'Обязательное поле' }}
                        render={({ field }) => (
                        <Input {...field} type="string" placeholder=""/>
                        )}
                    />
                    {errors.code && <span className={styles.error}>{errors.code.message}</span>}
                </div>
                <Button
                    type="submit"
                    variant={ButtonTypeEnum.PRIMARY}
                    size={ButtonSizeEnum.NORMAL}
                    isLoading={isSubmitting}>
                    Добавить промокод
                </Button>
            </form>
        </Popup>)
}

const PromocodeCard = ({card, admin, onMutate}: {card: PromocodeDto, admin: AdminProfileDto | undefined, onMutate:() => void}) => {
    return (
        <div className={styles.row}>
            <div className={styles.rowInformation}>
                <h2 className={styles.rowTitle}>{card.title}</h2>
                <p className={styles.rowDescription}>{card.description}</p>
            </div>
            <div className={styles.rowFooter}>
                {!card.isAvailable && 
                    <p className={styles.warningMes}>
                        {`Принадлежит ученику с id: ${card.studentId}`}
                    </p>}
                <div className={styles.rowPrice}>
                    <label>Цена</label>
                    <p>{card.coinsPrice}</p>
                </div>
                <Button
                    onClick={async () => {await deletePromocode(); await onMutate();}}
                    disabled={(!admin?.permissions['DeletePromocodes']) || !card.isAvailable}
                    variant={ButtonTypeEnum.PRIMARY}
                    icon={<DeleteIcon/>}
                    size={ButtonSizeEnum.NORMAL}>
                    Удалить промокод
                </Button>
            </div>
        </div>
    )
}

export const PromocodesList = (admin: AdminProfileDto | undefined) => {
    const { debouncedValue, value, setValue } = useDebouncedValue('', 400);
    const { promocodes, mutate } = usePromocodes(debouncedValue)
    const [createPopupOpen, setCreatePopupOpen] = useState(false);

    return (
        <div className={styles.promocodesPage}>
            <div className={styles.listHeader}>
                <textarea
                    className={styles.textarea}
                    placeholder="Поиск по промокодам"
                    value={value}
                    onChange={(e) => setValue(e.target.value)}
                />
                <Button 
                        disabled={!admin?.permissions['AddPromocodes']}
                        variant={ButtonTypeEnum.PRIMARY}
                        icon={<PlusIcon/>}
                        onClick={async () => {setCreatePopupOpen(true)}}>
                        Добавить промокод
                </Button>
                {createPopupOpen && <CreatePromocodePopup 
                    onClose={() => setCreatePopupOpen(false)}
                    onMutate={async () => await mutate()} />}
            </div>
            <div className={styles.promocodesList}>
                {promocodes?.length > 0 ? promocodes.map((code) => (
                    <PromocodeCard
                        card={code}
                        admin={admin}
                        onMutate={async () => await mutate()}
                    />
                )) : <div className={styles.empty}>
                            <EmptyIcon />
                            <p className={styles.emptyText}>
                                Промокоды не найдены
                            </p>
                        </div>
                    }
            </div>
        </div>
    )
}