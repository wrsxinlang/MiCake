using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Senparc.CO2NET;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.RegisterServices;
using Senparc.Weixin.Work.Containers;

namespace BaseMiCakeApplication.WeChat
{
    public static class WorkServiceCollectionExtensions
    {
        public static SenparcSetting senparcSetting;
        public static SenparcWeixinSetting senparcWeixinSetting;
        public static IServiceCollection AddWork(this IServiceCollection services, IConfiguration configuration)
        {
            senparcSetting = configuration.Get<SenparcSetting>();
            senparcWeixinSetting = configuration.Get<SenparcWeixinSetting>();
            services.Configure<WorkSetting>(configuration.GetSection("WorkSetting"))
                .Configure<MpSetting>(configuration.GetSection("MpSetting"))
                .AddSenparcGlobalServices(configuration)
                .AddSenparcWeixinServices(configuration)
                .AddSingleton<IWorkApi, WorkApi>()
                .AddSingleton<MpApi>();
            return services;
        }
        public static IApplicationBuilder UserWork(this IApplicationBuilder app, IHostEnvironment env)
        {
            var register = Senparc.CO2NET.AspNet.RegisterServices.RegisterService.Start(env, senparcSetting).UseSenparcGlobal();
            register.UseSenparcWeixin(senparcWeixinSetting, senparcSetting);//.RegisterWorkAccount(senparcWeixinSetting.Value, "企业微信");
            var workSetting = app.ApplicationServices.GetService<IOptions<WorkSetting>>().Value;
            workSetting.ContactsAccessTokenKey = AccessTokenContainer.BuildingKey(workSetting.CorpId, workSetting.ContactsSecret);
            AccessTokenContainer.RegisterAsync(workSetting.CorpId, workSetting.ContactsSecret, "Contacts");
            workSetting.MsgAccessTokenKey = AccessTokenContainer.BuildingKey(workSetting.CorpId, workSetting.MsgSecret);
            AccessTokenContainer.RegisterAsync(workSetting.CorpId, workSetting.MsgSecret, "Msg");
            workSetting.LoginAccessTokenKey = AccessTokenContainer.BuildingKey(workSetting.CorpId, workSetting.LoginSecret);
            AccessTokenContainer.RegisterAsync(workSetting.CorpId, workSetting.LoginSecret, "Login");
            return app;
        }
    }
}
