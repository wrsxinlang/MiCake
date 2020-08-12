using BaseMiCakeApplication.Domain.Aggregates;
using BaseMiCakeApplication.Domain.Aggregates.Account;
using BaseMiCakeApplication.Domain.Aggregates.Idea;
using BaseMiCakeApplication.Infrastructure.StorageModels;
using BaseMiCakeApplication.Infrastructure.StroageModels;
using MiCake.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BaseMiCakeApplication.EFCore
{
    public class BaseAppDbContext : MiCakeDbContext
    {
        public BaseAppDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<ItinerarySnapshotModel> Itinerary { get; set; }
        public virtual DbSet<Book> Books { get; set; }

        /// <summary>
        /// 用户表
        /// </summary>
        public virtual DbSet<User> Sys_User { get; set; }

        /// <summary>
        /// 上传文件表
        /// </summary>
        public virtual DbSet<FileObject> Sys_File { get; set; }

        /// <summary>
        /// 创意表
        /// </summary>
        public virtual DbSet<NewIdeaSnapshotModel> Mlcy_NewIdeas { get; set; }

        /// <summary>
        /// 评论表
        /// </summary>
        public virtual DbSet<CommentDatasSnapshotModel> Mlcy_Comments { get; set; }

        /// <summary>
        /// 微信登录表
        /// </summary>
        public virtual DbSet<UserWithWechat> Sys_UserWechat { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .OwnsOne(s => s.Author);
        }
    }
}
