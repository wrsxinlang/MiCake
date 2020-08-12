using BaseMiCakeApplication.Controllers.Comman;
using MiCake.Audit;
using MiCake.DDD.Domain.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.Domain.Aggregates.Idea
{
    public class CommentDatas : AggregateRootHasPersistentObject<Guid>, IHasCreationTime
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
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

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

        public CommentDatas()
        {

        }

        public CommentDatas(Guid relationObjectID,int replyType,Guid createUserId,string replyContent,Guid? replyUserId, Guid? parentID)
        {
            Id = Guid.NewGuid();
            RelationObjectID = relationObjectID;
            ReplayType = (CY_Classify)replyType;
            CreationTime = DateTime.Now;
            CreateUserObjectID = createUserId;
            ReplyContent = replyContent;
            ReplyToUserObjectID = replyUserId;
            ParentID = parentID;
        }

        //TODO:添加消息审核事件
    }
}
