using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LST.Models
{
    public class GenerateMatchesViewModel
    {
        [DisplayName("开放日期")]
        public string Days { get; set; }

        [DisplayName("每天开放节次")]
        public string Lessons { get; set; }

        [DisplayName("每节限制人数")]
        public int Limit { get; set; }

        [DisplayName("报名开始时间")]
        public DateTime StartTime { get; set; }

        [DisplayName("报名结束时间")]
        public DateTime EndTime { get; set; }
    }
}