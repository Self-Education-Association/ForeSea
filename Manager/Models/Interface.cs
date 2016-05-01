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
        List<IAvailableTime> AvailableTime { get; }
        int MinCount { get; }
        int MaxCount { get; }
        int TotalCount { get; }
        decimal TotalTime { get; }
        decimal LateTime { get; }
        bool SetAvailableTime(List<IAvailableTime> time);
        bool CheckIn();
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