using DBConnection.Entities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnection
{
    public class TramContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<SensorsReading> SensorsReadings { get; set; }
        public DbSet<Tram> Trams { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<BeaconToken> BeaconTokens { get; set; }
        //public DbSet<SensorsReading.Gyroscope> Gyroscopes { get; set; }


        public TramContext(DbContextOptions options) : base(options)
        {
        }

        protected TramContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID=rjblccjftokfwc;Password=f04c048aa3978082ad41c56a4939558d60b9895c40205f7503dd6899ffef3c04;Host=ec2-54-247-70-127.eu-west-1.compute.amazonaws.com;Port=5432;Database=d9rli2kn2uff5q;Pooling=true;Use SSL Stream=True;SSL Mode=Require;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<SensorsReading>(entity =>
            {
                entity.HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.UserId);
            });
        }
    }
}
