using CisApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;


namespace CisApi.DataAccess.Context;

public class CisDbContext : DbContext
{
    public CisDbContext() { }

    public CisDbContext(DbContextOptions<CisDbContext> options) : base(options) { }


    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<IdeaEntity> Ideas { get; set; }
    public DbSet<IdeaCategory> IdeaCategories { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<VoteEntity> Votes { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdeaCategory>()
            .HasKey(ic => new { ic.IdeaId, ic.CategoryId }); 

        base.OnModelCreating(modelBuilder);
    
    }
}