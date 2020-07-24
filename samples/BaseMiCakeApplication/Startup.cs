using AutoMapper;
using BaseMiCakeApplication.Domain.Aggregates;
using BaseMiCakeApplication.EFCore;
using BaseMiCakeApplication.Handlers;
using BaseMiCakeApplication.MiCakeFeatures;
using BaseMiCakeApplication.Utils;
using IdentityModel;
using MiCake;
using MiCake.AspNetCore.Modules;
using MiCake.AspNetCore.Security;
using MiCake.Identity.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NSwag.Generation.Processors.Security;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace BaseMiCakeApplication
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
            services.AddControllers(options =>
            {
            });

            services.AddDbContext<BaseAppDbContext>(options =>
            {
                options.UseMySql("Server=localhost;Database=micakeexample;User=root;Password=WHsunjee_2018;", mySqlOptions => mySqlOptions
                    .ServerVersion(new ServerVersion(new Version(10, 5, 0), ServerType.MariaDb)));
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            VerifyUserClaims.UserID = GlobalArgs.ClaimUserId;
            var seurityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(Configuration["JwtConfig:SecurityKey"]));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(jwtOptions =>
                    {
                        jwtOptions.TokenValidationParameters = new TokenValidationParameters
                        {
                            NameClaimType = ClaimTypes.NameIdentifier,
                            RoleClaimType = ClaimTypes.Role,

                            ValidIssuer = Configuration["JwtConfig:Issuer"],
                            ValidAudience = Configuration["JwtConfig:Audience"],
                            IssuerSigningKey = seurityKey
                        };
                    });

            services.AddMiCakeWithDefault<BaseAppDbContext, BaseMiCakeModule>(
                miCakeConfig: config =>
                {
                    config.Handlers.Add(new DemoExceptionHanlder());
                },
                miCakeAspNetConfig: options =>
                {
                    options.UseCustomModel();
                    options.DataWrapperOptions.IsDebug = true;
                })
                .UseIdentity<User>()
                .Build();

            //Add Swagger
            //配置Mvc + json 序列化
            //services.AddMvc(options => { options.EnableEndpointRouting = false; })
            //        .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            //            .AddNewtonsoftJson(options =>
            //            {
            //                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm";
            //            });





            services.AddSwaggerDocument(document =>
            {
                //document.PostProcess = document =>
                //{
                //    document.Info.Version = "v1";
                //    document.Info.Title = "UserManageApp API";
                //    document.Info.Description = "A simple ASP.NET Core web API";
                //    document.Info.TermsOfService = "None";
                //    document.Info.Contact = new NSwag.OpenApiContact
                //    {
                //        Name = "wrs",
                //        Email = "335826963@qq.com",
                //        Url = "https://example.com"
                //    };
                //    document.Info.License = new NSwag.OpenApiLicense
                //    {
                //        Name = "Use under LICX",
                //        Url = "https://example.com/license"
                //    };
                //};
                document.DocumentName = "MiCake Demo Application";
                document.AddSecurity("JWT", new NSwag.OpenApiSecurityScheme()
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）",
                    Name = "Authorization",
                    In = NSwag.OpenApiSecurityApiKeyLocation.Header,
                    Type = NSwag.OpenApiSecuritySchemeType.ApiKey
                });
                document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });


            #region BugPatch
            //修复IJwtSupporter在反射MiCakeUser的时候 没有获取到私有属性的bug
            services.Replace(new ServiceDescriptor(typeof(IJwtSupporter), typeof(JwtSupporter), ServiceLifetime.Singleton));
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.StartMiCake();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();

            app.UseSwaggerUi3();
            //app.UseMvc();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //            name: "default",
            //            template: "{controller=Home}/{action=Index}/{id?}");
            //});

        }
    }
}
