import { useCurrentUser, useUserStore } from "@/entity/user";
import TypewriterText from "../../common-elements/TypewriterText/TypewriterText";
import styles from './GeneralBlock.module.css'
import { useNavigate } from "react-router";
import { ConsultationBlock } from "../../components/ConsultationBlock/ConsultationBlock";
import { RichButton } from "../../components/RichButton/RichButton";

export const GeneralBlock = () => {
    const { user, isLoading } = useCurrentUser();
    const token = useUserStore((s) => s.token);
    const navigate = useNavigate();

    // Пока грузится профиль вошедшего пользователя, не показываем форму гостя:
    // иначе она мелькает и вёрстка прыгает, когда её заменяет кнопка входа в кабинет
    const isUserPending = isLoading && !!token;

    return (
    <div className={styles.generalBlock}>
        <div className={styles.headerTitle}>
            <TypewriterText className={styles.title} text='PrimumCode'/>
            <TypewriterText className={styles.subtitle} text='Там, где идеи становятся кодом'/>
        </div>
        <div className={styles.rightBlock}>
            {isUserPending ? null : user === undefined ? (
                <>
                    <div className={styles.text}>
                        <p className={styles.defTextTitle} style={{paddingBottom: '1rem'}}>Желаете проконсультироваться?</p>
                        <p className={styles.defText}>Оставьте заявку, и мы свяжемся с Вами</p>
                    </div>
                    <ConsultationBlock/>
                    <p className={styles.defText}>или</p>
                    <RichButton
                        label='Войти/Зарегистрироваться'
                        onClick={() => navigate('/auth')}/>
                </>
            ) : (
                <>
                    <RichButton
                        label={`Войти как ${user.name}`}
                        onClick={() => navigate('/profile')}/>
                </>
            )}
        </div>
    </div>
    );
}
