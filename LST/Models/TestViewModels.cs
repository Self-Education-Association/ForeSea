using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LST.Models
{
    public class TestViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Enabled { get; set; }
        public string Score { get; set; }
    }
}