using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using CoreDBModel.Models.Enums;
using Engine;
using Engine.Nodes;
using Resourses;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class StudentAbonementNode(StudentClient client) : EndpointNode<DataBuffer, EngineOutputMessage>("stAbon")
    {
        public async override Task<INodeResult<DataBuffer, EngineOutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null)
        {
            var abonId = input.Arguments[0];
            var abon = await client.AbonementAsync(input.UserId!.Value, int.Parse(abonId));

            var buttons = new List<EngineOutputButton>();
            buttons.Add(new EngineOutputButton
            {
                Text = $"{Emoticons.Shedule}Расписания",
                EndpointNode = typeof(StudentAbonementShedulesNode),
                Args = new List<string> { abonId }
            });
            if (abon.AbonementStatus == AbonementStatus.Active || abon.AbonementStatus == AbonementStatus.Freezed)
            {
                buttons.Add(new EngineOutputButton
                {
                    Text = abon.AbonementStatus == AbonementStatus.Active ? $"{Emoticons.Ice}Заморозить" : $"{Emoticons.Fire}Активировать",
                    EndpointNode = typeof(StudentAbonementActivateDeactivateNode),
                    Args = new List<string> { abonId }
                });
            }
            if (abon.AbonementStatus != AbonementStatus.Deleted)
            {
                buttons.Add(new EngineOutputButton
                {
                    Text = $"{Emoticons.Trash}Удалить",
                    EndpointNode = typeof(StudentAbonementDeleteNode),
                    Args = new List<string> { abonId }
                });
            }
            buttons.Add(new EngineOutputButton
            {
                Text = $"{Emoticons.Back}Назад",
                EndpointNode = typeof(StudentAbonementsNode)
            });

            return Finish(new EngineOutputMessage
            {
                Message = $"{Emoticons.Abonement}Абонемент по курсу {abon.CourseName} ({abon.CourseThemeName})\n" +
                $"{Emoticons.Teacher}Преподаватель: {abon.TeacherDisplayName}\n" +
                $"{Emoticons.Cash}Цена за урок: {abon.PricePerLesson}\n" +
                $"Статус: {AbonementStatusRes.ResourceManager.GetString(abon.AbonementStatus.ToString()) ?? string.Empty}",

                Buttons = buttons
            });
        }
    }
}
