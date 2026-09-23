using CoreDBModel.Models;

namespace CoreDBIteratorTests.Infrastructure
{
    /// <summary>
    /// Ученик, преподаватель, курс и абонемент. Данные подобраны так, что id профиля преподавателя
    /// совпадает с id постороннего пользователя и не совпадает с id пользователя-преподавателя:
    /// если уведомление адресовано по id профиля, его получит посторонний, и тест это увидит.
    /// </summary>
    public sealed record LessonScenario(
        int OutsiderUserId,
        int StudentUserId,
        int TeacherUserId,
        int TeacherProfileId,
        int AbonementId)
    {
        public static LessonScenario Create(PrimumContext db, decimal price = 1000m)
        {
            var seed = new Seed(db);
            var outsider = seed.User("Outsider");
            var student = seed.Student(seed.User("Student"));
            var teacher = seed.Teacher(seed.User("Teacher"));
            var course = seed.Course(teacher, price);
            var abonement = seed.Abonement(course, student);

            // Падение, а не Inconclusive: пропущенный тест в CI легко не заметить
            if (teacher.Id != outsider.Id || teacher.Id == teacher.UserId)
                Assert.Fail("LessonScenario нужно создавать первым на чистой базе: иначе id профиля преподавателя не отличить от id пользователя");

            return new LessonScenario(outsider.Id, student.UserId, teacher.UserId, teacher.Id, abonement.Id);
        }
    }
}
