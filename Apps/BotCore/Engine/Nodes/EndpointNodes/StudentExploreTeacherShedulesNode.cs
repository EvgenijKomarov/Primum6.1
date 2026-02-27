using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using Engine;
using Engine.Nodes;
using Resourses;
using System.Text;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class StudentExploreTeacherShedulesNode(PublicClient publicClient, StudentClient studentClient) : EndpointNode<DataBuffer, EngineOutputMessage>("stExplrShedules")
    {
        public async override Task<INodeResult<DataBuffer, EngineOutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null)
        {
            var courseId = input.Arguments[0];
            var teacherId = input.Arguments[1];
            var themeId = input.Arguments[2];

            var shedules = await publicClient.TeacherShedulesAsync(int.Parse(teacherId));

            var backButton = new EngineOutputButton
            {
                Text = $"{Emoticons.Back}Назад",
                EndpointNode = typeof(StudentExploreCoursesByThemeNode),
                Args = new List<string> { "0", themeId }
            };

            var buttons = new List<EngineOutputButton>();
            foreach (var shedule in shedules)
            {
                buttons.Add(new EngineOutputButton
                {
                    Text = $"{Emoticons.Shedule}{DayOfWeekRes.ResourceManager.GetString(shedule.DayOfWeek.ToString())} {shedule.Time}:00",
                    EndpointNode = typeof(StudentSubscribeCourseNode),
                    Args = new List<string> { courseId, teacherId, themeId, shedule.TeacherSheduleId.ToString() }
                });
            }
            buttons.Add(backButton);

            return Finish(new EngineOutputMessage
            {
                Message = $"{Emoticons.Shedule}Доступные расписания преподавателя:",
                Buttons = buttons
            });
        }
    }
}
