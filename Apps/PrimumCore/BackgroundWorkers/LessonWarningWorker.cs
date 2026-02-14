using PrimumCore.BackgroundWorkers.Executors;

namespace PrimumCore.BackgroundWorkers
{
    public class LessonWarningWorker(LessonWarningExecutor executor, ILogger<LessonWarningWorker> logger) : PeriodWorker(
        "LessonWarner",
        executor.Execute(logger),
        TimeSpan.FromMinutes(1),
        logger
        );
}
