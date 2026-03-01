using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using CoreConnection.DTOs.Inputs;
using CoreDBModel.Models.Enums;
using Engine;
using Engine.Nodes;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class AdminSolveIncidentNode(AdminClient client) : EndpointNode<DataBuffer, EngineOutputMessage>("admSolveInc")
    {
        public async override Task<INodeResult<DataBuffer, EngineOutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null)
        {
            var objId = input.Arguments[0];
            var meaning = input.Arguments[1];
            var decision = input.Arguments[2];
            var decisionExplanation = "Unavailable to collect. Solved by chatbot";

            await client.SolveIncidentAsync(input.UserId!.Value, new IncidentDecisionInputDto
            {
                ObjectId = int.Parse(objId),
                Meaning = Enum.Parse<IncidentMeaning>(meaning),
                Decision = Enum.Parse<IncidentDecision>(decision),
                DecisionExplanation = decisionExplanation
            });
            input.Arguments.Clear();
            return Next<AdminIncidentsNode>(input);
        }
    }
}
