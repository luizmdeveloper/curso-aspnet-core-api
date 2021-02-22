using Domain.Infraestructure.Notifications;
using Domain.Infraestructure.Security;
using Domain.Infraestructure.Services;
using LuizMario.Domain.Core.Entity;
using LuizMario.Dto.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace LuizMario.Domain.Core.Service
{
    public class TokenService : BaseService
    {
        private readonly TokenConfigurationDto _tokenConfiguration;
        private readonly SiginConfigurations _siginConfigurations;

        public TokenService(
            TokenConfigurationDto tokenConfiguration, 
            SiginConfigurations siginConfigurations, 
            INotification notification
        ) : base(notification)
        {
            this._tokenConfiguration = tokenConfiguration;
            this._siginConfigurations = siginConfigurations;
        }

        public string Generation(User user)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.Id.ToString(), "Login"),
                new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Name),
                    new Claim("Profile", user.Profile.Name),
                }
            );

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor() 
            {             
                Issuer = _tokenConfiguration.Issuer,
                Audience =  _tokenConfiguration.Audience,
                SigningCredentials = _siginConfigurations.signingCredentials,
                Subject = identity,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddSeconds(_tokenConfiguration.Seconds)
            });

            return handler.WriteToken(securityToken);
        }
    }
}
