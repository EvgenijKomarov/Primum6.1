using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using Engine;
using Engine.Nodes;
using Resourses;

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
                        Text = $"{Emoticons.Lesson}Занятия на ближайшее время",
                        EndpointNode = typeof(StudentFutureLessonsNode)
                    },
                    new EngineOutputButton
                    {
                        Text = $"{Emoticons.Lesson}Все занятия",
                        EndpointNode = typeof(StudentAllLessonsNode)
                    },
                    new EngineOutputButton
                    {
                        Text = $"{Emoticons.Abonement}Мои абонементы",
                        EndpointNode = typeof(StudentAbonementsNode)
                    },
                    new EngineOutputButton
                    {
                        Text = $"{Emoticons.Theme}Доступные курсы по темам",
                        EndpointNode = typeof(StudentExploreThemesNode)
                    },
                    new EngineOutputButton
                    {
                        Text = $"{Emoticons.Promocode}Мои промокоды",
                        EndpointNode = typeof(StudentBoughtPromocodesNode)
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
