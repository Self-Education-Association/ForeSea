using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manager.Models
{
    public class ManagerCheckInHelper
    {
        List<AvailableTime> availableTimeList;
        List<ManageTable> manageTableList = new List<ManageTable>();

        public class ManageTableRecord
        {
            public CheckInTime Time { get; set; }

            public Manager Manager { get; set; }
        }

        public class ManageTable : IEnumerable<ManageTableRecord>
        {
            private List<ManageTableRecord> list = new List<ManageTableRecord>();

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

            public ManageTableRecord this[int rowIndex]
            {
                get
                {
                    foreach (var item in list)
                    {
                        if (item.Time.TimeId == rowIndex)
                        {
                            return item;
                        }
                    }
                    return null;
                }
            }

            public ManageTable(List<CheckInTime> timeTable)
            {
                foreach (var item in timeTable)
                {
                    list.Add(new ManageTableRecord { Time = item });
                }
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

        public ManagerCheckInHelper(List<AvailableTime> availableTimeList)
        {
            this.availableTimeList = availableTimeList;
        }

        /// <summary>
        /// 获取Manager为空的可用ManageTable（由ManageTableRecord组成的集合类型）。
        /// </summary>
        /// <returns>Manager为空而包含值班时间的ManageTable</returns>
        public static ManageTable GetEmptyManageTable()
        {
            return new ManageTable(Manager.GetCheckInTimeList());
        }

        public List<ManageTable> GetManageTableList()
        {
            fillInManageTable(11, GetEmptyManageTable());
            return manageTableList;
        }

        private void fillInManageTable(int index, ManageTable table)
        {
            foreach (var item in availableTimeList)
            {
                if (item.TimeId == index)
                {
                    table[index].Manager = item.Manager;
                    if (table.Usable == true)
                    {
                        manageTableList.Add(table);
                    }
                    fillInManageTable(getNextTimeId(index), table);
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
}