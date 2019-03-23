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

            //三个检查全部返回true则为true，否则返回false。
            return MinAvailableTimeCheck() && MinFirstClassCheck() && MinWorkDayCheck();
        }

        /// <summary>
        /// 返回至少有指定节数的可值班时间。
        /// </summary>
        /// <returns></returns>
        public int MinAvailableTimeCount()
        {
            int times = 6;
            return MinCount * times;
        }

        /// <summary>
        /// 判断是否至少有指定节数的可值班时间，不符合则返回false。
        /// </summary>
        /// <returns></returns>
        public bool MinAvailableTimeCheck()
        {
            return AvailableTimes.Count >= MinAvailableTimeCount();
        }

        /// <summary>
        /// 判断是否至少有指定节数的早班，不符合则返回false。
        /// </summary>
        /// <returns></returns>
        public bool MinFirstClassCheck()
        {
            int minFirstClass = 1;
            int firstClassCount = 0;
            foreach (var item in AvailableTimes)
            {
                if (item.TimeId % 10 == 1)
                {
                    firstClassCount++;
                }
            }
            return firstClassCount >= minFirstClass;
        }

        /// <summary>
        /// 判断是否至少有指定节数的早班，不符合则返回false。
        /// </summary>
        /// <returns></returns>
        public bool MinWorkDayCheck()
        {
            int minWorkDay = 5;
            int workDayCount = 0;
            foreach (var item in AvailableTimes)
            {
                if (item.TimeId / 10 <= 5)
                {
                    workDayCount++;
                }
            }
            return workDayCount >= minWorkDay;
        }

        List<double> demandMark = new List<double>();

        /// <summary>
        /// 返回标准化后的需求分数
        /// </summary>
        /// <returns>降序排列的需求分数列表</returns>
        public List<double> GetDemandMark()
        {
            if (demandMark.Count == 3)
            {
                return demandMark;
            }
            var data = new List<double>();
            foreach (var item in AvailableTimes)
            {
                switch (item.Demand)
                {
                    case DemandType.First:
                        data.Add(100);
                        break;
                    case DemandType.Second:
                        data.Add(80);
                        break;
                    case DemandType.Third:
                        data.Add(60);
                        break;
                    default:
                        break;
                }
            }
            double std = 0;
            double avg = 0;
            foreach (var item in data)
            {
                avg += item;
            }
            avg /= data.Count;
            foreach (var item in data)
            {
                std += Math.Pow(item - avg, 2);
            }
            std = Math.Sqrt(std);
            var result = new List<double>();
            if (std != 0)
            {
                var temp = new List<double>();
                foreach (var item in data)
                {
                    temp.Add((item - avg) / std);
                }
                temp = temp.OrderBy(t => t).ToList();
                result.Add(temp[0]);
                result.Add(temp[temp.Count / 2]);
                result.Add(temp[temp.Count - 1]);
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    result.Add(data[0] - avg);
                }
            }
            result = result.OrderByDescending(t => t).ToList();
            demandMark = result;
            return result;
        }

        public static CheckInTime FindCheckInTime()
        {
            DateTime datetime = DateTime.Now;
            DayOfWeek day = datetime.DayOfWeek;

            List<CheckInTime> checkInTime = CheckInTime.CheckInTimeList;
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
            List<CheckInTime> checkInTime = CheckInTime.CheckInTimeList;
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
            List<CheckInTime> checkInTime = CheckInTime.CheckInTimeList;
            foreach (CheckInTime time in checkInTime)
            {
                if (time.TimeName == name)
                {
                    return time;
                }
            }
            return default(CheckInTime);
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

        public readonly static List<CheckInTime> CheckInTimeList = GetCheckInTimeList();

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

        public static int GetNextTimeId(int index)
        {
            int i = index / 10;
            int j = index % 10;
            if (i <= 0 || j <= 0 || i > 7 || j > 7)
            {
                return 0;
            }
            if (i <= 5)
            {
                if (j < 7)
                {
                    return index + 1;
                }
                return (i + 1) * 10 + 1;
            }
            if (i == 6)
            {
                if (j < 4)
                {
                    return index + 1;
                }
                return 71;
            }
            if (i == 7)
            {
                if (j < 4)
                {
                    return index + 1;
                }
                return 0;
            }
            return 0;
        }

        static List<CheckInTime> GetCheckInTimeList()
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
                result.Add(new CheckInTime(i == 7 ? 0 : (DayOfWeek)i, 120, string.Format("第{0}天第{1}节", i, 1), i * 10 + 1, generateTime(8, 50, 00)));
                result.Add(new CheckInTime(i == 7 ? 0 : (DayOfWeek)i, 120, string.Format("第{0}天第{1}节", i, 2), i * 10 + 2, generateTime(11, 00, 00)));
                result.Add(new CheckInTime(i == 7 ? 0 : (DayOfWeek)i, 120, string.Format("第{0}天第{1}节", i, 3), i * 10 + 3, generateTime(13, 00, 00)));
                result.Add(new CheckInTime(i == 7 ? 0 : (DayOfWeek)i, 120, string.Format("第{0}天第{1}节", i, 4), i * 10 + 4, generateTime(15, 00, 00)));
                //result.Add(new CheckInTime(i == 7 ? 0 : (DayOfWeek)i, 90, string.Format("第{0}天第{1}节", i, 5), i * 10 + 5, generateTime(18, 00, 00)));
                //result.Add(new CheckInTime(i == 7 ? 0 : (DayOfWeek)i, 120, string.Format("第{0}天第{1}节", i, 6), i * 10 + 6, generateTime(19, 30, 00)));
            }
            return result;
        }

        static List<TimeSpan> generateTime(int hour, int min, int sec)
        {
            List<TimeSpan> result = new List<TimeSpan>();
            result.Add(new TimeSpan(hour, min - 5, sec));
            result.Add(new TimeSpan(hour, min + 5, sec));
            result.Add(new TimeSpan(hour, min + 15, sec));//迟到15min算旷班
            return result;
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