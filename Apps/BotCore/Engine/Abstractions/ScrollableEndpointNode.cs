using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using BotCore.Engine.Nodes.EndpointNodes;
using CoreDBModel.Models;
using Engine;
using Engine.Nodes;
using Resourses;

namespace BotCore.Engine.Abstractions
{
    public abstract class ScrollableEndpointNode<TItem>(string endpointId) : EndpointNode<DataBuffer, EngineOutputMessage>(endpointId)
    {
        protected int? TotalCount { get; set; }
        protected TItem? Item { get; set; }
        public abstract Task<string> ItemInfo(TItem item, DataBuffer buffer);
        /// <summary>
        /// Use this to initialize item and total count
        /// </summary>
        /// <param name="index"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public abstract Task Initialize(int index, DataBuffer buffer);
        public abstract Task<IEnumerable<EngineOutputButton>> ItemButtons(TItem item, DataBuffer buffer);
        public abstract Task<EngineOutputButton> BackButton(DataBuffer buffer);
        public abstract Task<string> IfItemsEmptyText(DataBuffer buffer);

        public async sealed override Task<INodeResult<DataBuffer, EngineOutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null)
        {
            var index = input.PageIndex ?? 0;

            await Initialize(index, input);
            if (TotalCount is null) { throw new ArgumentNullException("TotalCount not initialized"); }
            if (Item is null)
            {
                return Finish(new EngineOutputMessage
                {
                    Message = await IfItemsEmptyText(input),
                    Buttons = new List<EngineOutputButton> { await BackButton(input) }
                });
            }

            var buttons = new List<EngineOutputButton>();
            if (index != 0)
            {
                buttons.Add(new EngineOutputButton
                {
                    Text = Emoticons.Up,
                    EndpointNode = GetType(),
                    Args = input.Arguments,
                    PageIndex = index-1
                });
            }
            buttons.AddRange(await ItemButtons(Item, input));
            if (index != TotalCount - 1)
            {
                buttons.Add(new EngineOutputButton
                {
                    Text = Emoticons.Down,
                    EndpointNode = GetType(),
                    Args = input.Arguments,
                    PageIndex = index+1
                });
            }
            buttons.Add(await BackButton(input));

            return Finish(new EngineOutputMessage
            {
                Message = await ItemInfo(Item, input),
                Buttons = buttons
            });
        }
    }
}
