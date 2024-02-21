using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Ilnar123.Models;

public partial class _123Context : DbContext
{
    public _123Context()
    {
    }

    public _123Context(DbContextOptions<_123Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Boy> Boys { get; set; }

    public virtual DbSet<Girl> Girls { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;User=root;Database=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Boy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("boy");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Age)
                .HasMaxLength(100)
                .HasColumnName("age");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Patronymic)
                .HasMaxLength(100)
                .HasColumnName("patronymic");
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .HasColumnName("surname");
        });

        modelBuilder.Entity<Girl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("girls");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Age)
                .HasMaxLength(100)
                .HasColumnName("age");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Patronymic)
                .HasMaxLength(100)
                .HasColumnName("patronymic");
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .HasColumnName("surname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
