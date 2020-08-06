using BaseMiCakeApplication.Domain.Events.IdeaEvents;
using MiCake.Audit;
using MiCake.DDD.Domain;
using MiCake.DDD.Domain.Store;
using System;

namespace BaseMiCakeApplication.Domain.Aggregates.Idea
{
    public class NewIdea : AggregateRootHasPersistentObject<Guid>, IHasCreationTime, IHasModificationTime
    {
        /// <summary>
        /// 简介
        /// </summary>
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
        public string Title { get; set; }

        /// <summary>
        /// 属于哪个作品集
        /// </summary>
        public Guid? WorksID { get; set; }

        /// <summary>
        /// 是否用户编辑更改
        /// </summary>
        public bool IsOtherEdit { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public Checker Checker { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? CheckedTime { get; set; }
        public DateTime? ModificationTime { get; set; }
        public DateTime CreationTime { get; set; }

        public Guid CreateUserID { get; set; }

        public NewIdea()
        {

        }

        public NewIdea(string title, string introduce, string graphic,Guid userID)
        {
            Id = Guid.NewGuid();
            Title = title;
            Introduce = introduce;
            Graphic = graphic;
            CreateUserID = userID;
            CreationTime = DateTime.Now;
        }

        public void RegisterCommand(Guid id)
        {
            AddDomainEvent(new AddIdenEvent(id));
        }

        public void UpdateCommand(Guid id)
        {
            UpdateDomainEvent(new UpdateIdenEvent(id));
        }

    }

    public class Checker : ValueObject
    {
        public Guid CheckerID { get; set; }
        public string CheckerName { get; set; }
    }
}
