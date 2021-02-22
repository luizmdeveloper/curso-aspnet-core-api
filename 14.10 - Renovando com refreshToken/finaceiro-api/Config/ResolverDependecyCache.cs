using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace finaceiro_api.Config
{
    public static class ResolverDependecyCache
    {
        public static void Resolver(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("redis-server");
                options.InstanceName = "finance-api";
            });
        }
    }
}
