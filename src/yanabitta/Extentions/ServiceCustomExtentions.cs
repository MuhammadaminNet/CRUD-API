using Data.IRepositories;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Services.IServices;
using Services.Services;
using yanabitta.Auth;

namespace yanabitta.Extentions
{
    public static class ServiceCustomExtentions
    {
        public static void AddCustomExtentions(this IServiceCollection services)
        {
            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
        }

        public static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v2", new OpenApiInfo { Title = "yanabitta", Version = "v2", Description = "luboy"});
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }
    }
}
