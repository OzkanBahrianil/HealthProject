﻿// <auto-generated />
using System;
using DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20220414184511_mig_writer_fix_identity")]
    partial class mig_writer_fix_identity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EntityLayer.Concrate.About", b =>
                {
                    b.Property<int>("AboutID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AboutDetails1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AboutDetails2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AboutImage1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AboutImage2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AboutMapLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("AboutID");

                    b.ToTable("Abouts");
                });

            modelBuilder.Entity("EntityLayer.Concrate.Admin", b =>
                {
                    b.Property<int>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdminImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminRole")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminShortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminUsername")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("EntityLayer.Concrate.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("EntityLayer.Concrate.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("About")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NameSurname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("VideoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("EntityLayer.Concrate.ArticleCategory", b =>
                {
                    b.Property<int>("ArticleCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArticleCategoryDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ArticleCategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ArticleCategoryStatus")
                        .HasColumnType("bit");

                    b.HasKey("ArticleCategoryID");

                    b.ToTable("ArticleCategories");
                });

            modelBuilder.Entity("EntityLayer.Concrate.Articles", b =>
                {
                    b.Property<int>("ArticlesID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AppUserId")
                        .HasColumnType("int");

                    b.Property<int>("ArticleCategoryID")
                        .HasColumnType("int");

                    b.Property<string>("ArticlesContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ArticlesPdf")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ArticlesPublishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ArticlesShortContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ArticlesStatus")
                        .HasColumnType("bit");

                    b.Property<string>("ArticlesTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ArticlesType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ArticlesWritersName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ArticlesID");

                    b.HasIndex("AppUserId");

                    b.HasIndex("ArticleCategoryID");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("EntityLayer.Concrate.Blog", b =>
                {
                    b.Property<int>("BlogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AppUserId")
                        .HasColumnType("int");

                    b.Property<string>("BlogContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BlogCreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("BlogImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BlogShortContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("BlogStatus")
                        .HasColumnType("bit");

                    b.Property<string>("BlogThumbnailImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BlogTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int?>("WriterId")
                        .HasColumnType("int");

                    b.HasKey("BlogID");

                    b.HasIndex("AppUserId");

                    b.HasIndex("CategoryID");

                    b.HasIndex("WriterId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("EntityLayer.Concrate.BlogRayting", b =>
                {
                    b.Property<int>("BlogRaytingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BlogID")
                        .HasColumnType("int");

                    b.Property<int>("BlogRaytingCount")
                        .HasColumnType("int");

                    b.Property<int>("BlogTotalScore")
                        .HasColumnType("int");

                    b.HasKey("BlogRaytingID");

                    b.ToTable("BlogRaytings");
                });

            modelBuilder.Entity("EntityLayer.Concrate.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("CategoryStatus")
                        .HasColumnType("bit");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("EntityLayer.Concrate.Comment", b =>
                {
                    b.Property<int>("CommentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BlogID")
                        .HasColumnType("int");

                    b.Property<int>("BlogScore")
                        .HasColumnType("int");

                    b.Property<string>("CommentContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CommentDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("CommentStatus")
                        .HasColumnType("bit");

                    b.Property<string>("CommentTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommentUserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CommentID");

                    b.HasIndex("BlogID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("EntityLayer.Concrate.CommentProduct", b =>
                {
                    b.Property<int>("CommentProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CommentProductContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CommentProductDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("CommentProductStatus")
                        .HasColumnType("bit");

                    b.Property<string>("CommentProductTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommentProductUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MedicalProductProductID")
                        .HasColumnType("int");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("CommentProductID");

                    b.HasIndex("MedicalProductProductID");

                    b.ToTable("CommentProduct");
                });

            modelBuilder.Entity("EntityLayer.Concrate.Contact", b =>
                {
                    b.Property<int>("ContactID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ContactDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ContactMail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ContactStatus")
                        .HasColumnType("bit");

                    b.Property<string>("ContactSubject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactUserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContactID");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("EntityLayer.Concrate.MedicalProduct", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AppUserId")
                        .HasColumnType("int");

                    b.Property<int>("ProductCategoryID")
                        .HasColumnType("int");

                    b.Property<string>("ProductCompanyWebsite")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ProductRealiseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductShortContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ProductStatus")
                        .HasColumnType("bit");

                    b.Property<string>("ProductStyle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductThumbnailImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int?>("WriterId")
                        .HasColumnType("int");

                    b.HasKey("ProductID");

                    b.HasIndex("AppUserId");

                    b.HasIndex("ProductCategoryID");

                    b.HasIndex("WriterId");

                    b.ToTable("MedicalProducts");
                });

            modelBuilder.Entity("EntityLayer.Concrate.Message", b =>
                {
                    b.Property<int>("MessageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("MessageDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("MessageDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("MessageStatus")
                        .HasColumnType("bit");

                    b.Property<int?>("ReceiverID")
                        .HasColumnType("int");

                    b.Property<int?>("SenderID")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WriterId")
                        .HasColumnType("int");

                    b.Property<int?>("WriterId1")
                        .HasColumnType("int");

                    b.HasKey("MessageID");

                    b.HasIndex("ReceiverID");

                    b.HasIndex("SenderID");

                    b.HasIndex("WriterId");

                    b.HasIndex("WriterId1");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("EntityLayer.Concrate.NewsLetter", b =>
                {
                    b.Property<int>("MailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Mail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("MailStatus")
                        .HasColumnType("bit");

                    b.HasKey("MailID");

                    b.ToTable("NewsLetters");
                });

            modelBuilder.Entity("EntityLayer.Concrate.Notification", b =>
                {
                    b.Property<int>("NotificationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NotificationColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NotificationDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("NotificationStatus")
                        .HasColumnType("bit");

                    b.Property<DateTime>("NotificationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("NotificationType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NotificationTypeSymbol")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NotificationID");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("EntityLayer.Concrate.Presentation", b =>
                {
                    b.Property<int>("PresentationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PresentationDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PresentationImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PresentationSendUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PresentationStatus")
                        .HasColumnType("bit");

                    b.Property<string>("PresentationTitle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PresentationID");

                    b.ToTable("Presentations");
                });

            modelBuilder.Entity("EntityLayer.Concrate.ProductCategory", b =>
                {
                    b.Property<int>("ProductCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProductCategoryDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductCategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ProductCategoryStatus")
                        .HasColumnType("bit");

                    b.HasKey("ProductCategoryID");

                    b.ToTable("ProductCategory");
                });

            modelBuilder.Entity("EntityLayer.Concrate.Writer", b =>
                {
                    b.Property<int>("WriterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("WriterAbout")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WriterImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WriterLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WriterMail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WriterName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WriterPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("WriterStatus")
                        .HasColumnType("bit");

                    b.Property<string>("WriterVideoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WriterId");

                    b.ToTable("Writers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("EntityLayer.Concrate.Articles", b =>
                {
                    b.HasOne("EntityLayer.Concrate.AppUser", "AppUser")
                        .WithMany("Articles")
                        .HasForeignKey("AppUserId");

                    b.HasOne("EntityLayer.Concrate.ArticleCategory", "ArticleCategory")
                        .WithMany("Articles")
                        .HasForeignKey("ArticleCategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("ArticleCategory");
                });

            modelBuilder.Entity("EntityLayer.Concrate.Blog", b =>
                {
                    b.HasOne("EntityLayer.Concrate.AppUser", "AppUser")
                        .WithMany("Blogs")
                        .HasForeignKey("AppUserId");

                    b.HasOne("EntityLayer.Concrate.Category", "Category")
                        .WithMany("Blogs")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityLayer.Concrate.Writer", null)
                        .WithMany("Blogs")
                        .HasForeignKey("WriterId");

                    b.Navigation("AppUser");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("EntityLayer.Concrate.Comment", b =>
                {
                    b.HasOne("EntityLayer.Concrate.Blog", "Blog")
                        .WithMany("Comments")
                        .HasForeignKey("BlogID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Blog");
                });

            modelBuilder.Entity("EntityLayer.Concrate.CommentProduct", b =>
                {
                    b.HasOne("EntityLayer.Concrate.MedicalProduct", "MedicalProduct")
                        .WithMany("CommentProducts")
                        .HasForeignKey("MedicalProductProductID");

                    b.Navigation("MedicalProduct");
                });

            modelBuilder.Entity("EntityLayer.Concrate.MedicalProduct", b =>
                {
                    b.HasOne("EntityLayer.Concrate.AppUser", "AppUser")
                        .WithMany("MedicalProduct")
                        .HasForeignKey("AppUserId");

                    b.HasOne("EntityLayer.Concrate.ProductCategory", "ProductCategory")
                        .WithMany("MedicalProducts")
                        .HasForeignKey("ProductCategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityLayer.Concrate.Writer", null)
                        .WithMany("MedicalProduct")
                        .HasForeignKey("WriterId");

                    b.Navigation("AppUser");

                    b.Navigation("ProductCategory");
                });

            modelBuilder.Entity("EntityLayer.Concrate.Message", b =>
                {
                    b.HasOne("EntityLayer.Concrate.AppUser", "ReceiverUser")
                        .WithMany("WriterReceiver")
                        .HasForeignKey("ReceiverID");

                    b.HasOne("EntityLayer.Concrate.AppUser", "SenderUser")
                        .WithMany("WriterSender")
                        .HasForeignKey("SenderID");

                    b.HasOne("EntityLayer.Concrate.Writer", null)
                        .WithMany("WriterReceiver")
                        .HasForeignKey("WriterId");

                    b.HasOne("EntityLayer.Concrate.Writer", null)
                        .WithMany("WriterSender")
                        .HasForeignKey("WriterId1");

                    b.Navigation("ReceiverUser");

                    b.Navigation("SenderUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("EntityLayer.Concrate.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("EntityLayer.Concrate.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("EntityLayer.Concrate.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("EntityLayer.Concrate.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityLayer.Concrate.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("EntityLayer.Concrate.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EntityLayer.Concrate.AppUser", b =>
                {
                    b.Navigation("Articles");

                    b.Navigation("Blogs");

                    b.Navigation("MedicalProduct");

                    b.Navigation("WriterReceiver");

                    b.Navigation("WriterSender");
                });

            modelBuilder.Entity("EntityLayer.Concrate.ArticleCategory", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("EntityLayer.Concrate.Blog", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("EntityLayer.Concrate.Category", b =>
                {
                    b.Navigation("Blogs");
                });

            modelBuilder.Entity("EntityLayer.Concrate.MedicalProduct", b =>
                {
                    b.Navigation("CommentProducts");
                });

            modelBuilder.Entity("EntityLayer.Concrate.ProductCategory", b =>
                {
                    b.Navigation("MedicalProducts");
                });

            modelBuilder.Entity("EntityLayer.Concrate.Writer", b =>
                {
                    b.Navigation("Blogs");

                    b.Navigation("MedicalProduct");

                    b.Navigation("WriterReceiver");

                    b.Navigation("WriterSender");
                });
#pragma warning restore 612, 618
        }
    }
}
