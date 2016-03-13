using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LST.Models
{
    public class OperationRecord
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Operation { get; set; }

        public string TargetName { get; set; }

        public DateTime Time { get; set; }

        public OperationRecord(string userName, string operation, string targetName)
        {
            Id = Guid.NewGuid();
            UserName = userName;
            Operation = operation;
            TargetName = targetName;
            Time = DateTime.Now;
        }
    }
}