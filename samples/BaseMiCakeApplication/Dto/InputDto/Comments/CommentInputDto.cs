using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.Dto.InputDto.Comments
{
    public class CommentInputDto
    {
        /// <summary>
        /// 是关于什么的评论
        /// </summary>
        [Required]
        public Guid RelationObjectID { get; set; }

        /// <summary>
        /// 那个用户创建的
        /// </summary>
        public Guid CreateUserObjectID { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        [Required]
        public string ReplyContent { get; set; }

        /// <summary>
        /// 回复的谁
        /// </summary>
        public Guid? ReplyToUserObjectID { get; set; }

        /// <summary>
        /// null:表示顶级评论;否则表示：二级评论
        /// </summary>
        public Guid? ParentID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreationTime { get; set; }
    }
}
