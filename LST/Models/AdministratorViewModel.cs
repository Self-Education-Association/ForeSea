/*
Copyright [2016] [puyang.c@foxmail.com]

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LST.Models
{
    public class GenerateMatchesViewModel
    {
        [DisplayName("开放日期")]
        [DataType(DataType.MultilineText)]
        public string Days { get; set; }

        [DisplayName("每天开放节次")]
        [DataType(DataType.MultilineText)]
        public string Lessons { get; set; }

        [DisplayName("每节限制人数")]
        public int Limit { get; set; }

        [DisplayName("报名开始时间")]
        public DateTime StartTime { get; set; }

        [DisplayName("报名结束时间")]
        public DateTime EndTime { get; set; }
    }
}