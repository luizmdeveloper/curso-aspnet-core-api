using Domain.Infraestructure.Controllers;
using Domain.Infraestructure.Notifications;
using Domain.Infraestructure.Security;
using LuizMario.Domain.Core.Service;
using LuizMario.Dto.Configuration;
using LuizMario.Dto.Input;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        [HttpPost]
        public ActionResult GenerationToken(UserInputDto userInput) 
        {
            string token = string.Empty;

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
    }
}
