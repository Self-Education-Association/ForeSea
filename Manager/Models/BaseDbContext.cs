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

        public virtual DbSet<AvailableTime> AvailableTime { get; set; }

        public virtual DbSet<Log> Logs { get; set; }

        public virtual DbSet<Status> Status { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Manager>().HasMany(m => m.AvailableTimes).WithRequired(a => a.Manager);
            modelBuilder.Entity<Manager>().Property(m => m.ManagerId).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Log>().Property(l => l.LogTime).HasColumnType("datetime2");
            modelBuilder.Entity<Log>().Property(l => l.LogId).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<AvailableTime>().Property(a => a.AvailableTimeId).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
        }
    }
}