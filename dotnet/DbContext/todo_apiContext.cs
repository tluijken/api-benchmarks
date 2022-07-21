using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
namespace TodoApi.App.DbContext;

public class TodoApiContext : Microsoft.EntityFrameworkCore.DbContext
{
    public TodoApiContext(DbContextOptions<TodoApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DieselSchemaMigration> DieselSchemaMigrations { get; set; }
    public virtual DbSet<Todo> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            throw new ArgumentException("You dummy");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DieselSchemaMigration>(entity =>
        {
            entity.HasKey(e => e.Version)
                .HasName("__diesel_schema_migrations_pkey");

            entity.ToTable("__diesel_schema_migrations");

            entity.Property(e => e.Version)
                .HasMaxLength(50)
                .HasColumnName("version");

            entity.Property(e => e.RunOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("run_on")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Todo>(entity =>
        {
            ((EntityTypeBuilder)entity).ToTable("todos");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Checked).HasColumnName("checked");

            entity.Property(e => e.Value)
                .IsRequired()
                .HasColumnName("value");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    private static void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        //
    }
}
