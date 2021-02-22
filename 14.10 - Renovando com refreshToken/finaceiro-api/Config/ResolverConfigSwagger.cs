using Domain.Infraestructure.Security;
using LuizMario.Dto.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace finaceiro_api.Config
{
    public static class ResolverConfigSwagger
    {
        public static void Resolver(this IServiceCollection services, IConfiguration configuration) 
        {
            var tokenConfiguration = new TokenConfigurationDto();
            new ConfigureFromConfigurationOptions<TokenConfigurationDto>(
                    configuration.GetSection("Token")
                ).Configure(tokenConfiguration);

            var siginConfigurations = new SiginConfigurations();

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

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                swagger.AddSecurityDefinition("Bearer", securitySchema);

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                services.AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                }).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenConfiguration.Issuer,
                        ValidAudience = tokenConfiguration.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(siginConfigurations.Key.ToString()))
                    };
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swagger.IncludeXmlComments(xmlPath);
            });
        }
    }
}
