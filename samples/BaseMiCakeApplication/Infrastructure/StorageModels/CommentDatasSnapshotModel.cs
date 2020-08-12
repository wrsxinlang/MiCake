using BaseMiCakeApplication.Controllers.Comman;
using BaseMiCakeApplication.Domain.Aggregates.Idea;
using MiCake.Audit.SoftDeletion;
using MiCake.DDD.Extensions.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.Infrastructure.StorageModels
{
    public class CommentDatasSnapshotModel : PersistentObject<Guid, CommentDatas, CommentDatasSnapshotModel>, IHasAuditWithSoftDeletion
    {
        /// <summary>
        /// 是关于什么的评论
        /// </summary>
        public Guid RelationObjectID { get; set; }

        /// <summary>
        /// 回复类型
        /// </summary>
        public CY_Classify ReplayType { get; set; }

        /// <summary>
        /// 那个用户创建的
        /// </summary>
        public Guid CreateUserObjectID { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        public string ReplyContent { get; set; }

        /// <summary>
        /// 回复的谁
        /// </summary>
        public Guid? ReplyToUserObjectID { get; set; }

        /// <summary>
        /// null:表示顶级评论;否则表示：二级评论
        /// </summary>
        public Guid? ParentID { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? ModificationTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionTime { get; set; }

        public override void ConfigureMapping()
        {
            MapConfiger.MapProperty(d => d.Id, s => s.Id);
        }
    }
}
