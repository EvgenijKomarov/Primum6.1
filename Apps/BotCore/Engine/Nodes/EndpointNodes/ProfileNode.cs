using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using Engine;
using Engine.Nodes;
using Resourses;

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
                    Text = $"{Emoticons.Student}Профиль ученика",
                    EndpointNode = typeof(StudentProfileNode),
                    Args = new List<string>()
                });
            }
            if(input.User?.IsApprovedTeacher != null && input.User?.IsApprovedTeacher == true)
            {
                buttons.Add(new EngineOutputButton
                {
                    Text = $"{Emoticons.Teacher}Профиль преподавателя",
                    EndpointNode = typeof(TeacherProfileNode),
                    Args = new List<string>()
                });
            }
            if (input.User?.IsAdmin != null && input.User?.IsAdmin == true)
            {
                buttons.Add(new EngineOutputButton
                {
                    Text = $"{Emoticons.Admin}Профиль админа",
                    EndpointNode = typeof(AdminProfileNode),
                    Args = new List<string>()
                });
            }

            return Finish(new EngineOutputMessage
            {
                Message = $"Привет, {Emoticons.User}{input.User?.DisplayName}!\n" +
                $"{Emoticons.Id}Id: {input.User?.Id}\n" +
                $"{Emoticons.Cash}Балланс: {input.User?.Cash} рублей\n",
                Buttons = buttons
            });
        }
    }
}
