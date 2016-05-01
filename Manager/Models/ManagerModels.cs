using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manager.Models
{
    public class Manager : IManager
    {
        public string ManagerId { get; set; }
        public string Name { get; private set; }
        public string AccountName { get; private set; }

        public virtual List<IAvailableTime> AvailableTime { get; private set; }

        public int MinCount { get; private set; }
        public int MaxCount { get; private set; }

        public int TotalCount { get; private set; }
        public decimal TotalTime { get; private set; }
        public decimal LateTime { get; private set; }

        public bool SetAvailableTime(List<IAvailableTime> time)
        {
            bool result = CheckAvailableTime(time);
            if (result)
            {
                AvailableTime = time;
            }
            return result;
        }

        public void SetCount(int min, int max)
        {
            MinCount = min;
            MaxCount = max;
        }

        public bool CheckIn()
        {
            return false;
        }

        public bool CheckOut()
        {
            return false;
        }

        private bool AddTime(decimal total, decimal late)
        {
            TotalTime += total;
            LateTime += late;
            return true;
        }

        private static bool CheckAvailableTime(List<IAvailableTime> time)
        {
            return true;
        }
    }

    public class AvailableTime : IAvailableTime
    {
        public string AvailableTimeId { get; set; }

        public int TimeId { get; }

        public virtual Manager Manager { get; }

        public DemandType Demand { get; set; }
    }

    public class Log
    {
        public string LogId { get; set; }

        public string LogContent { get; set; }
    }

    public class Status
    {
        public string StatusId { get; set; }

        public string AccountName { get; set; }
    }
}