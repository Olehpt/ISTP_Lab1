﻿// <auto-generated />
using System;
using InformationSystemInfrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InformationSystemInfrastructure.Migrations
{
    [DbContext(typeof(ProjectCsContext))]
    partial class ProjectCsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InformationSystemDomain.Model.Article", b =>
                {
                    b.Property<int>("ArticleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ArticleID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArticleId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("Media")
                        .HasColumnType("image");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("PublicationDate")
                        .HasColumnType("date");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int")
                        .HasColumnName("SubjectID");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TypeId")
                        .HasColumnType("int")
                        .HasColumnName("TypeID");

                    b.HasKey("ArticleId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TypeId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("InformationSystemDomain.Model.AuthorsPerArticle", b =>
                {
                    b.Property<int>("LinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("LinkID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LinkId"));

                    b.Property<int>("Article")
                        .HasColumnType("int");

                    b.Property<int>("Authors")
                        .HasColumnType("int");

                    b.HasKey("LinkId");

                    b.HasIndex("Article");

                    b.HasIndex("Authors");

                    b.ToTable("AuthorsPerArticle", (string)null);
                });

            modelBuilder.Entity("InformationSystemDomain.Model.Comment", b =>
                {
                    b.Property<int>("ComId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ComID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ComId"));

                    b.Property<int>("ArticleId")
                        .HasColumnType("int")
                        .HasColumnName("ArticleID");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int")
                        .HasColumnName("AuthorID");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("text");

                    b.Property<DateOnly>("PublicationDate")
                        .HasColumnType("date");

                    b.HasKey("ComId");

                    b.HasIndex("ArticleId");

                    b.HasIndex("AuthorId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("InformationSystemDomain.Model.Organization", b =>
                {
                    b.Property<int>("OrgId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrgID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrgId"));

                    b.Property<string>("Contacts")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("MembersId")
                        .HasColumnType("int")
                        .HasColumnName("MembersID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("OrgId");

                    b.HasIndex("MembersId");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("InformationSystemDomain.Model.PublicationType", b =>
                {
                    b.Property<int>("TypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TypeID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TypeId"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Requirements")
                        .HasColumnType("text");

                    b.HasKey("TypeId");

                    b.ToTable("PublicationTypes");
                });

            modelBuilder.Entity("InformationSystemDomain.Model.Subject", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SubjectID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubjectId"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("SubjectId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("InformationSystemDomain.Model.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<byte[]>("Avatar")
                        .HasColumnType("image");

                    b.Property<string>("Contacts")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Info")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("SignUpDate")
                        .HasColumnType("date");

                    b.HasKey("UserId")
                        .HasName("PK_Authors/Users");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("InformationSystemDomain.Model.Article", b =>
                {
                    b.HasOne("InformationSystemDomain.Model.Subject", "Subject")
                        .WithMany("Articles")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired()
                        .HasConstraintName("FK_Articles_Subjects");

                    b.HasOne("InformationSystemDomain.Model.PublicationType", "Type")
                        .WithMany("Articles")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired()
                        .HasConstraintName("FK_Articles_Types");

                    b.Navigation("Subject");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("InformationSystemDomain.Model.AuthorsPerArticle", b =>
                {
                    b.HasOne("InformationSystemDomain.Model.Article", "ArticleNavigation")
                        .WithMany("AuthorsPerArticles")
                        .HasForeignKey("Article")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired()
                        .HasConstraintName("FK_AuthorsPerArticle_Article");

                    b.HasOne("InformationSystemDomain.Model.User", "AuthorsNavigation")
                        .WithMany("AuthorsPerArticles")
                        .HasForeignKey("Authors")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired()
                        .HasConstraintName("FK_AuthorsPerArticle_Authors");

                    b.Navigation("ArticleNavigation");

                    b.Navigation("AuthorsNavigation");
                });

            modelBuilder.Entity("InformationSystemDomain.Model.Comment", b =>
                {
                    b.HasOne("InformationSystemDomain.Model.Article", "Article")
                        .WithMany("Comments")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired()
                        .HasConstraintName("FK_Comments_Articles");

                    b.HasOne("InformationSystemDomain.Model.User", "Author")
                        .WithMany("Comments")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired()
                        .HasConstraintName("FK_Comments_Authors");

                    b.Navigation("Article");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("InformationSystemDomain.Model.Organization", b =>
                {
                    b.HasOne("InformationSystemDomain.Model.User", "Members")
                        .WithMany("Organizations")
                        .HasForeignKey("MembersId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired()
                        .HasConstraintName("FK_Organizations_Members");

                    b.Navigation("Members");
                });

            modelBuilder.Entity("InformationSystemDomain.Model.Article", b =>
                {
                    b.Navigation("AuthorsPerArticles");

                    b.Navigation("Comments");
                });

            modelBuilder.Entity("InformationSystemDomain.Model.PublicationType", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("InformationSystemDomain.Model.Subject", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("InformationSystemDomain.Model.User", b =>
                {
                    b.Navigation("AuthorsPerArticles");

                    b.Navigation("Comments");

                    b.Navigation("Organizations");
                });
#pragma warning restore 612, 618
        }
    }
}
