using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.WeChat
{
    public interface IWorkApi
    {
        void SendMsg(string toUser, string toParty, string title, string content, string url, string btntxt = null);
        void SendTextMsg(string content, string toUser = null, string toParty = null);
        string GetCode(string urlEncode);
        string GetUserId(string code);
        List<WorkUser> GetDepartmentMemberInfo(long depId, int isAll = 0);
        string GetWebHost();
        /// <summary>
        /// 获取jsdk签名信息
        /// </summary>
        /// <param name="url">页面路径</param>
        /// <returns></returns>
        object GetWXConfig(string url);
        void SendNew(string title, string description, string picUrl, string url, string toUser);
    }

}
