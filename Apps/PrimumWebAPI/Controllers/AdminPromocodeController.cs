using CoreConnection;
using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Extensions;

namespace PrimumWebAPI.Controllers
{
    [Route("admin/promocodes")]
    [Authorize]
    public class AdminPromocodeController(AdminClient client) : DefaultController
    {

        /// <summary>
        /// Посмотреть все промокоды (даже купленные)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PromocodeDto>>> GetPromocodes()
            => Ok(await client.AllPromocodesAsync(User.GetUserId()));

        /// <summary>
        /// Конкретный промокод
        /// </summary>
        /// <param name="promocodeId"></param>
        /// <returns></returns>
        [HttpGet("{promocodeId}")]
        public async Task<ActionResult<PromocodeDto>> GetPromocode([FromRoute] int promocodeId)
            => Ok(await client.PromocodeAsync(User.GetUserId(), promocodeId));

        /// <summary>
        /// Добавить промокод. Только для админов с правом AddPromocodes
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<int>> AddPromocode([FromBody] PromocodeInputDto dto = null!)
            => Ok(await client.AddPromocodeAsync(User.GetUserId(), dto));

        /// <summary>
        /// Удалить промокод. Только для админов с правом DeletePromocodes
        /// </summary>
        /// <param name="promocodeId"></param>
        /// <returns></returns>
        [HttpDelete("{promocodeId}")]
        public async Task<ActionResult<int>> DeletePromocode([FromRoute] int promocodeId)
            => Ok(await client.DeletePromocodeAsync(User.GetUserId(), promocodeId));
    }
}
