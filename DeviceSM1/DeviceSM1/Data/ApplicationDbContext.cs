using System;
using System.Collections.Generic;
using System.Text;
using DeviceSM1.Models.Identity;
using DeviceSM1.Models.Table;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeviceSM1.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(b =>
            {
                b.HasKey(u => u.Id);

                b.ToTable("User");

                b.Property(u => u.Id).ValueGeneratedOnAdd();
                b.Property(u => u.Phone).HasMaxLength(256);
                b.Property(u => u.Mobile).HasMaxLength(256);
                b.Property(u => u.Address).HasMaxLength(256);
                b.Property(u => u.Company).HasMaxLength(256);
                b.Property(u => u.ContactPerson).HasMaxLength(256);
                b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
                b.Property(u => u.SecurityStamp).HasMaxLength(256);

                b.Ignore(u => u.AccessFailedCount)
                    .Ignore(u => u.Email)
                    .Ignore(u => u.NormalizedEmail)
                    .Ignore(u => u.EmailConfirmed)
                    .Ignore(u => u.PhoneNumber)
                    .Ignore(u => u.PhoneNumberConfirmed)
                    .Ignore(u => u.TwoFactorEnabled)
                    .Ignore(u => u.LockoutEnd)
                    .Ignore(u => u.LockoutEnabled)
                    .Ignore(u => u.AccessFailedCount);
            });

            builder.Entity<ApplicationRole>(b =>
            {
                b.HasKey(u => u.Id);

                b.ToTable("Role");

                b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
                b.Property(u => u.Id);
                b.Property(u => u.DisplayName).HasMaxLength(256);
            });
        }

        public DbSet<TblDevice> DeviceList { get; set; }
        public DbSet<TblSensor> SensorList { get; set; }
        public DbSet<TblLocation> LocationList { get; set; }
    }
}
