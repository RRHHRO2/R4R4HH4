using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Proyecto.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=RRHH")
        {
        }

        public DbSet<AreaTrabajo> AreasTrabajo { get; set; }
        public DbSet<Ausencia> Ausencias { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<EmpleadoDependiente> EmpleadosDependientes { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<SeguridadSocial> SeguridadSociales { get; set; }
        public DbSet<TipoContrato> TiposContrato { get; set; }
        public DbSet<TipoDocumento> TiposDocumento { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configuración de Area de Trabajo
            modelBuilder.Entity<AreaTrabajo>()
                .ToTable("AreasTrabajo")
                .HasKey(a => a.IdArea);

            modelBuilder.Entity<AreaTrabajo>()
                .Property(a => a.IdArea)
                .HasColumnName("IdArea")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<AreaTrabajo>()
                .Property(a => a.NombreArea)
                .IsRequired()
                .HasColumnName("NombreArea")
                .HasMaxLength(100)
                .IsUnicode(false); // varchar

            modelBuilder.Entity<AreaTrabajo>()
                .Property(a => a.Descripcion)
                .IsRequired()
                .HasColumnName("Descripcion")
                .IsUnicode(false) // text es varchar no unicode
                .HasColumnType("text");

            // Configuración de Ausencia
            modelBuilder.Entity<Ausencia>()
                .ToTable("Ausencias")
                .HasKey(a => a.Id);

            modelBuilder.Entity<Ausencia>()
                .Property(a => a.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Ausencia>()
                .Property(a => a.IdEmpleado)
                .HasColumnName("IdEmpleado")
                .IsRequired();

            modelBuilder.Entity<Ausencia>()
                .Property(a => a.FechaInicio)
                .HasColumnName("FechaInicio")
                .IsRequired()
                .HasColumnType("date");

            modelBuilder.Entity<Ausencia>()
                .Property(a => a.FechaFin)
                .HasColumnName("FechaFin")
                .IsRequired()
                .HasColumnType("date");

            modelBuilder.Entity<Ausencia>()
                .Property(a => a.TipoAusencia)
                .HasColumnName("TipoAusencia")
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false); // varchar(50)

            modelBuilder.Entity<Ausencia>()
                .Property(a => a.Justificacion)
                .HasColumnName("Justificacion")
                .IsRequired()
                .HasColumnType("text")
                .IsUnicode(false);

            // Relación Ausencia -> Empleado
            modelBuilder.Entity<Ausencia>()
                .HasRequired(a => a.Empleado)
                .WithMany(e => e.Ausencias)
                .HasForeignKey(a => a.IdEmpleado)
                .WillCascadeOnDelete(false);

            // Tabla Contratos
            modelBuilder.Entity<Contrato>()
                .ToTable("Contratos")
                .HasKey(c => c.Id);

            modelBuilder.Entity<Contrato>()
                .Property(c => c.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Contrato>()
                .Property(c => c.IdTipoContrato)
                .HasColumnName("IdTipoContrato");

            modelBuilder.Entity<Contrato>()
                .Property(c => c.IdEmpleado)
                .HasColumnName("IdEmpleado");

            modelBuilder.Entity<Contrato>()
                .Property(c => c.FechaInicio)
                .HasColumnName("FechaInicio")
                .HasColumnType("date");

            modelBuilder.Entity<Contrato>()
                .Property(c => c.FechaFin)
                .HasColumnName("FechaFin")
                .HasColumnType("date");

            // Relaciones

            modelBuilder.Entity<Contrato>()
                .HasOptional(c => c.TipoContrato)
                .WithMany(t => t.Contratos)
                .HasForeignKey(c => c.IdTipoContrato)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contrato>()
                .HasOptional(c => c.Empleado)
                .WithMany(e => e.Contratos)
                .HasForeignKey(c => c.IdEmpleado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Empleado>()
    .ToTable("Empleados")
    .HasKey(e => e.IdEmpleado);

            modelBuilder.Entity<Empleado>()
                .Property(e => e.IdEmpleado)
                .HasColumnName("IdEmpleado")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Empleado>()
                .Property(e => e.IdTipoDocumento)
                .HasColumnName("IdTipoDocumento");

            modelBuilder.Entity<Empleado>()
                .Property(e => e.NumeroDocumento)
                .HasColumnName("NumeroDocumento")
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Empleado>()
                .Property(e => e.FechaExpedicion)
                .HasColumnName("FechaExpedicion")
                .HasColumnType("date");

            modelBuilder.Entity<Empleado>()
                .Property(e => e.MunicipioExpedicion)
                .HasColumnName("MunicipioExpedicion");

            modelBuilder.Entity<Empleado>()
                .Property(e => e.Nombres)
                .HasColumnName("Nombres")
                .IsRequired()
                .HasMaxLength(70);

            modelBuilder.Entity<Empleado>()
                .Property(e => e.Apellidos)
                .HasColumnName("Apellidos")
                .IsRequired()
                .HasMaxLength(70);

            modelBuilder.Entity<Empleado>()
                .Property(e => e.LugarNacimiento)
                .HasColumnName("LugarNacimiento");

            modelBuilder.Entity<Empleado>()
                .Property(e => e.Direccion)
                .HasColumnName("Direccion")
                .IsRequired()
                .HasMaxLength(70);

            modelBuilder.Entity<Empleado>()
                .Property(e => e.Barrio)
                .HasColumnName("Barrio")
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Empleado>()
                .Property(e => e.Telefono)
                .HasColumnName("Telefono")
                .HasMaxLength(11);

            modelBuilder.Entity<Empleado>()
                .Property(e => e.Celular)
                .HasColumnName("Celular")
                .HasMaxLength(11);

            modelBuilder.Entity<Empleado>()
                .Property(e => e.Correo)
                .HasColumnName("Correo")
                .IsRequired()
                .HasMaxLength(70);

            modelBuilder.Entity<Empleado>()
                .Property(e => e.EPS)
                .HasColumnName("EPS");

            modelBuilder.Entity<Empleado>()
                .Property(e => e.FondoPension)
                .HasColumnName("FondoPension");

            modelBuilder.Entity<Empleado>()
                .Property(e => e.FondoCesantias)
                .HasColumnName("FondoCesantias");

            modelBuilder.Entity<Empleado>()
                .Property(e => e.IdAreaTrabajo)
                .HasColumnName("IdAreaTrabajo");

            modelBuilder.Entity<Empleado>()
                .Property(e => e.IdContrato)
                .HasColumnName("IdContrato");

            // Relaciones

            modelBuilder.Entity<Empleado>()
                .HasRequired(e => e.TipoDocumento)
                .WithMany()
                .HasForeignKey(e => e.IdTipoDocumento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Empleado>()
                .HasRequired(e => e.MunicipioNacimiento)
                .WithMany()
                .HasForeignKey(e => e.LugarNacimiento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Empleado>()
                .HasRequired(e => e.MunicipioExpedicionDoc)
                .WithMany()
                .HasForeignKey(e => e.MunicipioExpedicion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Empleado>()
                .HasRequired(e => e.EntidadEPS)
                .WithMany()
                .HasForeignKey(e => e.EPS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Empleado>()
                .HasRequired(e => e.EntidadFondoPension)
                .WithMany()
                .HasForeignKey(e => e.FondoPension)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Empleado>()
                .HasRequired(e => e.EntidadFondoCesantias)
                .WithMany()
                .HasForeignKey(e => e.FondoCesantias)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Empleado>()
                .HasRequired(e => e.AreaTrabajo)
                .WithMany()
                .HasForeignKey(e => e.IdAreaTrabajo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Departamento>()
                .ToTable("Departamentos")
                .HasKey(d => d.IdDepartamento);

            modelBuilder.Entity<Departamento>()
                .Property(d => d.IdDepartamento)
                .HasColumnName("IdDepartamento")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None); // No es identity

            modelBuilder.Entity<Departamento>()
                .Property(d => d.Nombre)
                .HasColumnName("Nombre")
                .IsRequired()
                .HasMaxLength(70);

            modelBuilder.Entity<EmpleadoDependiente>()
    .ToTable("Empleados_Dependientes")
    .HasKey(ed => ed.Id);

            modelBuilder.Entity<EmpleadoDependiente>()
                .Property(ed => ed.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<EmpleadoDependiente>()
                .Property(ed => ed.IdEmpleado)
                .HasColumnName("IdEmpleado")
                .IsRequired();

            modelBuilder.Entity<EmpleadoDependiente>()
                .Property(ed => ed.IdTipoDocumento)
                .HasColumnName("IdTipoDocumento")
                .IsRequired();

            modelBuilder.Entity<EmpleadoDependiente>()
                .Property(ed => ed.Apellidos)
                .HasColumnName("Apellidos")
                .IsRequired()
                .HasMaxLength(70);

            modelBuilder.Entity<EmpleadoDependiente>()
                .Property(ed => ed.Nombres)
                .HasColumnName("Nombres")
                .IsRequired()
                .HasMaxLength(70);

            modelBuilder.Entity<EmpleadoDependiente>()
                .Property(ed => ed.TipoDependiente)
                .HasColumnName("TipoDependiente")
                .IsRequired()
                .HasMaxLength(70);

            modelBuilder.Entity<EmpleadoDependiente>()
                .HasRequired(ed => ed.Empleado)
                .WithMany(e => e.Dependientes)
                .HasForeignKey(ed => ed.IdEmpleado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Municipio>()
                .ToTable("Municipios")
                .HasKey(m => m.IdMunicipio);

            modelBuilder.Entity<Municipio>()
                .Property(m => m.IdMunicipio)
                .HasColumnName("IdMunicipio")
                .IsRequired();

            modelBuilder.Entity<Municipio>()
                .Property(m => m.Nombre)
                .HasColumnName("Nombre")
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Municipio>()
                .Property(m => m.Estado)
                .HasColumnName("Estado")
                .IsRequired();

            modelBuilder.Entity<Municipio>()
                .Property(m => m.IdDepartamento)
                .HasColumnName("IdDepartamento")
                .IsRequired();

            modelBuilder.Entity<Municipio>()
                .HasRequired(m => m.Departamento)
                .WithMany(d => d.Municipios)
                .HasForeignKey(m => m.IdDepartamento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rol>()
                .ToTable("Roles")
                .HasKey(r => r.IdRol);

            modelBuilder.Entity<Rol>()
                .Property(r => r.IdRol)
                .HasColumnName("IdRol")
                .IsRequired();

            modelBuilder.Entity<Rol>()
                .Property(r => r.NombreRol)
                .HasColumnName("NombreRol")
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false); // Porque es varchar, no nvarchar

            modelBuilder.Entity<SeguridadSocial>()
                .ToTable("SeguridadSocial")
                .HasKey(s => s.IdSeguridadSocial);

            modelBuilder.Entity<SeguridadSocial>()
                .Property(s => s.IdSeguridadSocial)
                .HasColumnName("IdSeguridadSocial")
                .IsRequired();

            modelBuilder.Entity<SeguridadSocial>()
                .Property(s => s.Nombre)
                .HasColumnName("Nombre")
                .IsRequired()
                .HasMaxLength(70)
                .IsUnicode(true); // porque es nvarchar

            modelBuilder.Entity<SeguridadSocial>()
                .Property(s => s.Tipo)
                .HasColumnName("Tipo")
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true); // porque es nvarchar

            modelBuilder.Entity<TipoContrato>()
                .ToTable("TiposContrato")
                .HasKey(tc => tc.IdTipoContrato);

            modelBuilder.Entity<TipoContrato>()
                .Property(tc => tc.IdTipoContrato)
                .HasColumnName("IdTipoContrato")
                .IsRequired();

            modelBuilder.Entity<TipoContrato>()
                .Property(tc => tc.NombreTipo)
                .HasColumnName("NombreTipo")
                .HasMaxLength(50)
                .IsUnicode(false);

            modelBuilder.Entity<TipoDocumento>()
            .ToTable("TiposDocumento")
            .HasKey(td => td.IdTipoDocumento);

            modelBuilder.Entity<TipoDocumento>()
                .Property(td => td.IdTipoDocumento)
                .HasColumnName("IdTipoDocumento")
                .IsRequired();

            modelBuilder.Entity<TipoDocumento>()
                .Property(td => td.Nombre)
                .HasColumnName("Nombre")
                .HasMaxLength(50)
                .IsUnicode(true)   // nvarchar
                .IsRequired();

            modelBuilder.Entity<TipoDocumento>()
                .Property(td => td.Abreviatura)
                .HasColumnName("Abreviatura")
                .HasMaxLength(3)
                .IsUnicode(true)   // nchar es Unicode fixed length
                .IsFixedLength()   // para que sea nchar, no nvarchar
                .IsRequired();

            modelBuilder.Entity<Usuario>()
    .ToTable("Usuarios")
    .HasKey(u => u.Id);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Id)
                .HasColumnName("Id")
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Cedula)
                .HasColumnName("Cedula")
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Cedula)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.NombreCompleto)
                .HasColumnName("NombreCompleto")
                .HasMaxLength(100)
                .IsUnicode(false)  // varchar
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Correo)
                .HasColumnName("Correo")
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Contrasena)
                .HasColumnName("Contrasena")
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.HashKey)
                .HasColumnName("HashKey")
                .HasColumnType("binary")
                .HasMaxLength(32)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.HashIV)
                .HasColumnName("HashIV")
                .HasColumnType("binary")
                .HasMaxLength(16)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.IdRol)
                .HasColumnName("IdRol")
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.IdArea)
                .HasColumnName("IdArea")
                .IsRequired();

            modelBuilder.Entity<Usuario>()
             .HasRequired(u => u.Rol)
             .WithMany(r => r.Usuarios)
             .HasForeignKey(u => u.IdRol);

            modelBuilder.Entity<Usuario>()
                .HasRequired(u => u.AreaTrabajo)
                .WithMany(a => a.Usuarios)
                .HasForeignKey(u => u.IdArea);

            base.OnModelCreating(modelBuilder);
        }
    }
}