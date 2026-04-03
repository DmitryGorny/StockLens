using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockLens.Models;
using System;

namespace StockLens.data
{
    public class AppDBContext : IdentityDbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<Sectors> Sectors { get; set; }
        public DbSet<Industries> Industries {  get; set; }
        public DbSet<Tickers> Tickers { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Quotes> Quotes { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<RefreshTokens> RefreshTokens { get; set; }
        public DbSet<BriefcasesTickers> BriefcasesTickers { get; set; }
        public DbSet<Briefcases> Briefcases {  get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "User",
                    NormalizedName = "USER"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            builder.Entity<Briefcases>()
                .HasMany(b => b.Tickers)
                .WithMany(t => t.Briefcases)
                .UsingEntity<BriefcasesTickers>().HasKey(bt => new {bt.TickerId, bt.BriefcaseId});

            builder.Entity<BriefcasesTickers>().HasKey(k => new { k.BriefcaseId, k.TickerId });
        }
    }
}