using System;
using System.Data.Entity;
using System.Linq;

namespace Manager.Models
{

    public class BaseDbContext : DbContext
    {
        public BaseDbContext()
            : base("DefaultConnection")
        {

        }

        public virtual DbSet<Manager> Managers { get; set; }

        public virtual DbSet<AvailableTime > AvailableTime { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Manager>().HasMany(m => m.AvailableTime).WithRequired(a => a.Manager);
        }
    }
}