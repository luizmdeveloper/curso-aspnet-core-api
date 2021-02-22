using Domain.Infraestructure.Security;
using LuizMario.Dto.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;

namespace finaceiro_api.Config
{
    public static class ResolverConfigureAuthentication
    {
        public static void Resolver(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenConfiguration = new TokenConfigurationDto();
            new ConfigureFromConfigurationOptions<TokenConfigurationDto>(
                    configuration.GetSection("Token")
                ).Configure(tokenConfiguration);

            services.AddSingleton(tokenConfiguration);

            var siginConfigurations = new SiginConfigurations();
            services.AddSingleton(siginConfigurations);

            services.AddAuthentication(authOption =>
            {
                authOption.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOption.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOption =>
            {
                var paramSolution = bearerOption.TokenValidationParameters;
                paramSolution.IssuerSigningKey = siginConfigurations.Key;
                paramSolution.ValidAudience = tokenConfiguration.Audience;
                paramSolution.ValidIssuer = tokenConfiguration.Issuer;
                paramSolution.ValidateIssuerSigningKey = true;
                paramSolution.ValidateLifetime = true;
                paramSolution.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("auth", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }
    }
}
