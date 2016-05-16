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
        }
    }
}