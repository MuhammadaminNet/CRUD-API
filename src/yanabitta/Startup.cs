using Data.DbContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.Helpers;
using System.Text;
using yanabitta.Extentions;
using yanabitta.Middlewares;

namespace yanabitta
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddDbContext<UserDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("conStr")));


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "yanabitta", Version = "v1" });
            });

            ServiceCustomExtentions.AddCustomExtentions(services);
            ServiceCustomExtentions.AddSwaggerService(services);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(
                                Configuration["JWT:Key"]
                                )
                            )
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "yanabitta v1"));
            }

            EnvironmentHelper.WebRootPath = app.ApplicationServices.GetService<IWebHostEnvironment>()?.WebRootPath;


            app.UseMiddleware<UserExceptionMiddleWare>();

            app.UseHttpsRedirection();

            app.UseAuthentication();    
            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
