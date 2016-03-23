using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LST.Models
{
    public class TestMatch
    {
        public Guid Id { get; set; }

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
        }
    }

    public class TestRecord
    {
        public Guid Id { get; set; }

        public virtual TestMatch Match { get; set; }

        public virtual ApplicationUser User { get; set; }

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
            if (user.Applied == false)
                return false;
            using (var db = new ApplicationDbContext())
            {
                var contextmatch = db.TestMatches.Find(match.Id);
                var contextuser = db.Users.Find(user.Id);
                var record = contextuser.RecordsCollection.Where(r => r.Match.StartTime <= DateTime.Now && r.Match.EndTime >= DateTime.Now).SingleOrDefault();
                if (contextmatch == null || contextuser == null || record == null)
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

        public void AddHistory(ApplicationUser user)
        {
            var con = new SqlConnection(@"Server=10.1.1.68\seasqlserver;Database=ForeSea;User ID=checkin;Password=seacheckin;Trusted_Connection=false;");
            var cmd = new SqlCommand("", con);
            object result;
            cmd.CommandText = @"SELECT Score FROM dbo.LST_History WHERE Id=@Id";
            cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.VarChar));
            cmd.Parameters["@Id"].Value = user.StudentNumber;
            con.Open();
            try
            {
                result = cmd.ExecuteScalar();
            }
            finally
            {
                con.Close();
            }
            if (result != null)
            {
                using (var db = new ApplicationDbContext())
                {
                    var contextUser = db.Users.Find(user.Id);
                    var contextMatch = db.TestMatches.Where(t => t.Name == "Listening and Speaking Test - 1st").FirstOrDefault();
                    if (contextMatch == null)
                    {
                        db.TestMatches.Add(new TestMatch
                        {
                            Id = Guid.Empty,
                            Name = "Listening and Speaking Test - 1st",
                            Limit = 900,
                            Visible = false,
                            StartTime = new DateTime(2015, 12, 31, 0, 0, 0),
                            EndTime = new DateTime(2015, 12, 31, 23, 59, 59)
                        });
                        db.SaveChanges();
                        contextMatch = db.TestMatches.Find(Guid.Empty);
                    }

                    //处理数据
                    var record = new TestRecord(contextMatch, contextUser);
                    record.Score = result.ToString();
                    if (record.Score == "2.7")
                    {
                        record.Score = "通过考试";
                        contextUser.Enabled = false;
                    }
                    else if (record.Score == "-1")
                    {
                        record.Score = "旷考";
                        contextUser.Enabled = false;
                    }

                    contextUser.RecordsCollection.Add(record);
                    contextMatch.RecordsCollection.Add(record);
                    db.TestRecords.Add(record);
                    db.OperationRecords.Add(new OperationRecord(user.StudentNumber, "AddHistory", contextMatch.Name));

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
                        }
                    } while (!succeed);
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