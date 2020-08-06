using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.WeChat
{
    public class MpSetting : IOptions<MpSetting>
    {
        public MpSetting Value => this;
        public string AppID { get; set; }
        public string AppSecret { get; set; }
        public string MchID { get; set; }
        public string ParentAppID { get; set; }
        public string ParentMchID { get; set; }
        public string Key { get; set; }
        public string NotifyUrl { get; set; }
    }
}
