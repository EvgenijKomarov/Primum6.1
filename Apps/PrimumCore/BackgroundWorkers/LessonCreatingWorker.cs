using PrimumCore.BackgroundWorkers.Executors;

namespace PrimumCore.BackgroundWorkers
{
    public class LessonCreatingWorker(LessonCreatingExecutor executor, ILogger<LessonCreatingWorker> logger) : PeriodWorker(
        "LessonCreating",
        executor.Execute(logger),
        TimeSpan.FromMinutes(1),
        logger
        );
}
