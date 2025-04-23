using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public partial class TheNetherWorldContext : DbContext
{
    public TheNetherWorldContext()
    {
    }

    public TheNetherWorldContext(DbContextOptions<TheNetherWorldContext> options)
        : base(options)
    {
    }

    public virtual DbSet<JudgmentInfoTable> JudgmentInfoTables { get; set; }

    public virtual DbSet<LogoutTable> LogoutTables { get; set; }

    public virtual DbSet<OperationLog> OperationLogs { get; set; }

    public virtual DbSet<Operator> Operators { get; set; }

    public virtual DbSet<RegistrationTable> RegistrationTables { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=TheNetherWorld; TrustServerCertificate=true;User ID=sa;Password=123456");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JudgmentInfoTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Judgment__3214EC077B307C51");

            entity.ToTable("JudgmentInfoTable");

            entity.Property(e => e.BirthTime).HasColumnType("datetime");
            entity.Property(e => e.DeathCause).HasMaxLength(255);
            entity.Property(e => e.DeathTime).HasColumnType("datetime");
            entity.Property(e => e.JudgmentTime).HasColumnType("datetime");
            entity.Property(e => e.Lifespan).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Sex).HasMaxLength(10);

            entity.HasOne(d => d.UserInfo).WithMany(p => p.JudgmentInfoTables)
                .HasForeignKey(d => d.UserInfoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JudgmentInfoTable_UserInfo");
        });

        modelBuilder.Entity<LogoutTable>(entity =>
        {
            entity.ToTable("LogoutTable");

            entity.Property(e => e.LogoutTime).HasColumnType("datetime");

            entity.HasOne(d => d.UserInfo).WithMany(p => p.LogoutTables)
                .HasForeignKey(d => d.UserInfoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogoutTable_UserInfo");
        });

        modelBuilder.Entity<OperationLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Operatio__3214EC07A6F0918B");

            entity.ToTable("OperationLog");

            entity.Property(e => e.Info).HasMaxLength(200);
            entity.Property(e => e.OperationTime).HasColumnType("datetime");

            entity.HasOne(d => d.Operator).WithMany(p => p.OperationLogs)
                .HasForeignKey(d => d.OperatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OperationLog_Operation");
        });

        modelBuilder.Entity<Operator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Operator__3214EC072A64A941");

            entity.ToTable("Operator");

            entity.HasIndex(e => e.Name, "UQ_OperatorName").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Operators)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Operator_Role");
        });

        modelBuilder.Entity<RegistrationTable>(entity =>
        {
            entity.ToTable("RegistrationTable");

            entity.Property(e => e.RegistrationTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.UserInfo).WithMany(p => p.RegistrationTables)
                .HasForeignKey(d => d.UserInfoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RegistrationTable_UserInfo");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07B40B7550");

            entity.ToTable("Role");

            entity.HasIndex(e => e.Name, "UQ_RoleName").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.ToTable("UserInfo");

            entity.Property(e => e.JudgmentInfoState).HasDefaultValue(0);
            entity.Property(e => e.LogoutState).HasDefaultValue(0);
            entity.Property(e => e.RegistrationState).HasDefaultValue(0);
            entity.Property(e => e.UniqueCode).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
