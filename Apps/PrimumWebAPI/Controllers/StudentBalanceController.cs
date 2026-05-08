using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentServiceConnection;
using PrimumWebAPI.Extensions;

namespace PrimumWebAPI.Controllers
{
    [Route("student/balance")]
    [Authorize]
    public class StudentBalanceController(PaymentServiceClient paymentClient) : DefaultController
    {
        /// <summary>
        /// Создать заявку на пополнение баланса. Возвращает ссылку для оплаты, которую нужно открыть в браузере, чтобы оплатить баланс. После оплаты баланс будет пополнен автоматически.
        /// </summary>
        /// <returns></returns>
        [HttpPost("request-topup")]
        public async Task<ActionResult<string>> CreateTopupRequest([FromQuery] decimal amount)
            => Ok(await paymentClient.RequestTopupStudentBalanceAsync(User.GetUserId(), amount));

        /// <summary>
        /// Создать заявку на списание баланса. Пока ничего не возвращает.
        /// </summary>
        /// <returns></returns>
        [HttpPost("request-withdrawn")]
        public async Task<ActionResult> CreateBalanceRequest([FromQuery] decimal amount)
            => Ok(await paymentClient.WithdrawStudentBalanceAsync(User.GetUserId(), amount));
    }
}
