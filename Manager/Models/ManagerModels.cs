using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Manager.Models
{
    public class Manager
    {
        public Guid ManagerId { get; set; }
        public string Name { get; private set; }
        public string AccountName { get; private set; }

        public virtual List<AvailableTime> AvailableTimes { get; private set; }

        public int MinCount { get; private set; }
        public int MaxCount { get; private set; }

        public int TotalCount { get; private set; }
        public int TotalTime { get; private set; }
        public int LateTime { get; private set; }

        public void SetCount(int min, int max)
        {
            MinCount = min;
            MaxCount = max;
        }

        public bool CheckIn(CheckInTime time)
        {
            using (BaseDbContext db = new BaseDbContext())
            {
                var manager = db.Managers.Find(ManagerId);
                DateTime datetime = DateTime.Now;
                int lateTime = (int)(datetime.TimeOfDay - time.LateTime).TotalMinutes;
                if (lateTime <= 0)
                {
                    lateTime = 0;
                }
                bool saveFailed;
                do
                {
                    saveFailed = false;
                    manager.AddTime(time.TotalTime - lateTime, lateTime);
                    db.Status.Add(new Status { StatusId = Guid.Empty, Name = Name, TimeName = time.TimeName });
                    db.Logs.Add(new Log(string.Format("系统：添加[{0}]值班员[{1}]的在线纪录。", time.TimeName, Name)));
                    db.Logs.Add(new Log(string.Format("【{3}】值班员【{0}】于{1}签到，迟到{2}分钟。", Name, datetime, lateTime, time.TimeName)));
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        saveFailed = true;
                        e.Entries.Single().Reload();
                    }
                } while (saveFailed);
                return true;
            }
        }

        public bool CheckOut()
        {
            using (BaseDbContext db = new BaseDbContext())
            {
                bool saveFailed;
                do
                {
                    saveFailed = false;
                    var status = db.Status.Where(s => s.Name == Name).SingleOrDefault();
                    if (status != null)
                    {
                        db.Status.Remove(status);
                        db.Logs.Add(new Log(string.Format("系统：移除[{0}]值班员[{1}]的在线纪录。", status.TimeName, status.Name)));
                    }
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        saveFailed = true;
                        e.Entries.Single().Reload();
                        Log.ReportException(e);
                    }
                } while (saveFailed);
                return true;
            }
        }

        private bool AddTime(int total, int late)
        {
            TotalTime += total;
            LateTime += late;
            TotalCount++;
            return true;
        }

        public bool CheckAvailableTime()
        {
            //如果可以不用值班，始终返回true。
            if (MinCount == 0)
            {
                return true;
            }
            //判断是否至少有指定节数的可值班时间，不符合则返回false。
            if (AvailableTimes.Count() < MinAvailableTimeCount())
            {
                return false;
            }
            else
            {
                //判断是否至少有指定节数的早班，不符合则返回false。
                int count = 0;
                foreach (var item in AvailableTimes)
                {
                    if (item.TimeId % 10 == 1)
                    {
                        count++;
                    }
                }
                if (count < MinFirstClassCount())
                {
                    return false;
                }
            }
            //符合所有条件，返回true。
            return true;
        }

        public int MinAvailableTimeCount()
        {
            int times = 6;
            return times * MinCount;
        }

        public int MinFirstClassCount()
        {
            return 1;
        }


        public static CheckInTime FindCheckInTime()
        {
            DateTime datetime = DateTime.Now;
            DayOfWeek day = datetime.DayOfWeek;

            List<CheckInTime> checkInTime = GetCheckInTimeList();
            foreach (CheckInTime time in checkInTime)
            {
                if (time.Day == day && datetime.TimeOfDay > time.StartTime && datetime.TimeOfDay < time.EndTime)
                {
                    return time;
                }
            }
            return default(CheckInTime);
        }

        public static CheckInTime FindCheckInTime(int id)
        {
            List<CheckInTime> checkInTime = GetCheckInTimeList();
            foreach (CheckInTime time in checkInTime)
            {
                if (time.TimeId == id)
                {
                    return time;
                }
            }
            return default(CheckInTime);
        }

        public static CheckInTime FindCheckInTime(string name)
        {
            List<CheckInTime> checkInTime = GetCheckInTimeList();
            foreach (CheckInTime time in checkInTime)
            {
                if (time.TimeName == name)
                {
                    return time;
                }
            }
            return default(CheckInTime);
        }

        public static List<CheckInTime> GetCheckInTimeList()
        {
            List<CheckInTime> result = new List<CheckInTime>();
            for (int i = 1; i <= 5; i++)
            {
                result.Add(new CheckInTime((DayOfWeek)i, 110, string.Format("第{0}天第{1}节", i, 1), i * 10 + 1, generateTime(7, 50, 00)));
                result.Add(new CheckInTime((DayOfWeek)i, 130, string.Format("第{0}天第{1}节", i, 2), i * 10 + 2, generateTime(9, 40, 00)));
                result.Add(new CheckInTime((DayOfWeek)i, 80, string.Format("第{0}天第{1}节", i, 3), i * 10 + 3, generateTime(11, 50, 00)));
                result.Add(new CheckInTime((DayOfWeek)i, 120, string.Format("第{0}天第{1}节", i, 4), i * 10 + 4, generateTime(13, 10, 00)));
                result.Add(new CheckInTime((DayOfWeek)i, 110, string.Format("第{0}天第{1}节", i, 5), i * 10 + 5, generateTime(15, 10, 00)));
                result.Add(new CheckInTime((DayOfWeek)i, 90, string.Format("第{0}天第{1}节", i, 6), i * 10 + 6, generateTime(18, 00, 00)));
                result.Add(new CheckInTime((DayOfWeek)i, 120, string.Format("第{0}天第{1}节", i, 7), i * 10 + 7, generateTime(19, 30, 00)));
            }
            for (int i = 6; i <= 7; i++)
            {
                result.Add(new CheckInTime(i == 7 ? 0 : (DayOfWeek)i, 150, string.Format("第{0}天第{1}节", i, 1), i * 10 + 1, generateTime(9, 30, 00)));
                result.Add(new CheckInTime(i == 7 ? 0 : (DayOfWeek)i, 120, string.Format("第{0}天第{1}节", i, 2), i * 10 + 2, generateTime(12, 00, 00)));
                result.Add(new CheckInTime(i == 7 ? 0 : (DayOfWeek)i, 120, string.Format("第{0}天第{1}节", i, 3), i * 10 + 3, generateTime(14, 00, 00)));
                result.Add(new CheckInTime(i == 7 ? 0 : (DayOfWeek)i, 120, string.Format("第{0}天第{1}节", i, 4), i * 10 + 4, generateTime(16, 00, 00)));
                result.Add(new CheckInTime(i == 7 ? 0 : (DayOfWeek)i, 90, string.Format("第{0}天第{1}节", i, 5), i * 10 + 5, generateTime(18, 00, 00)));
                result.Add(new CheckInTime(i == 7 ? 0 : (DayOfWeek)i, 120, string.Format("第{0}天第{1}节", i, 6), i * 10 + 6, generateTime(19, 30, 00)));
            }
            return result;
        }

        private static List<TimeSpan> generateTime(int hour, int min, int sec)
        {
            List<TimeSpan> result = new List<TimeSpan>();
            result.Add(new TimeSpan(hour, min - 5, sec));
            result.Add(new TimeSpan(hour, min + 5, sec));
            result.Add(new TimeSpan(hour, min + 25, sec));
            return result;
        }
    }

    public struct CheckInTime
    {
        public DayOfWeek Day { get; set; }
        public int TotalTime { get; set; }
        public string TimeName { get; set; }
        public int TimeId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan LateTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public CheckInTime(DayOfWeek day, int totalTime, string timeName, int timeId, TimeSpan startTime, TimeSpan lateTime, TimeSpan endTime)
        {
            Day = day;
            TotalTime = totalTime;
            TimeName = timeName;
            TimeId = timeId;
            StartTime = startTime;
            LateTime = lateTime;
            EndTime = endTime;
        }

        public CheckInTime(DayOfWeek day, int totalTime, string timeName, int timeId, List<TimeSpan> time)
        {
            Day = day;
            TotalTime = totalTime;
            TimeName = timeName + " - " + time[0].ToString(@"hh\:mm");
            TimeId = timeId;
            if (time.Count != 3)
            {
                throw new InvalidOperationException("错误的Time列表，请检查输入！");
            }
            StartTime = time[0];
            LateTime = time[1];
            EndTime = time[2];
        }
    }

    public class AvailableTime
    {
        public Guid AvailableTimeId { get; set; }

        public string TimeName { get; set; }

        public int TimeId { get; set; }

        public virtual Manager Manager { get; set; }

        public DemandType Demand { get; set; }
    }

    public class Log
    {
        public Guid LogId { get; set; }

        public string LogContent { get; set; }

        public DateTime LogTime { get; set; }

        public static void ReportException(Exception e)
        {
            using (BaseDbContext db = new BaseDbContext())
            {
                db.Logs.Add(new Log(string.Format("异常：[{0}]，内部异常：[{1}]，发生在[{2}]，详细信息[{3}]。", e.Message, e.InnerException == null ? "无" : e.InnerException.Message, e.TargetSite, e.ToString())));
                db.SaveChanges();
                return;
            }
        }

        public Log(string logContent)
        {
            LogId = Guid.NewGuid();
            LogContent = logContent;
            LogTime = DateTime.Now;
        }
    }

    public class Status
    {
        public Guid StatusId { get; set; }

        public string Name { get; set; }

        public string TimeName { get; set; }
    }

    public enum DemandType
    {
        First,
        Second,
        Third
    }
}