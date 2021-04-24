using Microsoft.EntityFrameworkCore;
using SecuringApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Data
{
    public class SecuringAppDbContext : DbContext
    {
        public SecuringAppDbContext(DbContextOptions<SecuringAppDbContext> options) : base(options)
        {

        }

        public DbSet<TaskModel> Tasks { get; set; }
    }
}
