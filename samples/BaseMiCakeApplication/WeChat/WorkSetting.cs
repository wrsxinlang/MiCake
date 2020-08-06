using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.WeChat
{
    public class WorkSetting : IOptions<WorkSetting>
    {
        public WorkSetting Value => this;
        /// <summary>
        /// 公司ID
        /// </summary>
        public string CorpId { get; set; }
        /// <summary>
        /// 通讯录秘钥
        /// </summary>
        public string ContactsSecret { get; set; }
        /// <summary>
        /// 通讯录AccessToken键
        /// </summary>
        public string ContactsAccessTokenKey { get; set; }
        /// <summary>
        /// 消息中心应用AppID
        /// </summary>
        public string MsgAppId { get; set; }
        /// <summary>
        /// 消息中心应用秘钥
        /// </summary>
        public string MsgSecret { get; set; }
        /// <summary>
        /// 消息中心应用AccessToken键
        /// </summary>
        public string MsgAccessTokenKey { get; set; }
        /// <summary>
        /// 登录应用AppID
        /// </summary>
        public string LoginAppId { get; set; }
        /// <summary>
        /// 登录应用秘钥
        /// </summary>
        public string LoginSecret { get; set; }
        /// <summary>
        /// 登录应用AccessToken键
        /// </summary>
        public string LoginAccessTokenKey { get; set; }
        /// <summary>
        /// 推送消息host
        /// </summary>
        public string SendMsgHost { get; set; }
        //public IEnumerable<WorkUser> SystemUser { get; set; }
    }
}
