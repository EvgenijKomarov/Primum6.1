using BotCore.Engine.Abstractions;
using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using CoreConnection.DTOs;
using CoreDBModel.Models;
using Engine.Nodes;
using Resourses;
using System.Timers;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class StudentAllLessonsNode(StudentClient client) : ScrollableEndpointNode<LessonDto>("stAllLessons")
    {
        public override async Task<string> ItemInfo(LessonDto item, DataBuffer buffer)
        {
            return $"{Emoticons.Lesson}Занятие за {item.DateTime.ToString("HH:mm dd.MM.yyyy")}\n" +
                $"{Emoticons.Course}Курс: {item.CourseName}\n" +
                $"{Emoticons.Teacher}Преподаватель: {item.TeacherDisplayName}\n" +
                $"{Emoticons.Grade}Оценка: {(item.FinalGrade.HasValue ? item.FinalGrade.Value : "отсутствует")}\n" +
                $"{Emoticons.Cash}Стоимость: {item.Price}\n" +
                $"Статус: {LessonStatusRes.ResourceManager.GetString(item.LessonStatus.ToString()) ?? string.Empty}";
        }
        public override async Task Initialize(int index, DataBuffer input)
        {
            var res = (await client.LessonsAsync(input.UserId!.Value, index, 1));
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
                EndpointNode = typeof(StudentProfileNode)
            };
        }
        public override async Task<string> IfItemsEmptyText(DataBuffer input)
        {
            return $"{Emoticons.Lesson}У вас пока нет занятий";
        }
    }
}
