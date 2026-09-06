using PublishServiceConnection.Abstractions;
using Resourses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace PublishServiceConnection.Events
{
    public class TeacherExperienceGainEvent: ICommonNotification
    {
        public required int TeacherUserId { get; set; }

        public required string TeacherName { get; set; }

        public required int CourseId { get; set; }

        public required string CourseName { get; set; }

        public required int CourseExp {  get; set; }

        public required int TeacherExp { get; set; }

        public Dictionary<int, string> ToCommonNotifications()
        {
            return new Dictionary<int, string>
            {
                [TeacherUserId] = $"Вы получили {TeacherExp} опыта, а Ваш курс {CourseExp} - {CourseExp} опыта",
            };
        }
    }
}
