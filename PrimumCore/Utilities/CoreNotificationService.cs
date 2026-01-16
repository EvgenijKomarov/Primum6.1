using DataNotifications;
using DataNotificator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimumCore.Services
{
    public class CoreNotificationService
    {
        public CoreNotificationService(IPublisherFactory publisherFactory, ILogger<CoreNotificationService>? logger = null)
        {
            LessonFailureNotificator = publisherFactory.GetPublisher<LessonFailureNotification>();
            LessonNotificator = publisherFactory.GetPublisher<LessonNotification>();
            LessonPreparationNotificator = publisherFactory.GetPublisher<LessonPreparationNotification>();
            _logger = logger;
        }
        private ILogger<CoreNotificationService>? _logger;
        private IPublisher<LessonPreparationNotification> LessonPreparationNotificator;
        private IPublisher<LessonFailureNotification> LessonFailureNotificator;
        private IPublisher<LessonNotification> LessonNotificator;

        public virtual async Task PublishAsync<T>(T notification)
        {
            try
            {
                switch (notification)
                {
                    case LessonFailureNotification failureNotification:
                        await LessonFailureNotificator.PublishAsync(failureNotification);
                        _logger?.LogInformation($"Notification {notification} successfuly pushed on <LessonFailureNotification>");
                        break;
                    case LessonNotification lessonNotification:
                        await LessonNotificator.PublishAsync(lessonNotification);
                        _logger?.LogInformation($"Notification {notification} successfuly pushed on <LessonNotification>");
                        break;
                    case LessonPreparationNotification preparationNotification:
                        await LessonPreparationNotificator.PublishAsync(preparationNotification);
                        _logger?.LogInformation($"Notification {notification} successfuly pushed on <LessonPreparationNotification>");
                        break;
                    default:
                        _logger?.LogWarning($"Notification {notification} wasn't pushed because no valid notificators initialized");
                        break;
                }
            }
            catch(Exception ex)
            {
                _logger?.LogError(ex, "Failed to push notification");
            }
        }
    }
}
