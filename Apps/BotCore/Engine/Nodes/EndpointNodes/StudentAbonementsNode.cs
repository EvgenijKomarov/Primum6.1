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
    public class StudentAbonementsNode(StudentClient client) : EndpointNode<DataBuffer, EngineOutputMessage>("stAbons")
    {
        public async override Task<INodeResult<DataBuffer, EngineOutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null) 
        {
            var abonements = (await client.AbonementsAsync(input.UserId!.Value)).Items ?? new List<AbonementDto>();
            List<EngineOutputButton> buttons = new List<EngineOutputButton>();
            StringBuilder sb = new StringBuilder();
            foreach (var abonement in abonements)
            {
                sb.AppendLine($"{Emoticons.Abonement}{abonement.CourseName} ({abonement.CourseThemeName}) - " +
                    $"{AbonementStatusRes.ResourceManager.GetString(abonement.AbonementStatus.ToString()) ?? string.Empty}\n");
                buttons.Add(new EngineOutputButton
                {
                    Text = $"{Emoticons.Abonement}{abonement.CourseName}",
                    EndpointNode = typeof(StudentAbonementNode),
                    Args = new List<string> { abonement.AbonementId.ToString() }
                });
            }
            buttons.Add(new EngineOutputButton
            {
                Text = $"{Emoticons.Back}Назад",
                EndpointNode = typeof(StudentProfileNode)
            });
            return Finish(new EngineOutputMessage
            {
                Message = abonements.Count() == 0 ? "Абонементов нет" : sb.ToString(),
                Buttons = buttons
            });
        }
    }
}
