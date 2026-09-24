using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using Common.Utilities;
using CoreDBModel.Models;
using Engine;
using Engine.Nodes;
using PaymentServiceConnection;
using Resourses;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class TeacherSchedulesPickWeekNode(ConverterToDateTimeService datetimeService) : EndpointNode<DataBuffer, EngineOutputMessage>("tchPickWeekShedules")
    {
        public async override Task<INodeResult<DataBuffer, EngineOutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null)
        {
            List<EngineOutputButton> buttons = datetimeService
                .GetRusDaysOfWeek()
                .Select(x => new EngineOutputButton
                {
                    Text = $"{Emoticons.Shedule}{x.Item2}",
                    EndpointNode=typeof(TeacherShedulesNode),
                    Args = [((int)x.Item1).ToString()]
                })
                .ToList();

            buttons.Add(new EngineOutputButton
                    {
                        Text = $"{Emoticons.Back}Назад",
                        EndpointNode = typeof(TeacherProfileNode)
                    });

            return Finish(new EngineOutputMessage
            {
                Message = $"Выберите день недели",
                Buttons = buttons
            });
        }
    }
}
