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

                entity.HasData(
                    new User() { UserId = 1, Email = "user1@gmail.com", Name = "Jan", Lastname = "Kowalski", Password = "password1"},
                    new User() { UserId = 2, Email = "user2@gmail.com", Name = "Jan", Lastname = "Kowalski", Password = "password2" },
                    new User() { UserId = 3, Email = "user3@gmail.com", Name = "Jan", Lastname = "Kowalski", Password = "password3" }
                    );
            });
        }
    }
}
