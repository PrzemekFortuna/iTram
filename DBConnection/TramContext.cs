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


        public TramContext(DbContextOptions options) : base(options)
        {
        }

        protected TramContext()
        {
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
