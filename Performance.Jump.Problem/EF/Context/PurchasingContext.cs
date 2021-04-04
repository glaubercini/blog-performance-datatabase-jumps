using Performance.Jump.Problem.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Performance.Jump.Problem.EF.Context
{
    class PurchasingContext : DbContext
    {
        private static readonly string Host = "127.0.0.1";
        private static readonly string User = "postgres";
        private static readonly string DBname = "Adventureworks";
        private static readonly string Password = "blog";
        private static readonly string Port = "5432";

        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<PurchaseOrderHeader> PurchaseOrderHeader { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseNpgsql(string.Format(
                    "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
                    Host,
                    User,
                    DBname,
                    Port,
                    Password))
                .UseLowerCaseNamingConvention();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("purchasing");
            base.OnModelCreating(modelBuilder);
        }


    }
}