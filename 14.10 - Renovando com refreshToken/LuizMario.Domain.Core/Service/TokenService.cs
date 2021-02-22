using Domain.Infraestructure.Notifications;
using Domain.Infraestructure.Security;
using Domain.Infraestructure.Services;
using LuizMario.Domain.Core.Entity;
using LuizMario.Dto;
using LuizMario.Dto.Configuration;
using LuizMario.Dto.Input;
using LuizMario.Dto.OutputDto;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
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
        private readonly IDistributedCache _cache;

        public TokenService(
            TokenConfigurationDto tokenConfiguration, 
            SiginConfigurations siginConfigurations,
            IDistributedCache cache,
            INotification notification
        ) : base(notification)
        {
            this._tokenConfiguration = tokenConfiguration;
            this._siginConfigurations = siginConfigurations;
            this._cache = cache;
        }

        public TokenOutputDto Generation(User user)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.Id.ToString(), "Login"),
                new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Name),
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("UserName", user.Name),
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

            var token = new TokenOutputDto
            {
                Token = handler.WriteToken(securityToken),
                RefreshToken = Guid.NewGuid().ToString(),
                UserId = user.Id
            };

            SaveCache(token);

            return token;
        }

        public TokenOutputDto RenewToken(RefreshTokenInputDto refreshTokenInput, User user)
        {
            string refreshTokenStr = _cache.GetString(refreshTokenInput.RefreshToken);
            if (!string.IsNullOrWhiteSpace(refreshTokenStr)) 
            {
                var refreshTokenData = JsonConvert.DeserializeObject<RefreshTokenDto>(refreshTokenStr);

                if (refreshTokenData != null)
                {
                    _cache.Remove(refreshTokenInput.RefreshToken);
                    return Generation(user);
                }
            }

            return null;
        }

        private void SaveCache(TokenOutputDto token)
        {
            var refreshTokenData = new RefreshTokenDto
            {
                UserId = token.UserId,
                RefreshToken = token.RefreshToken
            };

            var timeExpresion = TimeSpan.FromSeconds(_tokenConfiguration.RefreshTokenSeconds);

            DistributedCacheEntryOptions optionsCahce = new DistributedCacheEntryOptions();
            optionsCahce.SetAbsoluteExpiration(timeExpresion);

            _cache.SetString(refreshTokenData.RefreshToken, JsonConvert.SerializeObject(refreshTokenData), optionsCahce);
        }
    }
}
