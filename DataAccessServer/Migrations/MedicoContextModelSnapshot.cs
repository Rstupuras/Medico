﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DatabaseServer.Migrations
{
    [DbContext(typeof(MedicoContext))]
    partial class MedicoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Appointment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTime");

                    b.Property<int>("DoctorID");

                    b.Property<bool>("IsViewed");

                    b.Property<int>("PatientID");

                    b.Property<string>("Reason");

                    b.Property<string>("Summary");

                    b.HasKey("ID");

                    b.HasIndex("DoctorID");

                    b.HasIndex("PatientID");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Doctor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.Property<string>("Password")
                        .HasMaxLength(15);

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Username")
                        .HasMaxLength(15);

                    b.HasKey("ID");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasFilter("[Username] IS NOT NULL");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Medicament", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<bool>("IsPrescribed");

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.HasKey("ID");

                    b.ToTable("Medicaments");
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsSent");

                    b.Property<DateTime>("OrderDate");

                    b.Property<string>("OrderNumber");

                    b.Property<int>("PatientID");

                    b.Property<int>("PharmacyID");

                    b.Property<string>("Status");

                    b.HasKey("ID");

                    b.HasIndex("PatientID");

                    b.HasIndex("PharmacyID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OrderItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MedicamentID");

                    b.Property<int>("OrderID");

                    b.Property<int>("Quantity");

                    b.HasKey("ID");

                    b.HasIndex("MedicamentID");

                    b.HasIndex("OrderID");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Patient", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<int>("MainDoctorID");

                    b.Property<string>("Name");

                    b.Property<string>("Password")
                        .HasMaxLength(15);

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Username")
                        .HasMaxLength(15);

                    b.HasKey("ID");

                    b.HasIndex("MainDoctorID");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasFilter("[Username] IS NOT NULL");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Pharmacy", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("Location");

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.Property<string>("Password")
                        .HasMaxLength(15);

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Username")
                        .HasMaxLength(15);

                    b.HasKey("ID");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasFilter("[Username] IS NOT NULL");

                    b.ToTable("Pharmacies");
                });

            modelBuilder.Entity("Prescription", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTimeFrom");

                    b.Property<DateTime>("DateTimeTo");

                    b.Property<string>("Description");

                    b.Property<int>("DoctorId");

                    b.Property<int>("MedicamentId");

                    b.Property<int>("PatientId");

                    b.HasKey("ID");

                    b.HasIndex("DoctorId");

                    b.HasIndex("MedicamentId");

                    b.HasIndex("PatientId");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("Appointment", b =>
                {
                    b.HasOne("Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.HasOne("Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pharmacy")
                        .WithMany("Orders")
                        .HasForeignKey("PharmacyID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OrderItem", b =>
                {
                    b.HasOne("Medicament", "Medicament")
                        .WithMany()
                        .HasForeignKey("MedicamentID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Patient", b =>
                {
                    b.HasOne("Doctor", "MainDoctor")
                        .WithMany()
                        .HasForeignKey("MainDoctorID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Prescription", b =>
                {
                    b.HasOne("Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Medicament", "Medicament")
                        .WithMany()
                        .HasForeignKey("MedicamentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Patient", "patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
