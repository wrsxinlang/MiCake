using BaseMiCakeApplication.Domain.Aggregates;
using BaseMiCakeApplication.Domain.Aggregates.Idea;
using MiCake.Audit.SoftDeletion;
using MiCake.DDD.Extensions.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.Infrastructure.StorageModels
{
    public class NewIdeaSnapshotModel : PersistentObject<Guid, NewIdea, NewIdeaSnapshotModel>, IHasAuditWithSoftDeletion
    {
        /// <summary>
        /// 简介
        /// </summary>
        [MaxLength(250)]
        public string Introduce { get; set; }

        /// <summary>
        /// 图文详情
        /// </summary>
        public string Graphic { get; set; }

        /// <summary>
        /// 关联版本ID
        /// </summary>
        public Guid RelationId { get; set; }

        /// <summary>
        /// 封面图
        /// </summary>
        public FileObject CoverImg { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [MaxLength(100)]
        public string Title { get; set; }

        /// <summary>
        /// 属于哪个作品集
        /// </summary>
        public Guid? WorksID { get; set; }


        /// <summary>
        /// 评论量
        /// </summary>
        public int CommentCount { get; set; }

        /// <summary>
        /// 浏览量
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// 收藏量
        /// </summary>
        public int PublishCount { get; set; }

        /// <summary>
        /// 是否用户编辑更改
        /// </summary>
        public bool IsOtherEdit { get; set; }

        public Guid CreateUserID { get; set; }

        public string Remark { get; set; }


        /// <summary>
        /// 审核人
        /// </summary>
        public Checker Checker { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? ModificationTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionTime { get; set; }

        public Guid CheckerID { get; set; }
        public string CheckerName { get; set; }
        public string ChecherAct { get; set; }

        public bool IsChecked { get; set; }

        public override void ConfigureMapping()
        {
            MapConfiger.MapProperty(d => d.Checker.CheckerName, s => s.CheckerName)
                       .MapProperty(d => d.Checker.CheckerID, s => s.CheckerID)
                       .MapProperty(d => d.Checker.ChecherAct, s => s.ChecherAct)
                       .MapProperty(d => d.Id, s => s.Id)
                       .MapProperty(d => d.Title, s => s.Title)
                       .MapProperty(d => d.CommentCount, s => s.CommentCount)
                       .MapProperty(d => d.ViewCount, s => s.ViewCount)
                       .MapProperty(d => d.PublishCount, s => s.PublishCount)
                       .MapProperty(d => d.CreateUserID, s => s.CreateUserID)
                       .MapProperty(d => d.Remark, s => s.Remark)
                       .MapProperty(d => d.IsChecked, s => s.IsChecked);
        }
    }
}
