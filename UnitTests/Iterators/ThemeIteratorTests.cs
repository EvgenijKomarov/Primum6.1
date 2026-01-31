using Moq;
using Moq.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumCore.Services.Iterators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Iterators
{
    public class ThemeIteratorTests
    {
        private Mock<IPrimumContext> _mockContext = null!;
        private ThemeIterator _iterator = null!;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<IPrimumContext>();
            _iterator = new ThemeIterator(_mockContext.Object);
        }

        #region GetThemes

        [Test]
        public async Task GetThemes_ReturnsAllOrOnlyActiveThemes()
        {
            // Arrange
            var activeTheme = new CourseTheme
            {
                CourseThemeId = 10,
                ThemeName = "Веб-разработка",
                IsActive = true
            };
            var inactiveTheme = new CourseTheme
            {
                CourseThemeId = 20,
                ThemeName = "Устаревшая тема",
                IsActive = false
            };

            _mockContext.Setup(x => x.Set<CourseTheme>())
                .ReturnsDbSet(new[] { activeTheme, inactiveTheme });

            // Act
            var allThemes = (await _iterator.GetThemes(isOnlyAvailable: false)).ToList();
            var onlyActive = (await _iterator.GetThemes(isOnlyAvailable: true)).ToList();

            // Assert
            Assert.That(allThemes.Count, Is.EqualTo(2));
            Assert.That(onlyActive.Count, Is.EqualTo(1));
            Assert.That(onlyActive[0].ThemeName, Is.EqualTo("Веб-разработка"));
            Assert.That(onlyActive[0].IsActive, Is.True);
        }

        #endregion

        #region GetTheme

        [Test]
        public async Task GetTheme_WhenExistsAndActive_ReturnsDto()
        {
            // Arrange
            var theme = new CourseTheme
            {
                CourseThemeId = 30,
                ThemeName = "Data Science",
                IsActive = true
            };

            _mockContext.Setup(x => x.Set<CourseTheme>())
                .ReturnsDbSet(new[] { theme });

            // Act
            var result = await _iterator.GetTheme(30);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.CourseThemeId, Is.EqualTo(30));
                Assert.That(result.ThemeName, Is.EqualTo("Data Science"));
                Assert.That(result.IsActive, Is.True);
            });
        }

        [Test]
        public void GetTheme_WhenThemeNotFound_ThrowsException()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<CourseTheme>())
                .ReturnsDbSet(new List<CourseTheme>());

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _iterator.GetTheme(999));
        }

        [Test]
        public void GetTheme_WhenThemeExistsButInactive_ThrowsException()
        {
            // Arrange
            var inactiveTheme = new CourseTheme
            {
                CourseThemeId = 40,
                ThemeName = "Неактивная тема",
                IsActive = false
            };
            _mockContext.Setup(x => x.Set<CourseTheme>())
                .ReturnsDbSet(new[] { inactiveTheme });

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _iterator.GetTheme(40));
        }

        #endregion
    }
}
