using PrimumCore.BackgroundWorkers.Executors;

namespace PrimumCore.BackgroundWorkers
{
    public class LessonIteratorWorker(LessonIteratorExecutor executor, ILogger<LessonIteratorWorker> logger): PeriodWorker(
        "LessonIterator",
        executor.Execute(logger),
        TimeSpan.FromMinutes(1),
        logger
        );
}
