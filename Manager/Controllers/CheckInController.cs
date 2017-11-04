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
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Manager.Models;
using System.Data.SqlClient;

namespace Manager.Controllers
{
    [CheckLogin]
    public class CheckInController : Controller
    {
        // GET: CheckIn
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddCheckList(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            SqlConnection conn = new SqlConnection(@"Server=10.1.1.68\seasqlserver;Database=ForeSea;User ID=checkin;Password=seacheckin;Trusted_Connection=false;");
            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandText = string.Format(@"INSERT INTO ForeSea.dbo.CheckIn_CheckList values({0})", id);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            TempData["Alert"] = "添加成功！";
            return RedirectToAction("Index");
            //edit
        }
    }
}