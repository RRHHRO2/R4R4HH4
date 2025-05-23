using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Proyecto.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=DefaultConnection") { }

        public DbSet<AreaTrabajo> AreasTrabajo { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Dependiente> Dependientes { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<SeguridadSocial> SeguridadSociales { get; set; }
        public DbSet<TipoDocumento> TiposDocumento { get; set; }
        public DbSet<Ausencia> Ausencias { get; set; }
        public DbSet<TipoContrato> TiposContrato { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Elimina pluralización automática de tablas
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // AreaTrabajo
            modelBuilder.Entity<AreaTrabajo>()
                .ToTable("AreaTrabajo")
                .HasKey(a => a.IdAreaTrabajo);

            modelBuilder.Entity<AreaTrabajo>()
                .Property(a => a.NombreArea)
                .HasColumnName("NombreArea")
                .IsRequired()
                .HasMaxLength(50);

            // Departamento
            modelBuilder.Entity<Departamento>()
                .ToTable("Departamento")
                .HasKey(d => d.IdDepartamento);

            modelBuilder.Entity<Departamento>()
                .Property(d => d.NombreDepartamento)
                .HasColumnName("NombreDepartamento")
                .IsRequired()
                .HasMaxLength(50);

            // Empleado
            modelBuilder.Entity<Empleado>()
                .ToTable("Empleado")
                .HasKey(e => e.IdEmpleado);

            modelBuilder.Entity<Empleado>()
                .Property(e => e.Nombre)
                .HasColumnName("Nombre")
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Empleado>()
                .Property(e => e.Apellido)
                .HasColumnName("Apellido")
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Empleado>()
                .HasRequired(e => e.Municipio)
                .WithMany(m => m.Empleados)
                .HasForeignKey(e => e.IdMunicipio);

            modelBuilder.Entity<Empleado>()
                .HasRequired(e => e.TipoDocumento)
                .WithMany(t => t.Empleados)
                .HasForeignKey(e => e.IdTipoDocumento);

            modelBuilder.Entity<Empleado>()
                .HasRequired(e => e.AreaTrabajo)
                .WithMany(a => a.Empleados)
                .HasForeignKey(e => e.IdAreaTrabajo);

            // Contrato
            modelBuilder.Entity<Contrato>()
                .ToTable("Contrato")
                .HasKey(c => c.IdContrato);

            modelBuilder.Entity<Contrato>()
                .HasRequired(c => c.Empleado)
                .WithMany(e => e.Contratos)
                .HasForeignKey(c => c.IdEmpleado);

            // Dependiente
            modelBuilder.Entity<Dependiente>()
                .ToTable("Dependiente")
                .HasKey(d => d.IdDependiente);

            modelBuilder.Entity<Dependiente>()
                .Property(d => d.Nombre)
                .HasColumnName("Nombre")
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Dependiente>()
                .Property(d => d.Parentesco)
                .HasColumnName("Parentesco")
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Dependiente>()
                .HasRequired(d => d.Empleado)
                .WithMany(e => e.Dependientes)
                .HasForeignKey(d => d.IdEmpleado);

            // Municipio
            modelBuilder.Entity<Municipio>()
                .ToTable("Municipio")
                .HasKey(m => m.IdMunicipio);

            modelBuilder.Entity<Municipio>()
                .Property(m => m.NombreMunicipio)
                .HasColumnName("NombreMunicipio")
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Municipio>()
                .HasRequired(m => m.Departamento)
                .WithMany(d => d.Municipios)
                .HasForeignKey(m => m.IdDepartamento);

            // Rol
            modelBuilder.Entity<Rol>()
                .ToTable("Rol")
                .HasKey(r => r.IdRol);

            modelBuilder.Entity<Rol>()
                .Property(r => r.NombreRol)
                .HasColumnName("NombreRol")
                .IsRequired()
                .HasMaxLength(50);

            // SeguridadSocial
            modelBuilder.Entity<SeguridadSocial>()
                .ToTable("SeguridadSocial")
                .HasKey(s => s.IdSeguridadSocial);

            modelBuilder.Entity<SeguridadSocial>()
                .Property(s => s.NombreEntidad)
                .HasColumnName("NombreEntidad")
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<SeguridadSocial>()
                .Property(s => s.Tipo)
                .HasColumnName("Tipo")
                .IsRequired()
                .HasMaxLength(100);

            // TipoDocumento
            modelBuilder.Entity<TipoDocumento>()
                .ToTable("TipoDocumento")
                .HasKey(t => t.IdTipoDocumento);

            modelBuilder.Entity<TipoDocumento>()
                .Property(t => t.NombreTipo)
                .HasColumnName("NombreTipo")
                .IsRequired()
                .HasMaxLength(50);

            // Ausencia
            modelBuilder.Entity<Ausencia>()
                .ToTable("Ausencia")
                .HasKey(a => a.IdAusencia);

            modelBuilder.Entity<Ausencia>()
                .Property(a => a.Motivo)
                .HasColumnName("Motivo")
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Ausencia>()
                .HasRequired(a => a.Empleado)
                .WithMany(e => e.Ausencias)
                .HasForeignKey(a => a.IdEmpleado);

            // TipoContrato
            modelBuilder.Entity<TipoContrato>()
                .ToTable("TipoContrato")
                .HasKey(t => t.IdTipoContrato);

            modelBuilder.Entity<TipoContrato>()
                .Property(t => t.NombreTipoContrato)
                .HasColumnName("NombreTipoContrato")
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Contrato>()
                .HasRequired(c => c.TipoContrato)
                .WithMany(t => t.Contratos)
                .HasForeignKey(c => c.IdTipoContrato);

            // Usuario
            modelBuilder.Entity<Usuario>()
                .ToTable("Usuarios")
                .HasKey(u => u.Id);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Id)
                .HasColumnName("Id");

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Cedula)
                .HasColumnName("Cedula");

            modelBuilder.Entity<Usuario>()
                .Property(u => u.NombreCompleto)
                .HasColumnName("NombreCompleto")
                .HasMaxLength(100);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Correo)
                .HasColumnName("Correo")
                .HasMaxLength(100);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Contrasena)
                .HasColumnName("Contrasena")
                .HasMaxLength(100);

            // FK: Usuario -> Rol
            modelBuilder.Entity<Usuario>()
                .HasOptional(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.IdRol);

            // FK: Usuario -> AreaTrabajo
            modelBuilder.Entity<Usuario>()
                .HasOptional(u => u.AreaTrabajo)
                .WithMany(a => a.Usuarios)
                .HasForeignKey(u => u.IdArea);
        }
    }
}