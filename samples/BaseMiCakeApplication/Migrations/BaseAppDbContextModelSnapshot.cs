﻿// <auto-generated />
using System;
using BaseMiCakeApplication.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BaseMiCakeApplication.Migrations
{
    [DbContext(typeof(BaseAppDbContext))]
    partial class BaseAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BaseMiCakeApplication.Domain.Aggregates.Account.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("ModificationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Reputation")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Sys_User");
                });

            modelBuilder.Entity("BaseMiCakeApplication.Domain.Aggregates.Account.UserWithWechat", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<Guid>("UserID")
                        .HasColumnType("char(36)");

                    b.Property<string>("WeChatOpenID")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Sys_UserWechat");
                });

            modelBuilder.Entity("BaseMiCakeApplication.Domain.Aggregates.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("BookName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BaseMiCakeApplication.Domain.Aggregates.FileObject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("FileExtention")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("FileLength")
                        .HasColumnType("bigint");

                    b.Property<int>("FileType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Url")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Sys_File");
                });

            modelBuilder.Entity("BaseMiCakeApplication.Domain.Aggregates.Idea.Checker", b =>
                {
                    b.Property<Guid>("CheckerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ChecherAct")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CheckerName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("CheckerID");

                    b.ToTable("Checker");
                });

            modelBuilder.Entity("BaseMiCakeApplication.Infrastructure.StorageModels.NewIdeaSnapshotModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CheckedMsg")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid?>("CheckerID")
                        .HasColumnType("char(36)");

                    b.Property<int>("CommentCount")
                        .HasColumnType("int");

                    b.Property<Guid?>("CoverImgId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CreateUserID")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletionTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Graphic")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Introduce")
                        .HasColumnType("varchar(250) CHARACTER SET utf8mb4")
                        .HasMaxLength(250);

                    b.Property<bool>("IsChecked")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsOtherEdit")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("ModificationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PublishCount")
                        .HasColumnType("int");

                    b.Property<Guid>("RelationId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Remark")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Title")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<int>("ViewCount")
                        .HasColumnType("int");

                    b.Property<Guid?>("WorksID")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("CheckerID");

                    b.HasIndex("CoverImgId");

                    b.ToTable("Mlcy_NewIdeas");
                });

            modelBuilder.Entity("BaseMiCakeApplication.Infrastructure.StroageModels.ItinerarySnapshotModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Content")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletionTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("ModificationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("NoteTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Itinerary");
                });

            modelBuilder.Entity("BaseMiCakeApplication.Domain.Aggregates.Book", b =>
                {
                    b.OwnsOne("BaseMiCakeApplication.Domain.Aggregates.BookAuthor", "Author", b1 =>
                        {
                            b1.Property<Guid>("BookId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("FirstName")
                                .HasColumnType("longtext CHARACTER SET utf8mb4");

                            b1.Property<string>("LastName")
                                .HasColumnType("longtext CHARACTER SET utf8mb4");

                            b1.HasKey("BookId");

                            b1.ToTable("Books");

                            b1.WithOwner()
                                .HasForeignKey("BookId");
                        });
                });

            modelBuilder.Entity("BaseMiCakeApplication.Infrastructure.StorageModels.NewIdeaSnapshotModel", b =>
                {
                    b.HasOne("BaseMiCakeApplication.Domain.Aggregates.Idea.Checker", "Checker")
                        .WithMany()
                        .HasForeignKey("CheckerID");

                    b.HasOne("BaseMiCakeApplication.Domain.Aggregates.FileObject", "CoverImg")
                        .WithMany()
                        .HasForeignKey("CoverImgId");
                });
#pragma warning restore 612, 618
        }
    }
}
