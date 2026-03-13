using BotCore.Engine.Abstractions;
using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using CoreConnection.DTOs;
using Resourses;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class TeacherShedulesNode(TeacherClient client) : ScrollableEndpointNode<TeacherSheduleDto>("tchShedules")
    {
        public override async Task<string> ItemInfo(TeacherSheduleDto item, DataBuffer buffer)
        {
            return $"{Emoticons.Shedule}Расписание: {DayOfWeekRes.ResourceManager.GetString(item.DayOfWeek.ToString()) ?? string.Empty} {item.Time}:00\n" +
                $"{Emoticons.Student}Ученик: {(item.IsAvailable ? item.StudentName : "отсутствует")}";
        }
        public override async Task<(TeacherSheduleDto?, int)> GetItemAndTotalCount(int index, DataBuffer input)
        {
            var res = await client.ShedulesAsync(input.UserId!.Value, index, 1);
            return (res.Items?.FirstOrDefault(), res.TotalPages);
        }
        public override async Task<IEnumerable<EngineOutputButton>> ItemButtons(TeacherSheduleDto item, DataBuffer buffer)
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
            return $"{Emoticons.Shedule}Расписаний пока не существует";
        }
    }
}
