using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using ApiRest.Domain.Models;

namespace ApiRest.DataAccess.Context;

public partial class CisapidbContext : DbContext
{
    public CisapidbContext()
    {
    }

    public CisapidbContext(DbContextOptions<CisapidbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Idea> Ideas { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vote> Votes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3307;database=cisapidb;user=edwin;password=edwin123", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.2.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("categories");

            entity.HasIndex(e => e.Name, "idx_categories_name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("comments");

            entity.HasIndex(e => e.CreatedAt, "idx_comments_created_at").IsDescending();

            entity.HasIndex(e => e.IdeaId, "idx_comments_idea_id");

            entity.HasIndex(e => e.UserId, "idx_comments_user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.IdeaId).HasColumnName("idea_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Idea).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdeaId)
                .HasConstraintName("comments_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("comments_ibfk_1");
        });

        modelBuilder.Entity<Idea>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ideas");

            entity.HasIndex(e => e.CreatedAt, "idx_ideas_status_created_at").IsDescending();

            entity.HasIndex(e => e.UserId, "idx_ideas_user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Ideas)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("ideas_ibfk_1");

            entity.HasMany(d => d.Categories).WithMany(p => p.Ideas)
                .UsingEntity<Dictionary<string, object>>(
                    "IdeaCategorium",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("idea_categoria_ibfk_2"),
                    l => l.HasOne<Idea>().WithMany()
                        .HasForeignKey("IdeaId")
                        .HasConstraintName("idea_categoria_ibfk_1"),
                    j =>
                    {
                        j.HasKey("IdeaId", "CategoryId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("idea_categoria");
                        j.HasIndex(new[] { "CategoryId" }, "idx_idea_categoria_category_id");
                        j.HasIndex(new[] { "IdeaId" }, "idx_idea_categoria_idea_id");
                        j.IndexerProperty<Guid>("IdeaId").HasColumnName("idea_id");
                        j.IndexerProperty<Guid>("CategoryId").HasColumnName("category_id");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Login, "idx_users_login").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Login).HasColumnName("login");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Vote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("votes");

            entity.HasIndex(e => e.CreatedAt, "idx_votes_created_at").IsDescending();

            entity.HasIndex(e => e.IdeaId, "idx_votes_idea_id");

            entity.HasIndex(e => new { e.UserId, e.IdeaId }, "idx_votes_user_idea").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.IdeaId).HasColumnName("idea_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VoteType)
                .HasColumnType("enum('up','down')")
                .HasColumnName("vote_type");

            entity.HasOne(d => d.Idea).WithMany(p => p.Votes)
                .HasForeignKey(d => d.IdeaId)
                .HasConstraintName("votes_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Votes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("votes_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
