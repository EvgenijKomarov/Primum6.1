using BotCore.Engine.Abstractions;
using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using CoreConnection.DTOs;
using Resourses;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class TeacherCoursesNode(TeacherClient client) : ScrollableEndpointNode<CourseDto>("tchCourses")
    {
        public override async Task<string> ItemInfo(CourseDto item, DataBuffer buffer)
        {
            return $"{Emoticons.Course}Курс: {item.Name}\n" +
                $"{Emoticons.Cash}Цена за урок: {item.Price}\n" +
                $"{Emoticons.Lesson}Максимум занятий: {item.MaxLessons}\n" +
                $"{Emoticons.Lesson}бесплатные занятия: {item.FreeLessons}\n" +
                $"Описание: {item.About}";
        }
        public override async Task<IEnumerable<CourseDto>> GetEnumerable(DataBuffer input)
        {
            return await client.CoursesAsync(input.UserId!.Value);
        }
        public override async Task<IEnumerable<EngineOutputButton>> ItemButtons(CourseDto item, DataBuffer buffer)
        {
            return new List<EngineOutputButton>();
        }
        public override async Task<EngineOutputButton> BackButton(DataBuffer input)
        {
            return new EngineOutputButton
            {
                Text = $"{Emoticons.Back}Назад",
                EndpointNode = typeof(TeacherProfileNode)
            };
        }
        public override async Task<string> IfItemsEmptyText(DataBuffer input)
        {
            return $"{Emoticons.Course}Курсов пока не существует";
        }
    }
}
