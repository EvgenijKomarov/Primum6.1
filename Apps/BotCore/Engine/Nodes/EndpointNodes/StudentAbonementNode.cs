using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
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

            return Finish(new EngineOutputMessage
            {
                Message = $"{Emoticons.Abonement}Абонемент по курсу {abon.CourseName} ({abon.CourseThemeName})\n" +
                $"{Emoticons.Teacher}Преподаватель: {abon.TeacherDisplayName}\n" +
                $"{Emoticons.Cash}Цена за урок: {abon.PricePerLesson}\n" +
                $"Статус: {AbonementStatusRes.ResourceManager.GetString(abon.AbonementStatus.ToString()) ?? string.Empty}",
                Buttons = new EngineOutputButton[]
                {
                    new EngineOutputButton
                    {
                        Text = $"{Emoticons.Back}Назад",
                        EndpointNode = typeof(StudentAbonementsNode)
                    }
                }
            });
        }
    }
}
