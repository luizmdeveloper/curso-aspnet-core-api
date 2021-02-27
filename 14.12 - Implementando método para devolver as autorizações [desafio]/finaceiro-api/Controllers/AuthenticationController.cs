using Domain.Infraestructure.Controllers;
using Domain.Infraestructure.Notifications;
using LuizMario.Domain.Core.Service;
using LuizMario.Dto.Input;
using LuizMario.Dto.OutputDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace finaceiro_api.Controllers
{
    [Route("v1/[controller]")]
    public class AuthenticationController : BasicController
    {
        private readonly UserService _service;
        private readonly TokenService _tokenService;

        public AuthenticationController(
            UserService service, 
            TokenService tokenService,
            INotification notification
        ) : base(notification)
        {
            this._service = service;
            this._tokenService = tokenService;
        }

        /// <summary>
        /// Authorization user.
        /// </summary> 
        /// <returns>Access token</returns>
        /// <response code="200">Return success</response>
        /// <response code="400">Return error validation</response>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult GenerationToken(UserInputDto userInput) 
        {
            TokenOutputDto token = null;
            if (IsModelValid()) 
            {
                var user = _service.FindByEmailAndPassword(userInput);
                if (user == null)
                {
                    AddError("Usuário e senha invalido");
                }
                else 
                {
                    token = _tokenService.Generation(user);
                }                
            }

            return CustomizeResponse(token);
        }

        /// <summary>
        /// Renew token.
        /// </summary> 
        /// <returns>Access token</returns>
        /// <response code="200">Return success</response>
        /// <response code="400">Return error validation</response>
        /// <response code="401">Return no authentication</response>
        [HttpPost("refreshToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult RefreshToken(RefreshTokenInputDto refreshTokenInput) 
        {
            TokenOutputDto token = null;
            if (IsModelValid() && refreshTokenInput.GrantType.ToLower().Equals("refresh_token")) 
            {
                var user = _service.FindById(CurrentUser);
                token = _tokenService.RenewToken(refreshTokenInput, user);
            }

            return CustomizeResponse(token);
        }

        /// <summary>
        /// Logout.
        /// </summary> 
        /// <response code="204">Return success</response>
        /// <response code="400">Return error validation</response>
        /// <response code="401">Return no authentication</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("logout")]
        public ActionResult Logout(LogoutInputDto logoutInput) 
        {
            if (IsModelValid()) 
            {
                _tokenService.RemoveCache(logoutInput.RefreshToken);
            }

            return NoContentCustomize();
        }
    }
}
