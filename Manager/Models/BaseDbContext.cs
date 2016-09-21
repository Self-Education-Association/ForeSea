/*
Copyright [2016] [puyang.c@foxmail.com]

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
 */

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
        }
    }
}