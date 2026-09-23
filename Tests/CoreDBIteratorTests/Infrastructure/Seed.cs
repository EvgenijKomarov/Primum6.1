using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CoreDBIteratorTests.Infrastructure
{
    /// <summary>Создаёт связанные сущности с разумными значениями по умолчанию.</summary>
    public sealed class Seed(PrimumContext db)
    {
        private int _userCounter;

        public User User(string name = "User")
        {
            var index = ++_userCounter;
            var user = new User
            {
                Name = name,
                Surname = "Test",
                Patronymic = "Qa",
                MailAdress = $"{name.ToLowerInvariant()}{index}@test.local",
                Password = "hash",
            };
            return Save(user);
        }

        public TeacherProfile Teacher(User? user = null, bool isBanned = false)
        {
            user ??= User("Teacher");
            user.IsBanned = isBanned;
            return Save(new TeacherProfile { User = user, About = "about", ApproveStatus = ApproveStatus.Approved });
        }

        public StudentProfile Student(User? user = null) =>
            Save(new StudentProfile { User = user ?? User("Student"), ApproveStatus = ApproveStatus.Approved });

        public Course Course(TeacherProfile teacher, decimal price = 1000m, int freeLessons = 0) =>
            Save(new Course
            {
                Name = "Python",
                About = "about",
                Teacher = teacher,
                Price = price,
                FreeLessons = freeLessons,
                MaxLessons = 2,
                CourseThemeId = 1,
                ReferalToken = Guid.NewGuid().ToString("N"),
                ApproveStatus = ApproveStatus.Approved,
            });

        public Abonement Abonement(
            Course course,
            StudentProfile student,
            AbonementStatus status = AbonementStatus.Active,
            int freeLessons = 0,
            int cancelledLessons = 0) =>
            Save(new Abonement
            {
                Course = course,
                Student = student,
                PricePerLesson = course.Price,
                FreeLessons = freeLessons,
                AbonementStatus = status,
                CancelledLessons = cancelledLessons,
            });

        public Lesson Lesson(
            Abonement abonement,
            DateTime dateTime,
            LessonStatus status = LessonStatus.Waiting,
            decimal? price = null,
            bool isWorkoff = false) =>
            Save(new Lesson
            {
                Abonement = abonement,
                DateTime = dateTime,
                Status = status,
                Price = price ?? abonement.PricePerLesson,
                IsWorkoff = isWorkoff,
            });

        public AbonementShedule Schedule(Abonement abonement, DayOfWeek day, int hour, DateTime lastIteration)
        {
            var teacherShedule = Save(new TeacherShedule { TeacherId = abonement.Course.TeacherId, DayOfWeek = day, Time = hour });
            return Save(new AbonementShedule { Abonement = abonement, TeacherShedule = teacherShedule, LastIteration = lastIteration });
        }

        public VerificationToken Token(User user, DateTime lifeTime, string token) =>
            Save(new VerificationToken { User = user, LifeTime = lifeTime, Token = token, Meaning = TokenMeaning.EmailVerification });

        /// <summary>SaveChanges проставляет CreatedAt = сейчас; для тестов на порядок абонементов задаём его вручную</summary>
        public void SetCreatedAt(BaseEntity entity, DateTime createdAt)
        {
            entity.CreatedAt = createdAt;
            db.Entry(entity).Property(e => e.CreatedAt).IsModified = true;
            db.SaveChanges();
        }

        private T Save<T>(T entity) where T : class
        {
            db.Add(entity);
            db.SaveChanges();
            return entity;
        }
    }
}
