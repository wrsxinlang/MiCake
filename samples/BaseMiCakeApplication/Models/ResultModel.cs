using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.Models
{
    public class ResultModel
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public int Count { get; set; }
        public object Data { get; set; }
        public ResultModel() { }

        public ResultModel(object data) 
        {
            this.Data = data;
        }

        public ResultModel(int count, object data)
        {
            this.Count = count;
            this.Data = data;
        }
        public ResultModel(int code, string msg, object data)
        {
            this.Code = code;
            this.Msg = msg;
            this.Data = data;
        }
        public ResultModel(int code, int count, object data)
        {
            this.Code = code;
            this.Count = count;
            this.Data = data;
        }
    }
}
