using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using Senparc.Weixin;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.TenPay;
using Senparc.Weixin.TenPay.V3;
using Microsoft.AspNetCore.Http;

namespace BaseMiCakeApplication.WeChat
{
    public class MpApi
    {
        private IOptions<MpSetting> mpSetting;
        private IHostEnvironment env;
        public MpApi(IOptions<MpSetting> mpSetting, IHostEnvironment env) { this.mpSetting = mpSetting; this.env = env; }
        public string GetAuthorizeUrl(string url)
        {
            return OAuthApi.GetAuthorizeUrl(mpSetting.Value.AppID, url, "", Senparc.Weixin.MP.OAuthScope.snsapi_base);
        }
        public string GetOpenID(string code)
        {
            var reObj = OAuthApi.GetAccessToken(mpSetting.Value.AppID, mpSetting.Value.AppSecret, code);
            return reObj.openid;
        }
        public string PaySuccess(HttpContext httpContext, Action<string> ac)
        {
            var response = new ResponseHandler(httpContext);
            var request = new RequestHandler(null);
            var orderQuery = new OrderQueryResult(response.ParseXML());
            if (!orderQuery.IsResultCodeSuccess() || !orderQuery.IsResultCodeSuccess())
            {
                request.SetParameter("return_msg", "Fail");
            }
            else
            {
                ac(orderQuery.out_trade_no);
                request.SetParameter("return_msg", "SUCCESS");
            }

            request.SetParameter("return_code", "SUCCESS");
            return request.ParseXML();
        }
        public WxChatPayResult Pay(decimal amount, string openID, string title, out string orderID)
        {
            var m = mpSetting.Value;
            var price = Convert.ToInt32(amount * 100);
            orderID = TenPayV3Util.BuildDailyRandomStr(32);
            //var amount = 1;
            //var openid = "ohYxb0-EBw7Vr2_H5WhqJRW6k62A";
            var param = new TenPayV3UnifiedorderRequestData(
                m.ParentAppID, m.ParentMchID, m.AppID, m.MchID, title, orderID, price,
                "127.0.0.1", m.NotifyUrl, TenPayV3Type.JSAPI, "", openID, m.Key, TenPayV3Util.GetNoncestr());
            var q = TenPayV3.Unifiedorder(param);
            if (q.IsResultCodeSuccess() && q.IsReturnCodeSuccess())
            {
                var re = new WxChatPayResult(m.Key, q.appid, q.prepay_id);
                return re;
            }
            return null;
        }
    }
}
