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
        /// Создать заявку на пополнение баланса
        /// </summary>
        /// <returns></returns>
        [HttpPost("request-topup")]
        public async Task<ActionResult<string>> CreateTopupRequest([FromRoute] decimal amount)
            => Ok(await paymentClient.RequestTopupStudentBalanceAsync(User.GetUserId(), amount));

        /// <summary>
        /// Создать заявку на пополнение баланса
        /// </summary>
        /// <returns></returns>
        [HttpPost("request-withdrawn")]
        public async Task<ActionResult> CreateBalanceRequest([FromRoute] decimal amount)
            => Ok(await paymentClient.WithdrawStudentBalanceAsync(User.GetUserId(), amount));
    }
}
