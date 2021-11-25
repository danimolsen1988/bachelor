using Bachelor.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor.Common
{
    public class SqlDBContext: DbContext
    {
        public SqlDBContext(DbContextOptions<SqlDBContext> options) : base(options) { 
            
        }

        public DbSet<Device> Devices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer("");
        }
    }
}
