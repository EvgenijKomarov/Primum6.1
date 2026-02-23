using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using BotCore.Resourses;
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
                Message = $"{Emoticons.Student}Профиль ученика\n" +
                $"{Emoticons.User}Пользователь: {input.User?.DisplayName}!\n" +
                $"{Emoticons.Id}Id: {input.User?.Id}\n" +
                $"{Emoticons.Cash}Балланс: {input.User?.Cash} рублей\n" +
                $"{Emoticons.Coins}Монеты: {studentProfile.Coins}",
                Buttons = new EngineOutputButton[]
                {
                    new EngineOutputButton
                    {
                        Text = $"{Emoticons.Lesson}Мои занятия",
                        EndpointNode = typeof(StudentLessonsNode)
                    }
                }
            });
        }
    }
}
