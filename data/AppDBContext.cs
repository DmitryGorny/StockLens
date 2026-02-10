using Microsoft.EntityFrameworkCore;
using StockLens.Models;
using System;

namespace StockLens.data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<Sectors> Sectors { get; set; }
        public DbSet<Industries> Industries {  get; set; }
        public DbSet<Tickers> Tickers { get; set; }
        public DbSet<Cities> Cities { get; set; }

        public DbSet<Quotes> Quotes { get; set; }
    }
}
