using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Manager.Models;
using System.Text.RegularExpressions;

namespace Manager.Controllers
{
    [CheckLogin]
    public class AvailableTimesController : Controller
    {
        private BaseDbContext db = new BaseDbContext();

        public ActionResult Index()
        {
            var manager = GetManager();
            if (manager == null)
            {
                return RedirectToAction("Login", "Account");
            }


            return View(manager.AvailableTimes.ToList());
        }
        
        // GET: AvailableTimes
        public ActionResult List()
        {
            var manager = GetManager();
            ViewBag.Available = manager.CheckAvailableTime();
            ViewBag.MinAvailableTimeCount = manager.MinAvailableTimeCount();
            if (manager == null)
            {
                return RedirectToAction("Login", "Account");
            }


            return View(manager.AvailableTimes.OrderBy(a => a.Demand).ThenBy(a => a.TimeId).ToList());
        }

        // GET: AvailableTimes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AvailableTimes/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AvailableTime availableTime)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var manager = GetManager();
                    if (manager.AvailableTimes.Where(a => a.TimeName == availableTime.TimeName).Count() != 0)
                    {
                        TempData["Alert"] = "你已经添加了这个可值班时间了！";
                        return RedirectToAction("Index");
                    }
                    availableTime.TimeId = Models.Manager.FindCheckInTime(availableTime.TimeName).TimeId;
                    availableTime.AvailableTimeId = Guid.NewGuid();
                    availableTime.Manager = GetManager();
                    db.AvailableTime.Add(availableTime);
                    db.Logs.Add(new Log(string.Format("系统：[添加]值班员[{1}]于[{0}]的可值班时间纪录。", availableTime.TimeName, manager.Name)));
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {

                Log.ReportException(e);
            }

            return RedirectToAction("Index");
        }

        // GET: AvailableTimes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AvailableTime availableTime = db.AvailableTime.Find(id);
            if (availableTime == null)
            {
                return HttpNotFound();
            }
            return View(availableTime);
        }

        // POST: AvailableTimes/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AvailableTime availableTime)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(availableTime).State = EntityState.Modified;
                    db.Logs.Add(new Log(string.Format("系统：[修改]值班员[{1}]于[{0}]的可值班时间纪录。", availableTime.TimeName, GetManager().Name)));
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Log.ReportException(e);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid id)
        {
            try
            {
                AvailableTime availableTime = db.AvailableTime.Find(id);
                db.AvailableTime.Remove(availableTime);
                db.Logs.Add(new Log(string.Format("系统：[移除]值班员[{1}]于[{0}]的可值班时间纪录。", availableTime.TimeName, GetManager().Name)));
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Log.ReportException(e);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Ajax使用的添加Action，对于非Ajax请求会返回403错误。
        /// </summary>
        /// <param name="id">CheckInTime.TimeId</param>
        /// <returns>200, 403, 404</returns>
        public ActionResult EasyCreate(int id)
        {
            if (!Request.IsAjaxRequest())
            {
                return new HttpStatusCodeResult(403);
            }
            var time = Models.Manager.FindCheckInTime(id);
            if (time.Equals(default(CheckInTime)))
            {
                return new HttpStatusCodeResult(404);
            }
            return Create(new AvailableTime { Demand = DemandType.Second, TimeName = time.TimeName });
        }

        /// <summary>
        /// Ajax使用的删除Action，对于非Ajax请求会返回403错误。
        /// </summary>
        /// <param name="id">CheckInTime.TimeId</param>
        /// <returns>200, 403, 404</returns>
        public ActionResult EasyDelete(int id)
        {
            if (!Request.IsAjaxRequest())
            {
                return new HttpStatusCodeResult(403);
            }
            var manager = GetManager();
            var time = manager.AvailableTimes.Where(a => a.TimeId == id).SingleOrDefault();
            if (time.Equals(default(CheckInTime)))
            {
                return new HttpStatusCodeResult(404);
            }
            return Delete(time.AvailableTimeId);
        }

        public ActionResult ManagerList()
        {
            return View(db.Managers.ToList());
        }

        private Models.Manager GetManager()
        {
            string managerName = Session["User"].ToString();
            return db.Managers.Where(m => m.AccountName == managerName).SingleOrDefault();
        }


        public ActionResult ManageTableList()
        {
            var manageCheckInHelper = new ManagerTableHelper(db.AvailableTime.ToList());
            var tableList = manageCheckInHelper.GetManageTableList().OrderBy(t => t.Usable).Take(5).ToList();
            return View(tableList);
        }

        public ActionResult AvailableTimeTable()
        {
            var availableTimeHelper = new AvailableTimeHelper(db.AvailableTime.ToList());
            var availableTimeTable = availableTimeHelper.GetAvailableTimeTable();
            return View(availableTimeTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
