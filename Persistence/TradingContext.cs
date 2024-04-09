using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class TradingContext : DbContext
    {
        public TradingContext(DbContextOptions<TradingContext> options)
           : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("TradingData");
            //optionsBuilder.EnableSensitiveDataLogging();
        }

        public DbSet<MarketDepthAuditLog> MarketDepthAuditLogs { get; set; }

    }
}
