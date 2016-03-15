using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace LST.Models
{
    public class TestMatch
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [DisplayName("考试场次")]
        public string Name { get; set; }

        [DisplayName("限制人数")]
        public int Limit { get; set; }

        [DisplayName("开始报名时间")]
        public DateTime StartTime { get; set; }

        [DisplayName("结束报名时间")]
        public DateTime EndTime { get; set; }

        [DisplayName("可见性")]
        public bool Visible { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual List<TestRecord> RecordsCollection { get; set; }

        [NotMapped]
        [DisplayName("报名人数")]
        public int Count
        {
            get
            {
                if (RecordsCollection == null)
                    return 0;
                else
                    return RecordsCollection.Count();
            }
            set
            {
                return;
            }
        }

        [NotMapped]
        [DisplayName("启用中")]
        public bool Enabled
        {
            get
            {
                if (DateTime.Now >= StartTime && DateTime.Now <= EndTime)
                    return true;
                else
                    return false;
            }
            set
            {
                return;
            }
        }
    }

    public class TestRecord
    {
        public Guid Id { get; set; }

        public TestMatch Match { get; set; }

        public ApplicationUser User { get; set; }

        public string Score { get; set; }

        public TestRecord(TestMatch match, ApplicationUser user)
        {
            Id = Guid.NewGuid();
            Match = match;
            User = user;
            Score = "";
        }

        public TestRecord()
        {
            Id = Guid.NewGuid();
            Score = "";
        }
    }

    public class TestHelper
    {
        public bool ApplyMatch(TestMatch match, ApplicationUser user)
        {
            using (var db = new ApplicationDbContext())
            {
                //获取实体对象
                var contextmatch = db.TestMatches.Find(match.Id);
                var contextuser = db.Users.Find(user.Id);
                var record = new TestRecord(contextmatch, contextuser);
                if (contextmatch == null || contextuser == null)
                    return false;

                //判断是否可以继续操作
                if (contextmatch.Count >= contextmatch.Limit || !contextmatch.Enabled || contextuser.Applied || !contextuser.Enabled)
                    return false;

                db.TestRecords.Add(record);
                contextuser.Applied = true;
                db.OperationRecords.Add(new OperationRecord(user.StudentNumber, "Apply", match.Name));

                //保存，采取乐观并发
                bool succeed;
                do
                {
                    succeed = true;
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        succeed = false;
                        ex.Entries.Single().Reload();
                        if (contextmatch.Count >= contextmatch.Limit)
                            return false;
                    }
                } while (!succeed);

                //检查操作成功
                if (!contextmatch.RecordsCollection.Contains(record))
                    return false;
            }
            return true;
        }

        public bool QuitMatch(TestMatch match, ApplicationUser user)
        {
            using (var db = new ApplicationDbContext())
            {
                var contextmatch = db.TestMatches.Find(match.Id);
                var contextuser = db.Users.Find(user.Id);
                var record = new TestRecord(contextmatch, contextuser);
                if (contextmatch == null || contextuser == null)
                    return false;

                if (!contextmatch.RecordsCollection.Contains(record) || !contextmatch.Enabled)
                    return false;

                contextmatch.RecordsCollection.Remove(record);
                contextuser.RecordsCollection.Remove(record);
                db.TestRecords.Remove(record);
                contextuser.Applied = false;
                db.OperationRecords.Add(new OperationRecord(user.StudentNumber, "Quit", contextmatch.Name));

                bool succeed;
                do
                {
                    succeed = true;
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        succeed = false;
                        ex.Entries.Single().Reload();
                    }
                } while (!succeed);

                if (contextmatch.RecordsCollection.Contains(record))
                    return false;
            }
            return true;
        }

        public bool GenerateMatches(IEnumerable<string> days, IEnumerable<string> lessons, int limit, DateTime startTime, DateTime endTime)
        {
            List<TestMatch> matches = new List<TestMatch>();
            using (var db = new ApplicationDbContext())
            {
                foreach (string d in days)
                    foreach (string l in lessons)
                    {
                        db.TestMatches.Add(new TestMatch
                        {
                            Name = d + " - " + l,
                            Limit = limit,
                            StartTime = startTime,
                            EndTime = endTime,
                            Visible = true
                        });
                    }
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        IEnumerable<TestMatch> MatchCrossJoin(IEnumerable<string> days, IEnumerable<string> lessons, int limit, DateTime startTime, DateTime endTime)
        {
            foreach (string d in days)
                foreach (string l in lessons)
                {
                    yield return new TestMatch { Name = d + l, Limit = limit, StartTime = startTime, EndTime = endTime };
                }
        }
    }
}