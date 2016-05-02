using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;

namespace LST.Models
{
    // 可以通过向 ApplicationUser 类添加更多属性来为用户添加配置文件数据。若要了解详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=317594。
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [DisplayName("学号")]
        public string StudentNumber { get; set; }

        [DisplayName("姓名")]
        public string StudentName { get; set; }

        [NotMapped]
        [DisplayName("已启用")]
        public bool Enabled
        {
            get
            {
                if (RecordsCollection == null)
                    return EnabledStored;
                if (RecordsCollection.Count >= 3)
                    return false;
                return EnabledStored;
            }
            set
            {
                EnabledStored = value;
            }
        }

        [Required]
        public bool EnabledStored { get; private set; } = true;

        [Required]
        [DisplayName("已报名")]
        public bool Applied { get; set; } = false;

        public virtual List<TestRecord> RecordsCollection { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<TestMatch> TestMatches { get; set; }

        public virtual DbSet<OperationRecord> OperationRecords { get; set; }

        public virtual DbSet<TestRecord> TestRecords { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TestMatch>().Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ApplicationUser>().Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ApplicationUser>().Property(a => a.EnabledStored).HasColumnName("Enabled");
            modelBuilder.Entity<TestRecord>().HasRequired(t => t.Match).WithMany(m => m.RecordsCollection).Map(r => r.MapKey("Match_Id")).WillCascadeOnDelete();
            modelBuilder.Entity<TestRecord>().HasRequired(t => t.User).WithMany(u => u.RecordsCollection).Map(r => r.MapKey("User_Id")).WillCascadeOnDelete();
        }
    }
}