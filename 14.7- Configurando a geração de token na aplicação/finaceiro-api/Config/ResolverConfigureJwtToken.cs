using Domain.Infraestructure.Security;
using LuizMario.Dto.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace finaceiro_api.Config
{
    public static class ResolverConfigureJwtToken
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
        }
    }
}
