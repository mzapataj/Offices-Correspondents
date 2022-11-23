using System;
using System.Collections.Generic;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Prueba.WebServices.Repository
{
    public partial class Prueba_ControlBoxContext : DbContext
    {
        public Prueba_ControlBoxContext()
        {
        }

        public Prueba_ControlBoxContext(DbContextOptions<Prueba_ControlBoxContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Corresponsal> Corresponsales { get; set; } = null!;
        public virtual DbSet<Oficina> Oficinas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Corresponsal>(entity =>
            {
                entity.HasKey(e => e.CorCorresponsalId)
                    .HasName("PK__CORRESPO__58307051DC1A41F0");

                entity.ToTable("CORRESPONSALES");

                entity.Property(e => e.CorCorresponsalId).HasColumnName("COR_CORRESPONSAL_ID");

                entity.Property(e => e.CorNombre)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("COR_NOMBRE");
            });

            modelBuilder.Entity<Oficina>(entity =>
            {
                entity.HasKey(e => e.OfiId)
                    .HasName("PK__OFICINAS__129AAC38E26F55AA");

                entity.ToTable("OFICINAS");

                entity.Property(e => e.OfiId).HasColumnName("OFI_ID");

                entity.Property(e => e.OfiCorresponsalId).HasColumnName("OFI_CORRESPONSAL_ID");

                entity.Property(e => e.OfiNombre)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("OFI_NOMBRE");

                entity.HasOne(d => d.OfiCorresponsal)
                    .WithMany(p => p.Oficinas)
                    .HasForeignKey(d => d.OfiCorresponsalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OFICINAS_CORRESPONSALES");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
