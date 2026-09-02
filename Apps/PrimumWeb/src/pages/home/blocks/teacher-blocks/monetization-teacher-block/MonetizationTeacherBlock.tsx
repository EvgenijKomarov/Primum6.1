import { Block } from "@/pages/home/common-elements/Block/Block";
import styles from './MonetizationTeacherBlock.module.css'

export const MonetizationTeacherBlock = () => {
  return (
    <Block title='Монетизация' >
        <div className={styles.content}>
            <p className={styles.text}>
                Цены за уроки вы назначаете сами. Ваш процент от цены урока зависит о уровня профиля, конверсии, и платных занятий в абонементе:
            </p>
            <div className={styles.formula}>
                <p className={styles.formulaPart}>{'Несгораемая часть\n(30%)'}</p>
                <p className={styles.formulaOperation}>+</p>
                <p className={styles.formulaPart}>
                    {`Бонус за конверсию\n(0-20%)`}
                </p>
                <p className={styles.formulaOperation}>+</p>
                <p className={styles.formulaPart}>
                    {`Бонус за уровень\n(0-20%)`}
                </p>
                <p className={styles.formulaOperation}>+</p>
                <p className={styles.formulaPart}>
                    {`Бонус за серию\n(0-10%)`}
                </p>
            </div>
            <p className={styles.textSmall}>
                Минимальная доля от стоимости - 30%, максимальная - 80%.
            </p>
            <div className={styles.description}>
                <div className={styles.descriptionItem}>
                    <p className={styles.descriptionLabel}>
                        Конверсия
                    </p>
                    <p className={styles.descriptionText}>
                        - это абонементы, у которых хотя бы один урок был оплачен. Для вычисления берется среднее значение среди 10 последних абонементов, у которых прошли бесплатные уроки.
                    </p>
                </div>
                <div className={styles.descriptionItem}>
                    <p className={styles.descriptionLabel}>
                        Уровень
                    </p>
                    <p className={styles.descriptionText}>
                        - элемент геймификации. За проведенные уроки Вы и Ваш курс получаете опыт и уровень. Чем выше уровень, тем больше бонус.
                    </p>
                </div>
                <div className={styles.descriptionItem}>
                    <p className={styles.descriptionLabel}>
                        Серия
                    </p>
                    <p className={styles.descriptionText}>
                        - за каждый оплаченный урок в абонементе начисляется бонус в 2,5%. Максимум можно получить 10% 
                    </p>
                </div>
            </div>
        </div>
    </Block>
  );
}