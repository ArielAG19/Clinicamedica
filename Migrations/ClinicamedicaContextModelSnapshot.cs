﻿// <auto-generated />
using System;
using Clinicamedica.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Clinicamedica.Migrations
{
    [DbContext(typeof(ClinicamedicaContext))]
    partial class ClinicamedicaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Clinicamedica.Models.Cita", b =>
                {
                    b.Property<int>("CitaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CitaId"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("MedicoId")
                        .HasColumnType("int");

                    b.Property<int?>("MedicoId1")
                        .HasColumnType("int");

                    b.Property<string>("Motivo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.Property<int?>("PacienteId1")
                        .HasColumnType("int");

                    b.HasKey("CitaId");

                    b.HasIndex("MedicoId");

                    b.HasIndex("MedicoId1");

                    b.HasIndex("PacienteId");

                    b.HasIndex("PacienteId1");

                    b.ToTable("Cita");
                });

            modelBuilder.Entity("Clinicamedica.Models.Factura", b =>
                {
                    b.Property<int>("FacturaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FacturaId"));

                    b.Property<DateTime>("FechaPago")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Monto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.Property<int>("TratamientoId")
                        .HasColumnType("int");

                    b.HasKey("FacturaId");

                    b.HasIndex("PacienteId");

                    b.HasIndex("TratamientoId");

                    b.ToTable("Factura");
                });

            modelBuilder.Entity("Clinicamedica.Models.Medico", b =>
                {
                    b.Property<int>("MedicoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MedicoId"));

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Especialidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MedicoId");

                    b.ToTable("Medico");
                });

            modelBuilder.Entity("Clinicamedica.Models.Paciente", b =>
                {
                    b.Property<int>("PacienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PacienteId"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PacienteId");

                    b.ToTable("Paciente");
                });

            modelBuilder.Entity("Clinicamedica.Models.Tratamiento", b =>
                {
                    b.Property<int>("TratamientoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TratamientoId"));

                    b.Property<int>("CitaId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TratamientoId");

                    b.HasIndex("CitaId");

                    b.ToTable("Tratamiento");
                });

            modelBuilder.Entity("Clinicamedica.Models.Cita", b =>
                {
                    b.HasOne("Clinicamedica.Models.Medico", "Medico")
                        .WithMany()
                        .HasForeignKey("MedicoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Clinicamedica.Models.Medico", null)
                        .WithMany("Citas")
                        .HasForeignKey("MedicoId1");

                    b.HasOne("Clinicamedica.Models.Paciente", "Paciente")
                        .WithMany()
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Clinicamedica.Models.Paciente", null)
                        .WithMany("Citas")
                        .HasForeignKey("PacienteId1");

                    b.Navigation("Medico");

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("Clinicamedica.Models.Factura", b =>
                {
                    b.HasOne("Clinicamedica.Models.Paciente", "Paciente")
                        .WithMany()
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Clinicamedica.Models.Tratamiento", "Tratamiento")
                        .WithMany()
                        .HasForeignKey("TratamientoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Paciente");

                    b.Navigation("Tratamiento");
                });

            modelBuilder.Entity("Clinicamedica.Models.Tratamiento", b =>
                {
                    b.HasOne("Clinicamedica.Models.Cita", "Cita")
                        .WithMany()
                        .HasForeignKey("CitaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cita");
                });

            modelBuilder.Entity("Clinicamedica.Models.Medico", b =>
                {
                    b.Navigation("Citas");
                });

            modelBuilder.Entity("Clinicamedica.Models.Paciente", b =>
                {
                    b.Navigation("Citas");
                });
#pragma warning restore 612, 618
        }
    }
}
