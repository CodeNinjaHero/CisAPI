using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace ApiRest.Models;

public partial class cisapidbContext : DbContext
{
    public cisapidbContext()
    {
    }

    public cisapidbContext(DbContextOptions<cisapidbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<category> categories { get; set; }

    public virtual DbSet<comment> comments { get; set; }

    public virtual DbSet<idea> ideas { get; set; }

    public virtual DbSet<user> users { get; set; }

    public virtual DbSet<vote> votes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3307;database=cisapidb;user=edwin;password=edwin123", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.2.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<category>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasIndex(e => e.name, "idx_categories_name").IsUnique();

            entity.Property(e => e.name).HasMaxLength(100);
        });

        modelBuilder.Entity<comment>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasIndex(e => e.created_at, "idx_comments_created_at").IsDescending();

            entity.HasIndex(e => e.idea_id, "idx_comments_idea_id");

            entity.HasIndex(e => e.user_id, "idx_comments_user_id");

            entity.Property(e => e.content).HasColumnType("text");
            entity.Property(e => e.created_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");

            entity.HasOne(d => d.idea).WithMany(p => p.comments)
                .HasForeignKey(d => d.idea_id)
                .HasConstraintName("comments_ibfk_2");

            entity.HasOne(d => d.user).WithMany(p => p.comments)
                .HasForeignKey(d => d.user_id)
                .HasConstraintName("comments_ibfk_1");
        });

        modelBuilder.Entity<idea>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasIndex(e => e.created_at, "idx_ideas_status_created_at").IsDescending();

            entity.HasIndex(e => e.user_id, "idx_ideas_user_id");

            entity.Property(e => e.created_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.description).HasColumnType("text");
            entity.Property(e => e.title).HasMaxLength(255);

            entity.HasOne(d => d.user).WithMany(p => p.ideas)
                .HasForeignKey(d => d.user_id)
                .HasConstraintName("ideas_ibfk_1");

            entity.HasMany(d => d.categories).WithMany(p => p.ideas)
                .UsingEntity<Dictionary<string, object>>(
                    "idea_categorium",
                    r => r.HasOne<category>().WithMany()
                        .HasForeignKey("category_id")
                        .HasConstraintName("idea_categoria_ibfk_2"),
                    l => l.HasOne<idea>().WithMany()
                        .HasForeignKey("idea_id")
                        .HasConstraintName("idea_categoria_ibfk_1"),
                    j =>
                    {
                        j.HasKey("idea_id", "category_id")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("idea_categoria");
                        j.HasIndex(new[] { "category_id" }, "idx_idea_categoria_category_id");
                        j.HasIndex(new[] { "idea_id" }, "idx_idea_categoria_idea_id");
                    });
        });

        modelBuilder.Entity<user>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasIndex(e => e.login, "idx_users_login").IsUnique();

            entity.Property(e => e.name).HasMaxLength(255);
            entity.Property(e => e.password).HasMaxLength(255);
        });

        modelBuilder.Entity<vote>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasIndex(e => e.created_at, "idx_votes_created_at").IsDescending();

            entity.HasIndex(e => e.idea_id, "idx_votes_idea_id");

            entity.HasIndex(e => new { e.user_id, e.idea_id }, "idx_votes_user_idea").IsUnique();

            entity.Property(e => e.created_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.vote_type).HasColumnType("enum('up','down')");

            entity.HasOne(d => d.idea).WithMany(p => p.votes)
                .HasForeignKey(d => d.idea_id)
                .HasConstraintName("votes_ibfk_2");

            entity.HasOne(d => d.user).WithMany(p => p.votes)
                .HasForeignKey(d => d.user_id)
                .HasConstraintName("votes_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
