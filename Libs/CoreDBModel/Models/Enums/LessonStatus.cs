using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreDBModel.Models.Enums
{
    public enum LessonStatus
    {
        //ЕЩЕ ДОЛГО ДО ЗАНЯТИЯ
        Waiting = 0, // ожидает начала

        //ЗА СУТКИ ДО, УЖЕ НЕЛЬЗЯ ОТМЕНИТЬ
        Warned = 1, // предупрежден за сутки

        //НАСТУПИЛО ВРЕМЯ
        Happened = 2, // произошло

        Missed = 3, //пропущено без объяснений, надо разобраться почему
        MissedWithoutReason = 4, //пропущено без уважительной причины
        MissedDueToException = 5, // пропущено из-за технических проблем или ошибок
        MissedDueToFreezing = 7, // пропущено из-за заморозки занятия

        Cancelled = 6, // отменен учеником
    }
}
