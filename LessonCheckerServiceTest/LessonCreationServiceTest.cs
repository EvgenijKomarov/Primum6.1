using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumCore.Services;
namespace CoreServiceTests
{
    public class LessonCreationServiceTest
    {
        public Mock<IPrimumContext> _mockContext;
        public Mock<DbContextFactory<IPrimumContext>> _contextFactory;
        public Mock<ConverterToDateTimeService> _convertionService;

        public LessonCreationService service;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<IPrimumContext>();
            _convertionService = new Mock<ConverterToDateTimeService>(MockBehavior.Loose, 0, new Mock<ILogger<ConverterToDateTimeService>>().Object);
            _contextFactory = new Mock<DbContextFactory<IPrimumContext>>("teststring");
            _contextFactory.Setup(f => f.CreateDbContext()).Returns( _mockContext.Object );

            service = new LessonCreationService(_contextFactory.Object, _convertionService.Object);
        }

        [Test]
        public async Task CreateLessons_WhenRequestValid_LessonsCreated()
        {
            DateTime now = DateTime.Now;
            var abonShedule = new AbonementShedule()
            {
                LastIteration = now.AddDays(-10),
                AbonementSheduleId = 1,
                AbonementId = 1,
                TeacherShedule = new TeacherShedule()
                {
                    DayOfWeek = "lala",
                    Time = 1
                }
            };
            _convertionService.Setup(x => x.GetNextFreeSuitableDate(It.IsAny<string>(), It.IsAny<int>())).Returns(now);
            _mockContext.Setup(x => x.Set<AbonementShedule>()).ReturnsDbSet(new List<AbonementShedule>()
            {
                abonShedule
            });
            _mockContext.Setup(x => x.Set<Lesson>()).ReturnsDbSet(new List<Lesson>());

            await service.IterateAsync();

            _contextFactory.Verify(x => x.SafeSaveChangesAsync(It.IsAny<IPrimumContext>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockContext.Verify(x => x.Set<Lesson>().Add(It.Is<Lesson>(l => l.AbonementId == 1 && l.DateTime == now)), Times.Once);
            Assert.That(abonShedule.LastIteration != now.AddDays(-10));
        }

        [Test]
        public async Task CreateLessons_WhenRequestInvalid_LessonsNotCreated()
        {
            DateTime now = DateTime.Now;
            var abonShedule = new AbonementShedule()
            {
                LastIteration = now.AddDays(10),
                AbonementSheduleId = 1,
                AbonementId = 1,
                TeacherShedule = new TeacherShedule()
                {
                    DayOfWeek = "lala",
                    Time = 1
                }
            };
            _convertionService.Setup(x => x.GetNextFreeSuitableDate(It.IsAny<string>(), It.IsAny<int>())).Returns(now);
            _mockContext.Setup(x => x.Set<AbonementShedule>()).ReturnsDbSet(new List<AbonementShedule>()
            {
                abonShedule
            });
            _mockContext.Setup(x => x.Set<Lesson>()).ReturnsDbSet(new List<Lesson>());

            await service.IterateAsync();

            _contextFactory.Verify(x => x.SafeSaveChangesAsync(It.IsAny<IPrimumContext>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockContext.Verify(x => x.Set<Lesson>().Add(It.Is<Lesson>(l => l.AbonementId == 1 && l.DateTime == now)), Times.Never);
            Assert.That(abonShedule.LastIteration != now.AddDays(-10));
        }
    }
}