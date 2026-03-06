using CoreConnection;
using CoreConnection.DTOs;
using CoreDBModel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Extensions;

namespace PrimumWebAPI.Controllers
{
    [Route("student/promocodes")]
    [Authorize]
    public class StudentPromocodeController(StudentClient client) : DefaultController
    {
        /// <summary>
        /// Все купленные учеником промокоды
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PromocodeDto>>> GetStudentPromocodes()
            => Ok(await client.PromocodesAsync(User.GetUserId()));

        /// <summary>
        /// Посмотреть доступные ученику к покупке промокоды
        /// </summary>
        /// <returns></returns>
        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<PromocodeDto>>> GetPromocodes()
            => Ok(await client.AvailablePromocodesAsync(User.GetUserId()));

        /// <summary>
        /// Посмотреть конкретный купленный промокод
        /// </summary>
        /// <returns></returns>
        [HttpGet("{promocodeId}")]
        public async Task<ActionResult<PromocodeDto>> GetPromocode([FromRoute] int promocodeId)
            => Ok(await client.PromocodeAsync(promocodeId, User.GetUserId()));

        /// <summary>
        /// Купить промокод
        /// </summary>
        /// <param name="promocodeId"></param>
        /// <returns></returns>
        [HttpPost("buy/{promocodeId}")]
        public async Task<ActionResult<PromocodeDto>> BuyPromocode([FromRoute] int promocodeId)
            => Ok(await client.BuyPromocodeAsync(User.GetUserId(), promocodeId));
    }
}
