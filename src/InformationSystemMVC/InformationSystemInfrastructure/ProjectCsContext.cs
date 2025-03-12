using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using InformationSystemDomain.Model;

namespace InformationSystemInfrastructure;

public partial class ProjectCsContext : DbContext
{
    public ProjectCsContext()
    {
    }

    public ProjectCsContext(DbContextOptions<ProjectCsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<AuthorsPerArticle> AuthorsPerArticles { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<PublicationType> PublicationTypes { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=User-pk\\SQLEXPRESS; Database=ProjectCS; Trusted_Connection=True; TrustServerCertificate=True; ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.Property(e => e.ArticleId).HasColumnName("ArticleID");
            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.Media).HasColumnType("image");
            entity.Property(e => e.Name).HasColumnType("text");
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.Topic).HasColumnType("text");
            entity.Property(e => e.TypeId).HasColumnName("TypeID");

            entity.HasOne(d => d.Subject).WithMany(p => p.Articles)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_Articles_Subjects");

            entity.HasOne(d => d.Type).WithMany(p => p.Articles)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_Articles_Types");
        });

        modelBuilder.Entity<AuthorsPerArticle>(entity =>
        {
            entity.HasKey(e => e.LinkId);

            entity.ToTable("AuthorsPerArticle");

            entity.Property(e => e.LinkId)
                .ValueGeneratedOnAdd()
                .HasColumnName("LinkID");

            entity.HasOne(d => d.ArticleNavigation).WithMany(p => p.AuthorsPerArticles)
                .HasForeignKey(d => d.Article)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_AuthorsPerArticle_Article");

            entity.HasOne(d => d.AuthorsNavigation).WithMany(p => p.AuthorsPerArticles)
                .HasForeignKey(d => d.Authors)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_AuthorsPerArticle_Authors");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.ComId);

            entity.Property(e => e.ComId).HasColumnName("ComID");
            entity.Property(e => e.ArticleId).HasColumnName("ArticleID");
            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.Content).HasColumnType("text");

            entity.HasOne(d => d.Article).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_Comments_Articles");

            entity.HasOne(d => d.Author).WithMany(p => p.Comments)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_Comments_Authors");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.OrgId);

            entity.Property(e => e.OrgId).HasColumnName("OrgID");
            entity.Property(e => e.Contacts).HasColumnType("text");
            entity.Property(e => e.Country).HasColumnType("text");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.MembersId).HasColumnName("MembersID");
            entity.Property(e => e.Name).HasColumnType("text");

            entity.HasOne(d => d.Members).WithMany(p => p.Organizations)
                .HasForeignKey(d => d.MembersId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_Organizations_Members");
        });

        modelBuilder.Entity<PublicationType>(entity =>
        {
            entity.HasKey(e => e.TypeId);

            entity.Property(e => e.TypeId).HasColumnName("TypeID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name).HasColumnType("text");
            entity.Property(e => e.Requirements).HasColumnType("text");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name).HasColumnType("text");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_Authors/Users");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Avatar).HasColumnType("image");
            entity.Property(e => e.Contacts).HasColumnType("text");
            entity.Property(e => e.Email).HasColumnType("text");
            entity.Property(e => e.Info).HasColumnType("text");
            entity.Property(e => e.Name).HasColumnType("text");
            entity.Property(e => e.Password).HasColumnType("text");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
