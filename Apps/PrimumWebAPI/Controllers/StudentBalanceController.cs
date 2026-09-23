using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentServiceConnection;
using PaymentServiceConnection.Models;
using PrimumWebAPI.Extensions;

namespace PrimumWebAPI.Controllers
{
    [Route("student/balance")]
    [Authorize]
    public class StudentBalanceController(PaymentServiceClient paymentClient) : DefaultController
    {
        private const string InvalidAmountMessage = "Amount must be positive";

        /// <summary>
        /// Создать заявку на пополнение баланса. Возвращает ссылку для оплаты, которую нужно открыть в браузере, чтобы оплатить баланс. После оплаты баланс будет пополнен автоматически.
        /// </summary>
        /// <returns></returns>
        [HttpPost("request-topup")]
        public async Task<ActionResult<PaymentResponse>> CreateTopupRequest([FromQuery] decimal amount)
        {
            if (amount <= 0) { return BadRequest(new { error = InvalidAmountMessage }); }
            return Ok(await paymentClient.RequestTopupStudentBalanceAsync(User.GetUserId(), amount));
        }

        [HttpGet]
        public async Task<ActionResult<int>> GetBalance()
            => Ok(await paymentClient.GetStudentBalanceAsync(User.GetUserId()));

        /// <summary>
        /// Создать заявку на списание баланса. Пока ничего не возвращает.
        /// </summary>
        /// <returns></returns>
        [HttpPost("request-withdrawn")]
        public async Task<ActionResult> CreateBalanceRequest([FromQuery] decimal amount)
        {
            if (amount <= 0) { return BadRequest(new { error = InvalidAmountMessage }); }
            return Ok(await paymentClient.WithdrawStudentBalanceAsync(User.GetUserId(), amount));
        }
    }
}
