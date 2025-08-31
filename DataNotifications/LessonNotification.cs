namespace DataNotifications
{
    public class LessonNotification()
    {
        public required DateTime DateTime { get; set; }

        public required int StudentUserId { get; set; }

        public required int TeacherUserId { get; set; }

        public required string TeacherLink { get; set; }

        public required string StudentLink { get; set; }
    }
}
