using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;

namespace finaceiro_api.Config
{
    public static class ResolverConfigSwagger
    {
        public static void Resolver(this IServiceCollection services) 
        {
            services.AddSwaggerGen(swagger => 
            {
                swagger.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "financeiro-api",
                    Description = "Aplicação para controle de despessas e receitas pessoais.",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact 
                    { 
                        Name = "Luiz Mário",
                        Email = "luizmariodev@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/luiz-mário-ferreira-191936ba") 
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swagger.IncludeXmlComments(xmlPath);
            });
        }
    }
}
