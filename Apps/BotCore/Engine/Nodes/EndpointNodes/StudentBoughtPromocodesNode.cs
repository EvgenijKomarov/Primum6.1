using BotCore.Engine.Abstractions;
using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using CoreConnection.DTOs;
using Resourses;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class StudentBoughtPromocodesNode(StudentClient client) : ScrollableEndpointNode<PromocodeDto>("stPromocodes")
    {
        public override async Task<string> ItemInfo(PromocodeDto item, DataBuffer buffer)
        {
            return $"{Emoticons.Promocode}Промокод {item.Title}\n" +
                $"Описание:{item.Description}\n\n" +
                $"Код: {item.Code}";
        }
        public override async Task<IEnumerable<PromocodeDto>> GetEnumerable(DataBuffer input)
        {
            return await client.PromocodesAsync(input.UserId!.Value);
        }
        public override async Task<IEnumerable<EngineOutputButton>> ItemButtons(PromocodeDto item, DataBuffer buffer)
        {
            return new List<EngineOutputButton>();
        }
        public override async Task<EngineOutputButton> BackButton(DataBuffer input)
        {
            return new EngineOutputButton
            {
                Text = $"{Emoticons.Back}Назад",
                EndpointNode = typeof(StudentProfileNode)
            };
        }
        public override async Task<string> IfItemsEmptyText(DataBuffer input)
        {
            return $"{Emoticons.Lesson}У вас пока нет купленных промокодов";
        }
    }
}
