using BaseMiCakeApplication.Controllers.Comman;
using MiCake.DDD.Domain;
using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.Domain.Aggregates
{
    public class FileObject: AggregateRoot<Guid>
    {
        /// <summary>
        /// 文件地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 文件长度
        /// </summary>
        public long FileLength { get; set; }

        /// <summary>
        /// 文件后缀名
        /// </summary>
        public string FileExtention { get; set; }


        public FileEnum FileType { get; set; }
    }
}
