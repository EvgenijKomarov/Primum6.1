using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using Engine;
using Engine.Nodes;
using Resourses;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class AdminProfileNode(AdminClient client) : EndpointNode<DataBuffer, EngineOutputMessage>("admProf")
    {
        public async override Task<INodeResult<DataBuffer, EngineOutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null)
        {
            var adminProfile = await client.ProfileAsync(input.UserId!.Value);
            return Finish(new EngineOutputMessage
            {
                Message = $"{Emoticons.Admin}Профиль админа\n" +
                $"{Emoticons.Admin}Админ: {adminProfile.DisplayName}\n" +
                $"{Emoticons.Id}Id: {input.User?.Id}\n" +
                $"Статус: {adminProfile.Status}",
                Buttons = new EngineOutputButton[]
                {
                    new EngineOutputButton
                    {
                        Text = $"{Emoticons.Incident}Решение инцидентов",
                        EndpointNode = typeof(AdminIncidentsNode)
                    },
                    new EngineOutputButton
                    {
                        Text = $"{Emoticons.Back}Назад",
                        EndpointNode = typeof(ProfileNode)
                    },
                }
            });
        }
    }
}
