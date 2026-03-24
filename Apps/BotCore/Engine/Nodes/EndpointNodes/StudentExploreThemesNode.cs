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
    public class StudentExploreThemesNode(PublicClient publicClient) : ScrollableEndpointNode<IEnumerable<CourseThemeDto>>("stExplrThemes")
    {
        public override async Task<string> ItemInfo(IEnumerable<CourseThemeDto> item, DataBuffer buffer)
        {
            return $"{Emoticons.Theme}Доступные темы курсов:";
        }

        public override async Task Initialize(int index, DataBuffer input)
        {
            var res = (await publicClient.ThemesAsync(index, 10));
            TotalCount = res.TotalPages;
            var items = res.Items;
            Item = items is null || !items.Any() ? null : items;
        }

        public override async Task<string> IfItemsEmptyText(DataBuffer input)
        {
            return $"{Emoticons.Theme}Тем пока не существует";
        }

        public override async Task<EngineOutputButton> BackButton(DataBuffer input)
        {
            return new EngineOutputButton
            {
                Text = $"{Emoticons.Back}Назад",
                EndpointNode = typeof(StudentProfileNode)
            };
        }

        public override async Task<IEnumerable<EngineOutputButton>> ItemButtons(IEnumerable<CourseThemeDto> items, DataBuffer buffer)
        {
            var buttons = new List<EngineOutputButton>();
            foreach (var theme in items)
            {
                buttons.Add(new EngineOutputButton()
                {
                    Text = Emoticons.Theme + theme.ThemeName,
                    EndpointNode = typeof(StudentExploreCoursesByThemeNode),
                    Args = new List<string> { theme.Id.ToString() }
                });
            }
            return buttons;
        }
    }
}
