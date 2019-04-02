
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

public class MedicoContext : DbContext
    {

    public MedicoContext(DbContextOptions<MedicoContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelbuilder)
    {
        foreach (var relationship in modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }   
                base.OnModelCreating(modelbuilder);
                modelbuilder.Entity<Doctor>()
                .HasIndex(c => c.Username).IsUnique();

                base.OnModelCreating(modelbuilder);
                modelbuilder.Entity<Patient>()
                .HasIndex(c => c.Username).IsUnique();

                base.OnModelCreating(modelbuilder);
                modelbuilder.Entity<Pharmacy>()
                .HasIndex(c => c.Username).IsUnique();
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<OrderItem> Items { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }

    }
