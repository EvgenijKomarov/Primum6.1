using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using CoreConnection.DTOs;
using Engine;
using Engine.Nodes;
using Resourses;
using System.Text;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class StudentAbonementShedulesNode(StudentClient client) : EndpointNode<DataBuffer, EngineOutputMessage>("stShedules")
    {
        public async override Task<INodeResult<DataBuffer, EngineOutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null)
        {
            var abonId = input.Arguments[0];
            var abon = await client.AbonementAsync(input.UserId!.Value, int.Parse(abonId));
            var shedules = (await client.AbonementShedulesAsync(input.UserId!.Value, int.Parse(abonId))).Items ?? new List<StudentSheduleDto>();

            List<EngineOutputButton> buttons = new List<EngineOutputButton>();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Расписание абонемента по курсу {abon.CourseName} с преподавателем {abon.TeacherDisplayName}:\n");
            foreach ( var shedule in shedules)
            {
                buttons.Add(new EngineOutputButton
                {
                    Text = $"{Emoticons.Shedule}{DayOfWeekRes.ResourceManager.GetString(shedule.DayOfWeek.ToString())} {shedule.Time}:00\n",
                    EndpointNode = typeof(StudentAbonementSheduleNode),
                    Args = new List<string> { abonId, shedule.AbonementSheduleId.ToString() }
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
