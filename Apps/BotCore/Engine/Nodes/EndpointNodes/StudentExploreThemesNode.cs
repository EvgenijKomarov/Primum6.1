using BotCore.Engine.Abstractions;
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
    public class StudentExploreThemesNode(PublicClient publicClient) : EndpointNode<DataBuffer, EngineOutputMessage>("stExplrThemes")
    {
        public async override Task<INodeResult<DataBuffer, EngineOutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null)
        {
            var themes = (await publicClient.ThemesAsync(0, 20)).Items ?? new List<CourseThemeDto>();

            var backButton = new EngineOutputButton
            {
                Text = $"{Emoticons.Back}Назад",
                EndpointNode = typeof(StudentProfileNode)
            };
            if (themes.Count == 0)
            {
                return Finish(new EngineOutputMessage
                {
                    Message = $"{Emoticons.Theme}Нет доступных тем на площадке!",
                    Buttons = new List<EngineOutputButton> { backButton }
                });
            }
            StringBuilder sb = new StringBuilder();
            var buttons = new List<EngineOutputButton>();
            foreach (var theme in themes) {
                buttons.Add(new EngineOutputButton()
                {
                    Text = Emoticons.Theme + theme.ThemeName,
                    EndpointNode = typeof(StudentExploreCoursesByThemeNode),
                    Args = new List<string> { "0", theme.Id.ToString() }
                });
            }
            buttons.Add(backButton);

            return Finish(new EngineOutputMessage
            {
                Message = "Доступные темы курсов:",
                Buttons = buttons
            });
        }
    }
}
