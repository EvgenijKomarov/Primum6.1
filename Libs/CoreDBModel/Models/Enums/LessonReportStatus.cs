using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDBModel.Models.Enums
{
    public enum LessonReportStatus
    {
        Ok = 1,

        TeacherHaventConnected = 2,
        StudentHaventConnected = 3,

        TeacherInappropriateContent = 4,
        StudentInappropriateContent = 5,

        TeacherBadBehavior = 6,
        StudentBadBehavior = 7,


    }
}
