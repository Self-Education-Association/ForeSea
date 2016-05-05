using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
                    int happinessDiscount = 5;
                    List<Manager> managerList = new List<Manager>();
                    foreach (var item in list)
                    {
                        if (!managerList.Contains(item.Manager))
                        {
                            managerList.Add(item.Manager);
                        }
                    }
                    managerList.Remove(null);
                    foreach (var item in managerList)
                    {
                        double firstHappiness = item.GetDemandMark()[0] * 30 + 70;
                        double secondHappiness = item.GetDemandMark()[1] * 30 + 70;
                        double thirdHappiness = item.GetDemandMark()[2] * 30 + 70;
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
                    return this.Where(t => t.Time.TimeId == index).SingleOrDefault();
                }
                set
                {
                    this[index].Manager = value.Manager;
                    this[index].Demand = value.Demand;
                }
            }

            public ManageTable(List<CheckInTime> timeTable)
            {
                foreach (var item in timeTable)
                {
                    list.Add(new ManageTableRecord { Time = item });
                }
            }

            public List<ManageTableRecord> GetManagerManageTable(Manager manager)
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

            public ManageTable Clone()
            {
                ManageTable result = new ManageTable(CheckInTime.CheckInTimeList);
                foreach (var item in this)
                {
                    result[item.Time.TimeId] = item;
                }
                return result;
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
        public static ManageTable GetEmptyManageTable(List<AvailableTime> availableTimeList)
        {
            var manageTable = new ManageTable(CheckInTime.CheckInTimeList);
            var availableTimeTable = new AvailableTimeHelper.AvailableTimeTable(availableTimeList);
            for (int i = 11; i != 0; i = CheckInTime.GetNextTimeId(i))
            {
                if (availableTimeTable[i].ManagerList.Count == 0)
                {
                    return null;
                }
                if (availableTimeTable[i].ManagerList.Count == 1)
                {
                    manageTable[i].Manager = availableTimeTable[i].ManagerList.SingleOrDefault();
                }
            }
            return manageTable;
        }

        public List<ManageTable> GetManageTableList()
        {
            fillInManageTable(GetEmptyManageTable(availableTimeList));
            return manageTableList;
        }

        private void fillInManageTable(ManageTable table)
        {
            var availableTimeTable = new AvailableTimeHelper.AvailableTimeTable(availableTimeList);
            List<int> jumpList = new List<int>();
            manageTableList = new List<ManageTable>();
            manageTableList.Add(new ManageTable(CheckInTime.CheckInTimeList));
            for (int i = 11; i != 0; i = CheckInTime.GetNextTimeId(i))
            {
                if (availableTimeTable[i].ManagerList.Count == 0)
                {
                    return;
                }
                if (availableTimeTable[i].ManagerList.Count == 1)
                {
                    manageTableList.Single()[i].Manager = availableTimeTable[i].ManagerList.Single();
                    jumpList.Add(i);
                }
            }
            for (int i = 11; i != 0; i = CheckInTime.GetNextTimeId(i))
            {
                if (jumpList.Contains(i))
                {
                    continue;
                }
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
                        var initialManageTable = GetEmptyManageTable(availableTimeList);
                        initialManageTable[i] = record;
                        newTableList.Add(initialManageTable);
                    }
                    else
                    {
                        foreach (var manageTable in manageTableList)
                        {
                            var newManageTable = manageTable.Clone();
                            newManageTable[i] = record;
                            newTableList.Add(newManageTable);
                        }
                    }
                }
                if (newTableList.Count == 0)
                {
                    manageTableList = new List<ManageTable>();
                    return;
                }
                newTableList = newTableList.Distinct(new ManageTableComparer()).ToList();
                manageTableList = checkManageTableList(newTableList);
                if (i == 76)
                {
                    break;
                }
            }
        }

        private List<ManageTable> checkManageTableList(List<ManageTable> list)
        {
            var result = new List<ManageTable>();
            foreach (var table in list)
            {
                foreach (var item in table)
                {
                    if (item.Manager == null)
                    {
                        continue;
                    }
                    int count = table.GetManagerManageTable(item.Manager).Count;
                    if (item.Manager.MinCount >= count || item.Manager.MaxCount <= count)
                    {
                        break;
                    }
                }
                result.Add(table);
            }
            if (result.Count >= 100)
            {
                return result.OrderByDescending(r => r.Happiness).Take(100).ToList();
            }
            return result;
        }

        public class ManageTableComparer : IEqualityComparer<ManageTable>
        {
            public bool Equals(ManageTable x, ManageTable y)
            {
                foreach (var item in x)
                {
                    if (y[item.Time.TimeId].Manager == null)
                    {
                        if (item.Manager != null)
                        {
                            return false;
                        }
                        continue;
                    }
                    if (item.Manager == null)
                    {
                        if (y[item.Time.TimeId].Manager != null)
                        {
                            return false;
                        }
                        continue;
                    }
                    if (y[item.Time.TimeId].Manager.Name != item.Manager.Name)
                    {
                        return false;
                    }
                }
                return true;
            }

            public int GetHashCode(ManageTable obj)
            {
                return obj.ToString().GetHashCode();
            }
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

        public sealed class AvailableTimeTable : IEnumerable<AvailableTimeTableRecord>
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

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<AvailableTimeTableRecord>)data).GetEnumerator();
            }

            IEnumerator<AvailableTimeTableRecord> IEnumerable<AvailableTimeTableRecord>.GetEnumerator()
            {
                return ((IEnumerable<AvailableTimeTableRecord>)data).GetEnumerator();
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