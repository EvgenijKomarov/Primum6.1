using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using Common.Utilities;
using CoreConnection;
using CoreConnection.DTOs;
using Engine;
using Engine.Nodes;
using Resourses;
using System.Text;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class StudentAbonementShedulesNode(
        StudentClient client,
        ConverterToDateTimeService datetimeService,
        UserClient userClient) : EndpointNode<DataBuffer, EngineOutputMessage>("stShedules")
    {
        public async override Task<INodeResult<DataBuffer, EngineOutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null)
        {
            var abonId = input.Arguments[0];
            var abon = await client.AbonementAsync(input.UserId!.Value, int.Parse(abonId));
            var shedules = (await client.AbonementShedulesAsync(input.UserId!.Value, int.Parse(abonId))).Items ?? new List<AbonementSheduleDto>();

            var user = await userClient.ProfileAsync(abon.StudentId);

            List<EngineOutputButton> buttons = new List<EngineOutputButton>();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Расписание абонемента по курсу {abon.CourseName} с преподавателем {abon.TeacherDisplayName}:\n");
            foreach ( var shedule in shedules)
            {
                var schedule = datetimeService.ApplyTimeZoneOffset(shedule.DayOfWeek, shedule.Time, user.TimezoneOffset);

                buttons.Add(new EngineOutputButton
                {
                    Text = $"{Emoticons.Shedule}{datetimeService.GetRusTranslation(schedule.Day)} {schedule.Hour}:00\n",
                    EndpointNode = typeof(StudentAbonementSheduleNode),
                    Args = new List<string> { abonId, shedule.Id.ToString() }
                });
            }

            buttons.Add(new EngineOutputButton
            {
                Text = $"{Emoticons.Back}Назад",
                EndpointNode = typeof(StudentAbonementNode),
                Args = new List<string> { abonId }
            });
            return Finish(new EngineOutputMessage
            {
                Message = sb.ToString(),
                Buttons = buttons
            });
        }
    }
}
