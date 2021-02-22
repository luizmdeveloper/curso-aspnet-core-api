using Domain.Infraestructure.Controllers;
using Domain.Infraestructure.Notifications;
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
        public AuthenticationController(INotification notification) : base(notification)
        {
        }

        [HttpPost]
        public ActionResult GenerationToken(UserInputDto userInput) 
        {
            if (IsModelValid()) 
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("++^x8pWYeg(aXqE_");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", "1") }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok (new {
                    Token = tokenHandler.WriteToken(token)
                });
            }

            return CustomizeResponse();
        }
    }
}
