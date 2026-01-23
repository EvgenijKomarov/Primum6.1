using DataNotifications;
using PrimumCore.Services.Connectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimumCore.Services.Utilities
{
    public class CoreNotificationService(IPublisher publisher, ILogger<CoreNotificationService>? logger = null)
    {

        public virtual async Task PublishAsync<T>(T notification)
        {
            try
            {
                switch (notification)
                {
                    case LessonFailureNotification failureNotification:
                        await publisher.PublishAsync(failureNotification);
                        break;
                    case LessonNotification lessonNotification:
                        await publisher.PublishAsync(lessonNotification);
                        break;
                    case LessonPreparationNotification preparationNotification:
                        await publisher.PublishAsync(preparationNotification);
                        break;
                    default:
                        logger?.LogWarning($"Notification {notification} wasn't pushed because no valid notificators initialized");
                        break;
                }
            }
            catch(Exception ex)
            {
                logger?.LogError(ex, "Failed to push notification");
            }
        }
    }
}
