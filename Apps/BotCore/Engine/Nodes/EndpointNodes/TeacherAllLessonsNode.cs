using BotCore.Engine.Abstractions;
using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using CoreConnection.DTOs;
using Engine;
using Engine.Nodes;
using Resourses;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class TeacherAllLessonsNode(TeacherClient client) : ScrollableEndpointNode<LessonDto>("tchAllLessons")
    {
        public override async Task<string> ItemInfo(LessonDto item, DataBuffer buffer)
        {
            return $"{Emoticons.Lesson}Занятие за {item.DateTime.ToString("HH:mm dd.MM.yyyy")}\n" +
                $"{Emoticons.Course}Курс: {item.CourseName}\n" +
                $"{Emoticons.Teacher}Ученик: {item.StudentDisplayName}\n" +
                $"{Emoticons.Grade}Оценка: {(item.FinalGrade.HasValue ? item.FinalGrade.Value : "отсутствует")}\n" +
                $"{Emoticons.Cash}Стоимость: {item.Price}\n" +
                $"Статус: {LessonStatusRes.ResourceManager.GetString(item.LessonStatus.ToString()) ?? string.Empty}";
        }
        public override async Task Initialize(int index, DataBuffer input)
        {
            var res = await client.LastLessonsAsync(input.UserId!.Value, index, 1);
            TotalCount = res.TotalPages;
            Item = res.Items?.FirstOrDefault();
        }
        public override async Task<IEnumerable<EngineOutputButton>> ItemButtons(LessonDto item, DataBuffer buffer)
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
            return $"{Emoticons.Lesson}Занятий пока не существует";
        }
    }
}
