using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manager.Models
{
    public interface IManager
    {
        string Name { get; }
        string AccountName { get; }
        List<AvailableTime> AvailableTimes { get; }
        int MinCount { get; }
        int MaxCount { get; }
        int TotalCount { get; }
        int TotalTime { get; }
        int LateTime { get; }
        bool SetAvailableTime(List<AvailableTime> time);
        bool CheckIn(Manager.CheckInTime time);
        bool CheckOut();
    }

    public interface IAvailableTime
    {
        int TimeId { get; }
        Manager Manager { get; }
        DemandType Demand { get; set; }
    }

    public enum DemandType
    {
        First,
        Second,
        Third
    }
}