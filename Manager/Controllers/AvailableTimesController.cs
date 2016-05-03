using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Manager.Models;

namespace Manager.Controllers
{
    [CheckLogin]
    public class AvailableTimesController : Controller
    {
        private BaseDbContext db = new BaseDbContext();

        // GET: AvailableTimes
        public ActionResult Index()
        {
            var manager = GetManager();
            ViewBag.Available = manager.CheckAvailableTime();
            if (manager == null)
            {
                return RedirectToAction("Login", "Account");
            }


            return View(manager.AvailableTimes);
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
                    availableTime.AvailableTimeId = Guid.NewGuid();
                    availableTime.Manager = GetManager();
                    db.AvailableTime.Add(availableTime);
                    db.Logs.Add(new Log(string.Format("系统：添加值班员[{1}]于[{0}]的可值班时间纪录。", availableTime.TimeName, manager.Name)));
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
                    db.Logs.Add(new Log(string.Format("系统：修改值班员[{1}]于[{0}]的可值班时间纪录（修改后）。", availableTime.TimeName, GetManager().Name)));
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
                db.Logs.Add(new Log(string.Format("系统：移除值班员[{1}]于[{0}]的可值班时间纪录。", availableTime.TimeName, GetManager().Name)));
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Log.ReportException(e);
            }
            return RedirectToAction("Index");
        }

        private Models.Manager GetManager()
        {
            string managerName = Session["User"].ToString();
            return db.Managers.Where(m => m.AccountName == managerName).SingleOrDefault();
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
