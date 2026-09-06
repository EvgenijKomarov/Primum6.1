using BotCore.Engine.Abstractions;
using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using CoreConnection.DTOs;
using Engine.Nodes;
using Resourses;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class TeacherAbonementsNode(TeacherClient client) : ScrollableEndpointNode<AbonementDto>("tchAbons")
    {
        public override async Task<string> ItemInfo(AbonementDto item, DataBuffer buffer)
        {
            return $"{Emoticons.Course}Курс: {item.CourseName} ({item.CourseThemeName})\n" +
                $"{Emoticons.Student}Ученик: {item.StudentDisplayName}\n" +
                $"{Emoticons.Cash}Цена за урок:{item.PricePerLesson}\n" +
                $"Статус: {AbonementStatusRes.ResourceManager.GetString(item.AbonementStatus.ToString()) ?? string.Empty}";
        }
        public override async Task Initialize(int index, DataBuffer input)
        {
            var res = await client.AbonementsAsync(input.UserId!.Value, index, 1);
            TotalCount = res.TotalPages;
            Item = res.Items?.FirstOrDefault();
        }
        public override async Task<IEnumerable<EngineOutputButton>> ItemButtons(AbonementDto item, DataBuffer buffer)
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
            return $"{Emoticons.Abonement}Подписантов пока не существует";
        }
    }
}
