using CommonNotificationServiceClient;
using CommonNotificationServiceClient.Models;
using CoreConnection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Extensions;

namespace PrimumWebAPI.Controllers
{
    [Authorize]
    [Route("user/common-notifications")]
    public class UserCommonNotificationsController(CommonNotificationClient client): DefaultController
    {
        /// <summary>
        /// Получить все уведомления пользователя
        /// </summary>
        /// <returns>Список уведомлений (20 максимум)</returns>
        [HttpGet]
        public async Task<ActionResult<CommonNotification>> GetNotifications()
            => Ok(await client.GetUserNotificationsAsync(User.GetUserId()));

        /// <summary>
        /// Отметить уведомление просмотренным
        /// </summary>
        /// <param name="notificationId">ID уведомления</param>
        /// <returns>Успешность операции</returns>
        [HttpPost("set-seen/{notificationId}")]
        public async Task<ActionResult<bool>> SetNotificationSeen([FromRoute] string notificationId)
            => Ok(await client.SetSeenNotificationAsync(notificationId));
    }
}
