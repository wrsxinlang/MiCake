using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Senparc.NeuChar.Entities;
using Senparc.Weixin;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.Asynchronous;
using Senparc.Weixin.Work.AdvancedAPIs.MailList.Member;
using Senparc.Weixin.Work.AdvancedAPIs.Media;
using Senparc.Weixin.Work.Containers;
using Senparc.Weixin.Work.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.WeChat
{
    public class WorkApi:IWorkApi
    {
        private IOptions<WorkSetting> workSetting;
        private IHostEnvironment env;
        public WorkApi(IOptions<WorkSetting> workSetting, IHostEnvironment env) { this.workSetting = workSetting; this.env = env; }
        public void SendMsg(string toUser, string toParty, string title, string content, string url, string btntxt = null)
        {
            if (!string.IsNullOrWhiteSpace(url) && url.IndexOf("http") < 0)
                url = workSetting.Value.SendMsgHost + url;
            var reObj = MassApi.SendTextCard(workSetting.Value.MsgAccessTokenKey, workSetting.Value.MsgAppId, title, content, url, btntxt: btntxt, toUser: toUser, toParty: toParty);
        }
        public void SendTextMsg(string content, string toUser = null, string toParty = null)
        {
            var reObj = MassApi.SendText(workSetting.Value.MsgAccessTokenKey, workSetting.Value.MsgAppId, content, toUser: toUser, toParty: toParty);
        }
        public void SendNew(string title, string description, string picUrl, string url, string toUser)
        {
            List<Article> articles = new List<Article>();
            if (!string.IsNullOrWhiteSpace(picUrl) && picUrl.IndexOf("http") < 0) { picUrl = workSetting.Value.SendMsgHost + picUrl; }
            if (!string.IsNullOrWhiteSpace(url) && url.IndexOf("http") < 0) { url = workSetting.Value.SendMsgHost + url; }
            articles.Add(new Article()
            {
                Title = title,
                Description = description,
                PicUrl = picUrl,
                Url = url
            });
            MassApi.SendNews(workSetting.Value.MsgAccessTokenKey, workSetting.Value.MsgAppId, articles, toUser);
        }
        public bool AddDepartment(Department dep)
        {
            if (env.IsDevelopment())
                return true;
            var reObj = MailListApi.CreateDepartment(workSetting.Value.ContactsAccessTokenKey, dep.Name, Convert.ToInt64(dep.ParentID), dep.Order, Convert.ToInt32(dep.DepartmentID));
            return reObj.errcode == ReturnCode_Work.请求成功;
        }
        public bool AddUser(WorkUser user)
        {
            if (env.IsDevelopment())
                return true;
            var entity = new MemberCreateRequest
            {
                userid = user.UserID,
                name = user.Name,
                mobile = user.Mobile,
                department = new long[] { Convert.ToInt64(user.DepartmentIds) },
                gender = user.Gender.ToString(),
                enable = 1,
            };
            var reObj = MailListApi.CreateMember(workSetting.Value.ContactsAccessTokenKey, entity);
            return reObj.errcode == ReturnCode_Work.请求成功;
        }
        public bool UpdateUser(WorkUser user)
        {
            if (env.IsDevelopment())
                return true;
            var entity = new MemberUpdateRequest
            {
                userid = user.UserID,
                name = user.Name,
                mobile = user.Mobile,
                department = new long[] { Convert.ToInt64(user.DepartmentIds) },
                gender = user.Gender.ToString(),
                enable = 1,
            };
            var reObj = MailListApi.UpdateMember(workSetting.Value.ContactsAccessTokenKey, entity);
            return reObj.errcode == ReturnCode_Work.请求成功;
        }
        public UploadTemporaryResultJson Upload(string path)
        {
            return Senparc.Weixin.Work.AdvancedAPIs.MediaApi.Upload(workSetting.Value.ContactsAccessTokenKey, Senparc.Weixin.Work.UploadMediaFileType.file, path);
        }
        public AsynchronousJobId BatchReplaceUser(string mediaId, Asynchronous_CallBack callbackObj)
        {
            return AsynchronousApi.BatchReplaceUser(workSetting.Value.ContactsAccessTokenKey, mediaId, callbackObj);
        }
        public AsynchronousJobId BatchReplaceParty(string mediaId, Asynchronous_CallBack callbackObj)
        {
            return AsynchronousApi.BatchReplaceParty(workSetting.Value.ContactsAccessTokenKey, mediaId, callbackObj);
        }
        public AsynchronousReplacePartyResult GetReplacePartyResult(string jobid)
        {
            return AsynchronousApi.GetReplacePartyResult(workSetting.Value.ContactsAccessTokenKey, jobid);
        }
        public string GetCode(string urlEncode)
        {
            var code = OAuth2Api.GetCode(workSetting.Value.CorpId, urlEncode, "", workSetting.Value.LoginAppId);
            return code;
        }
        /// <summary>
        /// 根据code获取UserID
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetUserId(string code)
        {
            var token = AccessTokenContainer.GetTokenResult(workSetting.Value.LoginAccessTokenKey);
            var reObj = OAuth2Api.GetUserId(token.access_token, code);
            return reObj.UserId;
        }
        public List<WorkUser> GetDepartmentMemberInfo(long depId, int isAll = 0)
        {
            var reModel = MailListApi.GetDepartmentMemberInfo(workSetting.Value.ContactsAccessTokenKey, depId, isAll);
            return reModel.userlist.Select(s => new WorkUser
            {
                Avatar = s.avatar,
                Name = s.name,
                Mobile = s.mobile,
                UserID = s.userid,
                Gender = s.gender,
                DepartmentIds = string.Join(",", s.department)
            }).ToList();
        }
        public string GetWebHost()
        {
            return workSetting.Value.SendMsgHost;
        }
        /// <summary>
        /// 获取jsdk签名信息
        /// </summary>
        /// <param name="url">页面路径</param>
        /// <returns></returns>
        public object GetWXConfig(string url)
        {
            long timestamp = JSSDKHelper.GetTimestamp();
            string nonce_str = JSSDKHelper.GetNoncestr();
            string ticket = JsApiTicketContainer.TryGetTicket(workSetting.Value.CorpId, workSetting.Value.MsgSecret);
            if (!string.IsNullOrWhiteSpace(url) && url.IndexOf("http") < 0)
                url = workSetting.Value.SendMsgHost + url;
            string signature = JSSDKHelper.GetSignature(ticket, nonce_str, timestamp, url);
            string CorpID = workSetting.Value.CorpId;
            var Data = new
            {
                CorpID,// Corpid
                timestamp,  //时间戳
                nonce_str,  //生成签名的随机串
                signature,  //签名
                ticket
            };
            return Data;
        }
    }
}
