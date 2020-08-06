using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.WeChat
{
    public class Department
    {
        public string Name { get; set; }
        public bool ParentID { get; internal set; }
        public int Order { get; internal set; }
        public bool DepartmentID { get; internal set; }
    }
}
