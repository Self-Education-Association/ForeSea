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
using LST.Models;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;

namespace LST.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            /*List<TestMatch> matches;
            using (var db = new ApplicationDbContext())
            {
                if (!db.Database.Exists())
                {
                    db.Database.Create();
                }
                DateTime time = DateTime.Now;
                matches = db.TestMatches.Where(t => t.Visible == true).ToList();
            }
            return View(matches);*/
            return Redirect("~/Test");
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult StudentNumber([Bind(Include = "StudentNumber,StudentName")]RegisterViewModel model)
        {
            string message = null;
            if (!string.IsNullOrEmpty(model.StudentNumber))
            {
                using (var db = new ApplicationDbContext())
                {
                    if (db.Users.Where(u => u.StudentNumber == model.StudentNumber).Count() != 0)
                    {
                        message = "此学号已经注册。";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var con = new SqlConnection(@"Server=10.1.1.68\seasqlserver;Database=ForeSea;User ID=checkin;Password=seacheckin;Trusted_Connection=false;");
                        var cmd = new SqlCommand("", con);
                        cmd.CommandText = @"SELECT * FROM Student WHERE ID=@Id AND Name=@Name";
                        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.VarChar));
                        cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar));
                        cmd.Parameters["@Id"].Value = model.StudentNumber;
                        cmd.Parameters["@Name"].Value = model.StudentName;
                        con.Open();
                        object result;
                        try
                        {
                            result = cmd.ExecuteScalar();
                        }
                        finally
                        {
                            con.Close();
                        }
                        if (result != null)
                        {
                            return Json(true, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            message = "数据库无匹配记录。";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            else
            {
                message = "错误的数据。";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}