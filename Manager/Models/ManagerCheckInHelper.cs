using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manager.Models
{
    public class ManagerTableHelper
    {
        List<AvailableTime> availableTimeList;
        List<ManageTable> manageTableList = new List<ManageTable>();

        public class ManageTableRecord
        {
            public CheckInTime Time { get; set; }

            public Manager Manager { get; set; }

            public DemandType Demand { get; set; }
        }

        public class ManageTable : IEnumerable<ManageTableRecord>
        {
            private List<ManageTableRecord> list = new List<ManageTableRecord>();

            public double Happiness
            {
                get
                {
                    double happiness = 0;
                    int firstHappiness = 100;
                    int secondHappiness = 80;
                    int thirdHappiness = 60;
                    int happinessDiscount = 5;
                    List<Manager> managerList = new List<Manager>();
                    foreach (var item in list)
                    {
                        if (!managerList.Contains(item.Manager))
                        {
                            managerList.Add(item.Manager);
                        }
                    }
                    foreach (var item in managerList)
                    {
                        double managerHappiness = 0;
                        var recordList = GetManagerManageTable(item);
                        foreach (var record in recordList)
                        {
                            switch (record.Demand)
                            {
                                case DemandType.First:
                                    managerHappiness += firstHappiness;
                                    break;
                                case DemandType.Second:
                                    managerHappiness += secondHappiness;
                                    break;
                                case DemandType.Third:
                                    managerHappiness += thirdHappiness;
                                    break;
                            }
                            managerHappiness -= recordList.Count * happinessDiscount;
                        }
                        happiness += managerHappiness / recordList.Count;
                    }
                    return happiness / managerList.Count;
                }
            }

            public bool Usable
            {
                get
                {
                    foreach (var item in this)
                    {
                        if (item.Manager == null)
                            return false;
                    }
                    return true;
                }
            }

            public ManageTableRecord this[int index]
            {
                get
                {
                    foreach (var item in list)
                    {
                        if (item.Time.TimeId == index)
                        {
                            return item;
                        }
                    }
                    return null;
                }
                set
                {
                    if (this[index].Manager == null)
                    {
                        this[index].Manager = value.Manager;
                        this[index].Demand = value.Demand;
                    }
                }
            }

            public ManageTable(List<CheckInTime> timeTable)
            {
                foreach (var item in timeTable)
                {
                    list.Add(new ManageTableRecord { Time = item });
                }
            }

            private List<ManageTableRecord> GetManagerManageTable(Manager manager)
            {
                return list.Where(m => m.Manager == manager).ToList();
            }

            public IEnumerator<ManageTableRecord> GetEnumerator()
            {
                return ((IEnumerable<ManageTableRecord>)list).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<ManageTableRecord>)list).GetEnumerator();
            }
        }

        public ManagerTableHelper(List<AvailableTime> availableTimeList)
        {
            this.availableTimeList = availableTimeList;
        }

        /// <summary>
        /// 获取Manager为空的可用ManageTable（由ManageTableRecord组成的集合类型）。
        /// </summary>
        /// <returns>Manager为空而包含值班时间的ManageTable</returns>
        public static ManageTable GetEmptyManageTable()
        {
            return new ManageTable(CheckInTime.CheckInTimeList);
        }

        public List<ManageTable> GetManageTableList()
        {
            fillInManageTable(GetEmptyManageTable());
            return manageTableList;
        }

        private void fillInManageTable(ManageTable table)
        {
            for (int i = 11; ; i = getNextTimeId(i))
            {
                List<ManageTableRecord> recordList = new List<ManageTableRecord>();
                foreach (var availableTime in availableTimeList)
                {
                    if (availableTime.TimeId == i)
                    {
                        recordList.Add(new ManageTableRecord { Manager = availableTime.Manager, Demand = availableTime.Demand, Time = Manager.FindCheckInTime(availableTime.TimeId) });
                    }
                }
                List<ManageTable> newTableList = new List<ManageTable>();
                foreach (var record in recordList)
                {
                    if (manageTableList.Count == 0)
                    {
                        var initialManageTable = GetEmptyManageTable();
                        initialManageTable[i] = record;
                        newTableList.Add(initialManageTable);
                    }
                    else
                    {
                        foreach (var manageTable in manageTableList)
                        {
                            manageTable[i] = record;
                            newTableList.Add(manageTable);
                        }
                    }
                }
                manageTableList = newTableList;
                if (i == 76)
                {
                    break;
                }
            }
        }

        private int getNextTimeId(int index)
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
                if (j < 6)
                {
                    return index + 1;
                }
                return (i + 1) * 10 + 1;
            }
            if (i == 7)
            {
                if (j < 6)
                {
                    return index + 1;
                }
                return 11;
            }
            return 0;
        }
    }
    public class AvailableTimeHelper
    {
        List<AvailableTime> availableTimeList;

        public class AvailableTimeTableRecord
        {
            public CheckInTime Time { get; set; }

            public List<Manager> ManagerList { get; set; }

            public int Count { get { return ManagerList.Count; } }

            public AvailableTimeTableRecord(CheckInTime time)
            {
                Time = time;
                ManagerList = new List<Manager>();
            }
        }

        public class AvailableTimeTable
        {
            List<AvailableTimeTableRecord> data = new List<AvailableTimeTableRecord>();

            public AvailableTimeTableRecord this[int index]
            {
                get
                {
                    return data.Where(a => a.Time.TimeId == index).SingleOrDefault();
                }
            }

            public AvailableTimeTable(List<AvailableTime> list)
            {
                emptyData();
                foreach (var item in list)
                {
                    this[item.TimeId].ManagerList.Add(item.Manager);
                }
            }

            void emptyData()
            {
                foreach (var item in CheckInTime.CheckInTimeList)
                {
                    data.Add(new AvailableTimeTableRecord(item));
                }
            }
        }

        public AvailableTimeHelper(List<AvailableTime> list)
        {
            availableTimeList = list;
        }

        public AvailableTimeTable GetAvailableTimeTable()
        {
            return new AvailableTimeTable(availableTimeList);
        }
    }
}