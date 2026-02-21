using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using Engine;
using Engine.Nodes;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class ProfileNode(): EndpointNode<DataBuffer, OutputMessage>("start")
    {
        public async override Task<INodeResult<DataBuffer, OutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null)
        {
            List<Button> buttons = new List<Button>();
            if(input.User?.IsApprovedStudent != null)
            {
                buttons.Add(new Button
                {
                    Text = "Профиль ученика",
                    EndpointNode = typeof(StudentProfileNode),
                    Args = new List<string>()
                });
            }
            if(input.User?.IsApprovedTeacher != null && input.User?.IsApprovedTeacher == true)
            {
                buttons.Add(new Button
                {
                    Text = "Профиль преподавателя",
                    EndpointNode = typeof(TeacherProfileNode),
                    Args = new List<string>()
                });
            }

            return Finish(new OutputMessage
            {
                Message = $"Привет, {input.User?.DisplayName}!\n" +
                $"Id: {input.User?.Id}\n" +
                $"Балланс: {input.User?.Cash} рублей\n",
                Buttons = buttons
            });
        }
    }
}
