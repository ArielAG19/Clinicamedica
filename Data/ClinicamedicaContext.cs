using System;
using Microsoft.EntityFrameworkCore;
using Clinicamedica.Models;

namespace Clinicamedica.Data
{
    public class ClinicamedicaContext : DbContext
    {
        public ClinicamedicaContext(DbContextOptions<ClinicamedicaContext> options)
            : base(options)
        {
        }

        public DbSet<Paciente> Paciente { get; set; } = default!;
        public DbSet<Cita> Cita { get; set; } = default!;
        public DbSet<Factura> Factura { get; set; } = default!;
        public DbSet<Medico> Medico { get; set; } = default!;
        public DbSet<Tratamiento> Tratamiento { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura la propiedad Monto en el modelo Factura
            modelBuilder.Entity<Factura>()
                .Property(f => f.Monto)
                .HasColumnType("decimal(18,2)");  // Especifica precisión y escala

            // Configura la relación entre Factura y Paciente
            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Paciente)
                .WithMany()  // Aquí puedes especificar la relación inversa si es necesario
                .HasForeignKey(f => f.PacienteId)
                .OnDelete(DeleteBehavior.NoAction);  // Usa NoAction en lugar de Restrict

            // Configura la relación entre Factura y Tratamiento
            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Tratamiento)
                .WithMany()  // Aquí puedes especificar la relación inversa si es necesario
                .HasForeignKey(f => f.TratamientoId)
                .OnDelete(DeleteBehavior.NoAction);  // Usa NoAction en lugar de Restrict

            // Configura la relación entre Tratamiento y Cita
            modelBuilder.Entity<Tratamiento>()
                .HasOne(t => t.Cita)
                .WithMany()  // Aquí puedes especificar la relación inversa si es necesario
                .HasForeignKey(t => t.CitaId)
                .OnDelete(DeleteBehavior.NoAction);  // Usa NoAction en lugar de Restrict

            // Configura la relación entre Cita y Paciente
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Paciente)
                .WithMany(p => p.Citas)  // Configura la relación inversa
                .HasForeignKey(c => c.PacienteId)
                .OnDelete(DeleteBehavior.NoAction);  // Usa NoAction en lugar de Restrict

            // Configura la relación entre Cita y Medico
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Medico)
                .WithMany(m => m.Citas)  // Configura la relación inversa
                .HasForeignKey(c => c.MedicoId)
                .OnDelete(DeleteBehavior.NoAction);  // Usa NoAction en lugar de Restrict
        }
    }
}