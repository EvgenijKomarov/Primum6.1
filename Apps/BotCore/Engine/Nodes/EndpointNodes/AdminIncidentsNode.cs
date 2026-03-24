using BotCore.Engine.Abstractions;
using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using CoreConnection.DTOs;
using Resourses;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class AdminIncidentsNode(AdminClient client) : ScrollableEndpointNode<IncidentDto>("admIncidents")
    {
        public override async Task<string> ItemInfo(IncidentDto item, DataBuffer buffer)
        {
            return $"{Emoticons.Spark}Субъект:{IncidentMeaningRes.ResourceManager.GetString(item.Meaning.ToString()) ?? string.Empty}\n" +
                $"{Emoticons.Id}Id субъекта: {item.ObjectId}\n" +
                $"{Emoticons.Status}Статус:{IncidentStatusRes.ResourceManager.GetString(item.Status.ToString()) ?? string.Empty}\n" +
                $"\n" +
                $"{Emoticons.Info}Контекст: \n" +
                $"{item.CommonInfo}";
        }
        public override async Task<(IncidentDto?, int)> GetItemAndTotalCount(int index, DataBuffer input)
        {
            var res = await client.IncidentsAsync(input.UserId!.Value, index, 1);
            return (res.Items?.FirstOrDefault(), res.TotalPages);
        }
        public override async Task<IEnumerable<EngineOutputButton>> ItemButtons(IncidentDto item, DataBuffer buffer)
        {   
            var buttons = new List<EngineOutputButton>();
            foreach(var decision in item.Decisions)
            {
                buttons.Add(new EngineOutputButton
                {
                    Text = IncidentDecisionRes.ResourceManager.GetString(decision.ToString()) ?? "Unknown",
                    EndpointNode = typeof(AdminSolveIncidentNode),
                    Args = new List<string> { 
                        item.ObjectId.ToString(),
                        ((int)item.Meaning).ToString(),
                        ((int)decision).ToString()
                    }
                });
            }
            return buttons;
        }
        public override async Task<EngineOutputButton> BackButton(DataBuffer input)
        {
            return new EngineOutputButton
            {
                Text = $"{Emoticons.Back}Назад",
                EndpointNode = typeof(AdminProfileNode)
            };
        }
        public override async Task<string> IfItemsEmptyText(DataBuffer input)
        {
            return $"{Emoticons.Incident}Инцидентов нет";
        }
    }
}
