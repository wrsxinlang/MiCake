using Senparc.Weixin.TenPay.V3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.WeChat
{
    public class WxChatPayResult
    {
        public WxChatPayResult(string key, string appId, string prepay_id)
        {
            this.appId = appId;
            this.package = "prepay_id=" + prepay_id;
            this.key = key;
        }
        public string appId { get; }
        public string timeStamp { get; } = TenPayV3Util.GetTimestamp();
        public string nonceStr { get; } = TenPayV3Util.GetNoncestr();
        public string package { get; }
        public string key { get; }
        public string signType { get; } = "MD5";
        private string _paySign;
        public string paySign
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_paySign))
                {
                    _paySign = TenPayV3.GetJsPaySign(appId, timeStamp, nonceStr, package, key);
                }
                return _paySign;
            }
        }
    }
}
