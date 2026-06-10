import { AbonementStatus } from "@/entity/abonement/model/types";
import { BadgeTypeEnum } from "@/shared/enums/badge";
import styles from './styles.module.css'
import { DayOfWeek } from "@/entity/schedule/model/types";

const STATUS_TRANSLATION: Record<AbonementStatus, {label: string, badgeType: BadgeTypeEnum, cls: string}> = {
        [AbonementStatus.Active]: { label: 'Активен', badgeType: BadgeTypeEnum.Positive, cls: styles.Positive},
        [AbonementStatus.Freezed]: { label: 'Заморожен', badgeType: BadgeTypeEnum.Warning, cls: styles.Warning},
        [AbonementStatus.Deleted]: { label: 'Удален', badgeType: BadgeTypeEnum.Negative, cls: styles.Negative},
}

const DAY_LABELS: Record<DayOfWeek, string> = {
  [DayOfWeek.Monday]:    'Понедельник',
  [DayOfWeek.Tuesday]:   'Вторник',
  [DayOfWeek.Wednesday]: 'Среда',
  [DayOfWeek.Thursday]:  'Четверг',
  [DayOfWeek.Friday]:    'Пятница',
  [DayOfWeek.Saturday]:  'Суббота',
  [DayOfWeek.Sunday]:    'Воскресенье',
};

const RU_MONTHS = ['января', 'февраля', 'марта', 'апреля', 'мая', 'июня',
  'июля', 'августа', 'сентября', 'октября', 'ноября', 'декабря'];

export function translateAbonementStatus(status: AbonementStatus): {label: string, badgeType: BadgeTypeEnum} {
    return STATUS_TRANSLATION[status];
}

export function translateDayOfWeek(dow: DayOfWeek): string {
    return DAY_LABELS[dow];
}

export function translateMonth(month: number): string {
    return RU_MONTHS[month];
}