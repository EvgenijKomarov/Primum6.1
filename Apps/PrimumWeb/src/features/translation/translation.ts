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

export const GRADES_TRANSLATION = [
  { labels: ['Не оценено'], value: 0, hint: 'Не оценено' },
  { labels: ['Старт отложен', 'Мимо усилий'], value: 1, hint: 'Ученик не справился и не прикладывал усилия' },
  { labels: ['Разведка боем', 'Первые шаги'], value: 2, hint: 'Ученик не справился, но прикладывал усилия' },
  { labels: ['На верном пути', 'Победа с потерями', 'Уверенный джун'], value: 3, hint: 'Ученик справился, но с серьезными недочетами' },
  { labels: ['Почти идеально', 'Победа', 'Уровень мидла'], value: 4, hint: 'Ученик справился успешно, но допустил мелкие недочеты' },
  { labels: ['Чистый код', 'Настоящий хакер', 'Уровень сеньора', 'Блестящая победа'], value: 5, hint: 'Ученик замечательно справился' },
]

export function translateAbonementStatus(status: AbonementStatus): {label: string, badgeType: BadgeTypeEnum, cls: string} {
    return STATUS_TRANSLATION[status];
}

export function translateDayOfWeek(dow: DayOfWeek): string {
    return DAY_LABELS[dow];
}

export function translateMonth(month: number): string {
    return RU_MONTHS[month];
}

export function translateGrade(grading: number, fixedLabel = false): { label: string, value: number, hint: string} {
  const grade = GRADES_TRANSLATION[grading];
  return { 
    label: fixedLabel ? grade.labels[0] : grade.labels[Math.floor(Math.random() * grade.labels.length)],
    value: grade.value,
    hint: grade.hint
  };
}

export function translateBoolean(input: boolean): string {
  return input ? 'Да' : 'Нет';
}