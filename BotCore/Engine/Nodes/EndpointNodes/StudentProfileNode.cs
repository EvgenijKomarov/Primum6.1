using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using Engine;
using Engine.Nodes;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class StudentProfileNode(StudentClient client) : EndpointNode<DataBuffer, EngineOutputMessage>("stProf")
    {
        public async override Task<INodeResult<DataBuffer, EngineOutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null)
        {
            var studentProfile = await client.ProfileAsync(input.UserId!.Value);
            return Finish(new EngineOutputMessage
            {
                Message = $"Профиль ученика" +
                $"Пользователь: {input.User?.DisplayName}!\n" +
                $"Id: {input.User?.Id}\n" +
                $"Балланс: {input.User?.Cash} рублей\n" +
                $"Монеты: {studentProfile.Coins}",
            });
        }
    }
}
