using Domain.Infraestructure.Notifications;
using LuizMario.Domain.Core.Repository;
using LuizMario.Domain.Core.Service;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System.Data;

namespace finaceiro_api.Config
{
    public static class ResolverDependencyConfig
    {
        public static void Resolver(this IServiceCollection services) 
        {
            services.AddScoped<IDbConnection, MySqlConnection>();
            services.AddScoped<INotification, Notification>();

            services.AddScoped<CategoryRepository>();
            services.AddScoped<PersonRepository>();
            services.AddScoped<FinancialMovimentRepository>();
            services.AddScoped<AuthorizationRepository>();
            services.AddScoped<ProfileRepository>();
            services.AddScoped<UserRepository>();

            services.AddScoped<CategoryService>();
            services.AddScoped<PersonService>();
            services.AddScoped<FinancialMovimentService>();
            services.AddScoped<AuthorizationService>();
            services.AddScoped<ProfileService>();
            services.AddScoped<UserService>();
            services.AddScoped<TokenService>();
        }
    }
}
