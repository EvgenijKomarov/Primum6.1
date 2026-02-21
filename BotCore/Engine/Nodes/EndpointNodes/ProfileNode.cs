using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using Engine;
using Engine.Nodes;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class ProfileNode(): EndpointNode<DataBuffer, EngineOutputMessage>("start")
    {
        public async override Task<INodeResult<DataBuffer, EngineOutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null)
        {
            List<EngineOutputButton> buttons = new List<EngineOutputButton>();
            if(input.User?.IsApprovedStudent != null)
            {
                buttons.Add(new EngineOutputButton
                {
                    Text = "Профиль ученика",
                    EndpointNode = typeof(StudentProfileNode),
                    Args = new List<string>()
                });
            }
            if(input.User?.IsApprovedTeacher != null && input.User?.IsApprovedTeacher == true)
            {
                buttons.Add(new EngineOutputButton
                {
                    Text = "Профиль преподавателя",
                    EndpointNode = typeof(TeacherProfileNode),
                    Args = new List<string>()
                });
            }

            return Finish(new EngineOutputMessage
            {
                Message = $"Привет, {input.User?.DisplayName}!\n" +
                $"Id: {input.User?.Id}\n" +
                $"Балланс: {input.User?.Cash} рублей\n",
                Buttons = buttons
            });
        }
    }
}
