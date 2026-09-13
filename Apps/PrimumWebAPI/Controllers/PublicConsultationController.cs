using CoreConnection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PrimumWebAPI.Controllers
{
    [Route("public/consultation")]
    [AllowAnonymous]
    public class PublicConsultationController(PublicClient client): DefaultController
    {
        /// <summary>
        /// Подать заявку на консультацию
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<int>> RequestConsultation(
            [FromBody] ConsultationRequestInput input)
            => Ok(await client.ConsultationRequestAsync(input));
    }
}
