using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using Engine;
using Engine.Nodes;
using Resourses;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class StudentAbonementSheduleNode(StudentClient client) : EndpointNode<DataBuffer, EngineOutputMessage>("stShedule")
    {
        public async override Task<INodeResult<DataBuffer, EngineOutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null)
        {
            var abonId = input.Arguments[0];
            var sheduleId = input.Arguments[1];

            var abon = await client.AbonementAsync(input.UserId!.Value, int.Parse(abonId));
            var shedule = await client.SheduleGetAsync(input.UserId!.Value, int.Parse(sheduleId));

            return Finish(new EngineOutputMessage
            {
                Message = $"{Emoticons.Shedule}Расписание:\n" +
                $"{Emoticons.Course}Курс: {abon.CourseName}\n" +
                $"{Emoticons.Teacher}Преподаватель: {abon.TeacherDisplayName}\n" +
                $"День недели: {DayOfWeekRes.ResourceManager.GetString(shedule.DayOfWeek.ToString())}\n" +
                $"Время: {shedule.Time}:00",
                Buttons = new EngineOutputButton[]
                {
                    new EngineOutputButton
                    {
                        Text = $"{Emoticons.Trash}Удалить",
                        EndpointNode = typeof(StudentSheduleDeleteNode),
                        Args = new List<string> { abonId, sheduleId }
                    },
                    new EngineOutputButton
                    {
                        Text = $"{Emoticons.Back}Назад",
                        EndpointNode = typeof(StudentAbonementShedulesNode),
                        Args = new List<string> { abonId }
                    }
                }
            });
        }
    }
}
