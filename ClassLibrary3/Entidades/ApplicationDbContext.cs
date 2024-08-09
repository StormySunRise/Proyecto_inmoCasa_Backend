using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3.Entidades
{
    public class ApplicationDbContext : DbContext
    {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }
            public DbSet<Proveedor> Proveedores { get; set; }
            public DbSet<OrdenCompra> OrdenesCompra { get; set; }
            public DbSet<Factura> Facturas { get; set; }
            public DbSet<Usuario> Usuarios { get; set; }
            public DbSet<Rol> Roles { get; set; }
            public DbSet<Cliente> Clientes { get; set; }
            public DbSet<Propiedad> Propiedades { get; set; }
            public DbSet<Solicitud> Solicitudes { get; set; }
            public DbSet<Documento> Documentos { get; set; }
            public DbSet<Contrato> Contratos { get; set; }
            public DbSet<VerificacionFactura> Verificaciones { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<Usuario>()
               .HasOne(u => u.Rol)
               .WithMany(r => r.Usuarios)
               .HasForeignKey(u => u.RolId)
               .IsRequired(false);  // Esto hace que la relación sea opcional

                modelBuilder.Entity<OrdenCompra>()
                    .HasOne(o => o.Proveedor)
                    .WithMany(p => p.OrdenesCompra)
                    .HasForeignKey(o => o.ProveedorId);

                modelBuilder.Entity<Factura>()
                    .HasOne(f => f.Proveedor)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(f => f.ProveedorId);

                modelBuilder.Entity<Factura>()
            .HasMany(f => f.Verificaciones)
           .WithOne(vf => vf.Factura)
           .HasForeignKey(vf => vf.FacturaId);

                modelBuilder.Entity<VerificacionFactura>()
               .HasOne(vf => vf.Factura)
               .WithMany(f => f.Verificaciones)
               .HasForeignKey(vf => vf.FacturaId);

                // Cliente-Solicitud (No acción en cascada)
                modelBuilder.Entity<Cliente>()
                        .HasMany(c => c.Solicitudes)
                        .WithOne(s => s.Cliente)
                        .HasForeignKey(s => s.ClienteId)
                        .OnDelete(DeleteBehavior.Restrict); // Evita eliminación en cascada

                // Cliente-Propiedad (No acción en cascada)
                modelBuilder.Entity<Cliente>()
                    .HasMany(c => c.Propiedades)
                    .WithOne(p => p.Propietario)
                    .HasForeignKey(p => p.PropietarioId)
                    .OnDelete(DeleteBehavior.Restrict); // Evita eliminación en cascada

                // Solicitud-Documento (No acción en cascada)
                modelBuilder.Entity<Solicitud>()
                    .HasMany(s => s.Documentos)
                    .WithOne(d => d.Solicitud)
                    .HasForeignKey(d => d.SolicitudId)
                    .OnDelete(DeleteBehavior.Restrict); // Evita eliminación en cascada

                // Solicitud-Contrato (No acción en cascada)
                modelBuilder.Entity<Solicitud>()
                    .HasMany(s => s.Contratos)
                    .WithOne(c => c.Solicitud)
                    .HasForeignKey(c => c.SolicitudId)
                    .OnDelete(DeleteBehavior.Restrict); // Evita eliminación en cascada



                // Contrato-Propiedad (No acción en cascada)
                modelBuilder.Entity<Contrato>()
                    .HasOne(c => c.Propiedad)
                    .WithMany(p => p.Contratos)
                    .HasForeignKey(c => c.PropiedadId)
                    .OnDelete(DeleteBehavior.Restrict); // Evita eliminación en cascada


                //AUMENTE ESO DE AHÍ 
                // Configuración existente
                modelBuilder.Entity<Cliente>()
                    .HasMany(c => c.Solicitudes)
                    .WithOne(s => s.Cliente)
                    .HasForeignKey(s => s.ClienteId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Configuración para Propiedad
                modelBuilder.Entity<Propiedad>()
                    .HasOne(p => p.Propietario)
                    .WithMany(c => c.Propiedades)
                    .HasForeignKey(p => p.PropietarioId)
                    .OnDelete(DeleteBehavior.Restrict);


                // Configuración de precisión y escala para propiedades decimales
                modelBuilder.Entity<Contrato>()
                    .Property(c => c.Monto)
                    .HasPrecision(18, 2); // Ajusta la precisión y escala según tus necesidades

                modelBuilder.Entity<OrdenCompra>()
                    .Property(o => o.MontoTotal)
                    .HasPrecision(18, 2);

                modelBuilder.Entity<Propiedad>()
                    .Property(p => p.Precio)
                    .HasPrecision(18, 2);
        }

        }
    }
