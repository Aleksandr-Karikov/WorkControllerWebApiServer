using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WorkController.WebApi.Common;
using WorkController.WebApi.DataBase.Context;
using WorkController.WebApi.IServices;
using WorkController.WebApi.Services;

namespace WorkController.WebApi
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
            //  var authOptionsConfiguration = Configuration.GetSection("Auth");
            //   services.Configure<AuthOptions>(authOptionsConfiguration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WorkController.WebApi", Version = "v1" });
            });
            services.AddScoped<IUserService, UserService>();
            services.AddDbContext<WorkControllerContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DBConnection"))
                );
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.RequireHttpsMetadata = false;
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           // ��������, ����� �� �������������� �������� ��� ��������� ������
                           ValidateIssuer = true,
                           // ������, �������������� ��������
                           ValidIssuer = AuthOptions.ISSUER,

                           // ����� �� �������������� ����������� ������
                           ValidateAudience = true,
                           // ��������� ����������� ������
                           ValidAudience = AuthOptions.AUDIENCE,
                           // ����� �� �������������� ����� �������������
                           ValidateLifetime = true,

                           // ��������� ����� ������������
                           IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                           // ��������� ����� ������������
                           ValidateIssuerSigningKey = true,
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiWorkControllerServer v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
