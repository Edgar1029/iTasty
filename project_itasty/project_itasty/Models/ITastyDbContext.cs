using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace project_itasty.Models;

public partial class ITastyDbContext : DbContext
{
    public ITastyDbContext()
    {
    }

    public ITastyDbContext(DbContextOptions<ITastyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<UserFollower> UserFollowers { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.;Database=iTastyDB;Integrated Security=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserFollower>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.FollowerId }).HasName("PK__user_fol__CE0DEA5237BDE189");

            entity.ToTable("user_follower");

            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.FollowerId).HasColumnName("followerID");
            entity.Property(e => e.FollowDate)
                .HasColumnType("datetime")
                .HasColumnName("followDate");
            entity.Property(e => e.UnfollowDate)
                .HasColumnType("datetime")
                .HasColumnName("unfollowDate");

            entity.HasOne(d => d.Follower).WithMany(p => p.UserFollowerFollowers)
                .HasForeignKey(d => d.FollowerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_foll__follo__3A81B327");

            entity.HasOne(d => d.User).WithMany(p => p.UserFollowerUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_foll__userI__398D8EEE");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_inf__3213E83F470D43CA");

            entity.ToTable("user_info");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.Photo).HasColumnName("photo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
