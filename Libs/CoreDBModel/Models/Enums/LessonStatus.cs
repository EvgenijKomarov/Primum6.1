using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreDBModel.Models.Enums
{
    public enum LessonStatus
    {
        Waiting = 0, // ожидает начала
        Warned = 1, // предупрежден за сутки
        Happened = 2, // произошло
        Missed = 3, //пропущено без объяснений, надо разобраться почему
        MissedWithoutReason = 4 //пропущено без уважительной причины
    }
}
