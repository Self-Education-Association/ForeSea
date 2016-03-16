using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LST.Models
{
    public class TestRecordsViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "场次名称")]
        public string Name { get; set; }

        [Display(Name = "报名功能")]
        public bool Enabled { get; set; }

        [Display(Name = "本次考试")]
        public bool Applied { get; set; }

        [Display(Name = "可取消")]
        public bool Canceled { get; set; }

        [Display(Name = "分数")]
        public string Score { get; set; }
    }

    public class TestIndexViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "场次名称")]
        public string Name { get; set; }

        [Display(Name = "当前人数")]
        public int Count { get; set; }

        [Display(Name = "限制人数")]
        public int Limit { get; set; }

        [Display(Name = "开始时间")]
        public DateTime StartTime { get; set; }

        [Display(Name = "结束时间")]
        public DateTime EndTime { get; set; }

        [Display(Name = "可报名")]
        public bool Enabled { get; set; }
    }

    public class TestAccountViewModel
    {
        public bool Enabled { get; set; }

        public bool Applied { get; set; }
    }
}