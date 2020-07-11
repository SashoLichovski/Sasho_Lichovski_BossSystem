using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BossSystem.Data
{
    public class BossSystemDbContext : DbContext
    {
        public BossSystemDbContext(DbContextOptions<BossSystemDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
