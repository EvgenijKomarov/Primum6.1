import { useCurrentUser } from "@/entity/user";
import TypewriterText from "../../common-elements/TypewriterText/TypewriterText";
import styles from './GeneralBlock.module.css'
import { useNavigate } from "react-router";
import { ConsultationBlock } from "../../components/ConsultationBlock/ConsultationBlock";
import { RichButton } from "../../components/RichButton/RichButton";

export const GeneralBlock = () => {
    const { user, isLoading } = useCurrentUser();
    const navigate = useNavigate();

    return (
    <div className={styles.generalBlock}>
        <div className={styles.headerTitle}>
            <TypewriterText className={styles.title} text='PrimumCode'/>
            <TypewriterText className={styles.subtitle} text='Там, где идеи становятся кодом'/>
        </div>
        <div className={styles.rightBlock}>
            {(user === undefined || isLoading) ? (
                <>
                    <p className={styles.defTextTitle}>Желаете проконсультироваться?</p>
                    <p className={styles.defText}>Оставьте заявку, и мы свяжемся с Вами</p>
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