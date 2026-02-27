using BotCore.Engine.Abstractions;
using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using CoreConnection.DTOs;
using Resourses;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class StudentExploreCoursesByThemeNode(PublicClient publicClient) : ScrollableEndpointNode<CourseDto>("stExplrCoursByTh")
    {
        public override async Task<string> ItemInfo(CourseDto item, DataBuffer buffer)
        {
            return $"{Emoticons.Course}Курс:{item.Name}\n" +
                $"{Emoticons.Cash}Стоимость урока:{item.Price}\n" +
                $"{Emoticons.Lesson}Пробные уроки:{item.FreeLessons}\n" +
                $"{Emoticons.Lesson}Максимум уроков:{item.MaxLessons}\n" +
                $"\n" +
                $"{Emoticons.Teacher}Преподаватель: {item.TeacherName}\n" +
                $"О преподавателе: {item.TeacherAbout}";
        }
        public override async Task<IEnumerable<CourseDto>> GetEnumerable(DataBuffer input)
        {
            var themeId = input.Arguments[1];
            return await publicClient.CoursesByThemeAsync(int.Parse(themeId));
        }
        public override async Task<IEnumerable<EngineOutputButton>> ItemButtons(CourseDto item, DataBuffer buffer)
        {
            return new List<EngineOutputButton>()
            {
                new EngineOutputButton
                {
                    Text=$"{Emoticons.Teacher}Расписание преподавателя",
                    EndpointNode = typeof(StudentExploreTeacherShedulesNode),
                    Args = new List<string>{ item.CourseId.ToString(), item.TeacherId.ToString(), item.CourseThemeId.ToString()  }
                }
            };
        }
        public override async Task<EngineOutputButton> BackButton(DataBuffer input)
        {
            return new EngineOutputButton
            {
                Text = $"{Emoticons.Back}Назад",
                EndpointNode = typeof(StudentExploreThemesNode)
            };
        }
        public override async Task<string> IfItemsEmptyText(DataBuffer input)
        {
            return $"{Emoticons.Course}Курсов по теме пока не существует";
        }
    }
}
