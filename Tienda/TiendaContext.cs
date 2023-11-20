using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tienda;

public partial class TiendaContext : DbContext
{
    public TiendaContext()
    {
    }

    public TiendaContext(DbContextOptions<TiendaContext> options)
        : base(options)
    {
    }    

    public virtual DbSet<Establecimiento> Establecimientos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductosVenta> ProductosVentas { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=PORTATIL\\SQLEXPRESS;Initial Catalog=Tienda;User Id=sa;Password=rootadmin;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Establecimiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__establec__3213E83F923EF6DA");

            entity.ToTable("establecimientos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("telefono");           
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__producto__3213E83F06012111");

            entity.ToTable("productos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstableciminetoId).HasColumnName("establecimineto_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.Establecimineto).WithMany(p => p.Productos)
                .HasForeignKey(d => d.EstableciminetoId)
                .HasConstraintName("FK__productos__estab__18EBB532");
        });

        modelBuilder.Entity<ProductosVenta>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("productos_ventas");

            entity.Property(e => e.ProductoId).HasColumnName("producto_id");
            entity.Property(e => e.VentaId).HasColumnName("venta_id");

            entity.HasOne(d => d.Producto).WithMany()
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK__productos__produ__1F98B2C1");

            entity.HasOne(d => d.Venta).WithMany()
                .HasForeignKey(d => d.VentaId)
                .HasConstraintName("FK__productos__venta__208CD6FA");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ventas__3213E83F450A10E4");

            entity.ToTable("ventas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.ProductoId).HasColumnName("producto_id");
            entity.Property(e => e.TiendaId).HasColumnName("tienda_id");
            entity.Property(e => e.Usuario_id)
                .HasMaxLength(450)
                .HasColumnName("usuario_id");

            entity.HasOne(d => d.Producto).WithMany(p => p.Venta)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK__ventas__producto__1CBC4616");

            entity.HasOne(d => d.Tienda).WithMany(p => p.Venta)
                .HasForeignKey(d => d.TiendaId)
                .HasConstraintName("FK__ventas__tienda_i__1BC821DD");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
