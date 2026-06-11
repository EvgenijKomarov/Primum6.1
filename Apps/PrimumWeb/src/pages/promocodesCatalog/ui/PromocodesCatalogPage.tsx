import { useState } from 'react';
import styles from './PromocodesCatalogPage.module.css';
import { usePromocodes } from '@/entity/promocode/model/usePromocodes';
import { useStudentProfile } from '@/entity/student';
import { CoinIcon, EmptyIcon } from '@/shared/icons/types';
import type { PromocodeDto } from '@/entity/promocode/model/types';
import { Card } from '@/shared/ui/Card/Card';
import Button from '@/shared/ui/Button/Button';
import { EnsurancePopup } from '@/widgets/popups/ensurance-popup/ui/EnsurancePopup';
import { buyPromocode } from '@/entity/promocode/api/promocode.api';
import { useToast } from '@/shared/ui/Toast/useToast';

interface PromocodeCardProps {
    promocode: PromocodeDto,
    studentCoins: number,
    mutateEntities: () => void
}
const PromocodeCard = ({ promocode, studentCoins, mutateEntities }: PromocodeCardProps) => {
    const [buyPopupOpen, setBuyPopupOpen] = useState<boolean>(false);
    const [showCode, setShowCode] = useState<boolean>(false);
    const { showToast } = useToast();

    const handleBuyingPromocode = async () => {
        await buyPromocode(promocode.id);
        mutateEntities();
    }

    const handleCopyingCode = async () => {
        await navigator.clipboard.writeText(promocode.code ?? '');
        showToast('Код скопирован в буфер обмена', 'success');
    }

    const codeVisible = showCode && promocode.code;

    return (<Card hoverable={true} width={66}>
                <span className={styles.cardTitle}>{promocode.title}</span>
                {promocode.isAvailable ? (
                    <div className={styles.cardContentAvailable}>
                        <span className={styles.cardDescription}>{promocode.description}</span>
                        <div className={styles.buttonRow}>
                            <Button 
                                icon={<CoinIcon/>}
                                disabled={studentCoins < promocode.coinsPrice}
                                onClick={()=>{setBuyPopupOpen(true)}}>
                                    {promocode.coinsPrice}
                            </Button>
                        </div>
                    </div>
                    ) : (
                        <div className={styles.cardContent}>
                            <div className={styles.cardSection}>
                                <span className={styles.cardDescription}>{promocode.description}</span>
                            </div>
                            <div className={styles.cardSection}>
                                    <div className={styles.cardCode}>
                                        {codeVisible ? (
                                            <div className={styles.cardCodeHeader}>
                                                <span className={styles.hint}>нажмите на код, чтобы скопировать в буфер обмена</span>
                                                <span 
                                                    className={`${styles.cardCodeValue} ${styles.shown}`} 
                                                    onClick={handleCopyingCode}>{promocode.code}</span>
                                            </div>
                                        ):(
                                            <span className={styles.cardCodeValue}>Код скрыт</span>
                                        )}
                                        <Button onClick={() => {setShowCode(!showCode)}}>
                                            {`Нажмите, чтобы ${showCode ? 'скрыть' : 'показать'} промокод`}
                                        </Button>
                                    </div>
                            </div>
                        </div>
                    )}
                {buyPopupOpen && (
                    <EnsurancePopup 
                        description={`Вы уверены, что хотите приобрести этот промокод за ${promocode.coinsPrice} монет?`}
                        setPopupOpen={setBuyPopupOpen}
                        onConfirm={handleBuyingPromocode}/>
                )}
            </Card>);
        }

export const PromocodesCatalogPage = () => {
    const [onlyBought, setOnlyBought] = useState<boolean>(false);
    const { studentProfile, mutate: mutateStudent } = useStudentProfile(true);
    const { promocodes, mutate: mutatePromocodes } = usePromocodes(onlyBought);

    return (
    <div className={styles.page}>
        <div className={styles.header}>
            <h1 className={styles.title}>Каталог промокодов</h1>
            <p className={styles.subtitle}>Вы можете обменять монеты, полученные за занятия, на цифровые товары</p>
            <div className={styles.balance}>
                <p className={styles.balanceLabel}>Монеты: </p>
                <p className={styles.balanceValue}>{studentProfile?.coins}</p>
                <CoinIcon/>
            </div>
        </div>

        <div className={styles.filterBar}>
            <button
                className={`${styles.chip} ${!onlyBought ? styles.chipActive : ''}`}
                onClick={() => setOnlyBought(false)}>
            Все
            </button>
            <button
                className={`${styles.chip} ${onlyBought ? styles.chipActive : ''}`}
                onClick={() => setOnlyBought(true)}>
            Купленные
            </button>
        </div>

        <div className={styles.content}>
            {promocodes.length !== 0 ? (
                promocodes.map((promocode, index) => (
                        <PromocodeCard 
                            key={`${promocode.id}-${index}`}
                            promocode={promocode} 
                            studentCoins={studentProfile?.coins ?? 0}
                            mutateEntities={() => { mutateStudent(); mutatePromocodes(); }}/>
                    ))
            ) : (
                <div className={styles.empty}>
                    <EmptyIcon />
                    <p className={styles.emptyText}>
                        {!onlyBought ? 'Промокоды закончились. Дождитесь поступления новых!' : 'Вы еще не купили ни один промокод'}
                    </p>
                </div>
            )}
        </div>
    </div>);
}