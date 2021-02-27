using Domain.Infraestructure.Controllers;
using Domain.Infraestructure.Notifications;
using LuizMario.Domain.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace finaceiro_api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AuthorityController : BasicController
    {
        private readonly AuthorizationService _service;
        public AuthorityController(AuthorizationService service, INotification notification) : base(notification)
        {
            this._service = service;
        }

        /// <summary>
        ///  Finnd all by profile 
        /// </summary> 
        /// <returns>Authorities</returns>
        /// <response code="200">Return success</response>
        /// <response code="404">Return not exists</response>
        /// <response code="401">User not authenticed</response>
        [HttpGet("{profileId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult FindAllByProfile(int profileId) 
        {
            var authorities = _service.FindAllByProfile(profileId);

            if (authorities.Count() == 0) 
            {
                return NotFound();
            }

            return Ok(authorities);
        }
    }
}
