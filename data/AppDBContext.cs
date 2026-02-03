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
    }
}
