using BaseMiCakeApplication.Domain.Aggregates.Account;
using BaseMiCakeApplication.EFCore;
using BaseMiCakeApplication.Handlers;
using BaseMiCakeApplication.MiCakeFeatures;
using BaseMiCakeApplication.Utils;
using MiCake;
using MiCake.AspNetCore.Modules;
using MiCake.AspNetCore.Security;
using MiCake.Identity.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSwag.Generation.Processors.Security;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System;
using System.IO;
using System.Security.Claims;
using System.Text;

using Senparc.CO2NET;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.RegisterServices;
using Senparc.Weixin.Work;
using BaseMiCakeApplication.WeChat;

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
            services.AddControllers();

            services.AddCors(options => options.AddPolicy("AllowAny", p => p.WithOrigins("http://localhost:8080", "https://mlcy.xyz").AllowAnyMethod().AllowAnyHeader().AllowCredentials()));

            services.AddDbContext<BaseAppDbContext>(options =>
            {
                options.UseMySql("Server=119.45.209.118;Database=micakeexample;User=root;Password=a12345;", mySqlOptions => mySqlOptions
                    .ServerVersion(new ServerVersion(new Version(5, 5, 65), ServerType.MariaDb)));
            });
            //企业微信
            services.AddWork(Configuration);
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
            services.AddSwaggerDocument(document =>
            {
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

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 268435456;//long.MaxValue;
            });

            #region BugPatch
            //修复IJwtSupporter在反射MiCakeUser的时候 没有获取到私有属性的bug
            services.Replace(new ServiceDescriptor(typeof(IJwtSupporter), typeof(JwtSupporter), ServiceLifetime.Singleton));
            #endregion

            services.AddMemoryCache();//使用本地缓存必须添加

            //services.AddSignalR();//使用 SignalR   -- DPBMARK WebSocket DPBMARK_END
            //services.AddSenparcWeixinServices(Configuration);//Senparc.Weixin 注册 

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IOptions<SenparcSetting> senparcSetting, IOptions<SenparcWeixinSetting> senparcWeixinSetting)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images")),
                RequestPath = new PathString("/Upload")
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images")),
                RequestPath = new PathString("/Upload")
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //初始化企业微信
            app.UserWork(env);
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("AllowAny");
            app.UseAuthentication();
            app.UseAuthorization();

            app.StartMiCake();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();

            ////使用 SignalR（.NET Core 3.0）                                                      -- DPBMARK WebSocket
            //app.UseEndpoints(endpoints =>
            //{
            //    //配置自定义 SenparcHub
            //    endpoints.MapHub<SenparcHub>("/SenparcHub");
            //});                                                                                  // DPBMARK_END
            //app.UseSenparcGlobal(env, senparcSetting.Value, globalRegister => { })
            //   .UseSenparcWeixin(senparcWeixinSetting.Value, weixinRegister =>
            //    {
            //       weixinRegister.RegisterWorkAccount(senparcWeixinSetting.Value, "【网络小助手】公众号");
            //    });

            // 启动 CO2NET 全局注册，必须！ 
            //IRegisterService register = RegisterService.Start(senparcSetting.Value).UseSenparcGlobal(false, null);

            ////开始注册微信信息，必须！ 
            //register.UseSenparcWeixin(senparcWeixinSetting.Value, senparcSetting.Value)
            //#region 注册企业号（按需） 

            //    //注册企业号 
            //    .RegisterWorkAccount(
            //        senparcWeixinSetting.Value.WeixinCorpId,
            //        senparcWeixinSetting.Value.WeixinCorpSecret,
            //        "【盛派网络】企业微信");
            ////还可注册任意多个企业号 

            //#endregion
        }
    }
}
