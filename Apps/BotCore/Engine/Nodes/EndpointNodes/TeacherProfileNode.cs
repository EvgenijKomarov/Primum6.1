using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using Engine;
using Engine.Nodes;
using Resourses;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class TeacherProfileNode(TeacherClient client) : EndpointNode<DataBuffer, EngineOutputMessage>("tchProf")
    {
        public async override Task<INodeResult<DataBuffer, EngineOutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null) 
        {
            var teascherProfile = await client.ProfileAsync(input.UserId!.Value);
            return Finish(new EngineOutputMessage
            {
                Message = $"{Emoticons.Teacher}Профиль преподавателя\n" +
                $"{Emoticons.User}Пользователь: {input.User?.DisplayName}!\n" +
                $"{Emoticons.Id}Id: {input.User?.Id}\n" +
                $"{Emoticons.Cash}Балланс: {input.User?.Cash} рублей\n",
                Buttons = new EngineOutputButton[]
                {
                    new EngineOutputButton
                    {
                        Text = $"{Emoticons.Lesson}Будущие занятия",
                        EndpointNode = typeof(TeacherFutureLessonsNode)
                    },
                    new EngineOutputButton
                    {
                        Text = $"{Emoticons.Lesson}Все занятия",
                        EndpointNode = typeof(TeacherAllLessonsNode)
                    },
                    new EngineOutputButton
                    {
                        Text = $"{Emoticons.Course}Курсы",
                        EndpointNode = typeof(TeacherCoursesNode)
                    },
                    new EngineOutputButton
                    {
                        Text = $"{Emoticons.Abonement}Подписанты",
                        EndpointNode = typeof(TeacherAbonementsNode)
                    },
                    new EngineOutputButton
                    {
                        Text = $"{Emoticons.Shedule}Расписания",
                        EndpointNode = typeof(TeacherShedulesNode)
                    },
                    new EngineOutputButton
                    {
                        Text = $"{Emoticons.Back}Назад",
                        EndpointNode = typeof(ProfileNode)
                    }
                }
            });
        }
    }
}
