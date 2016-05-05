using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Manager.Models
{
    public class ManageTableController : Controller
    {
        static BaseDbContext db = new BaseDbContext();
        static ManagerTableHelper.ManageTable manageTable = null;
        // GET: ManageTable
        public ActionResult Index()
        {
            var availableTimeTable = new AvailableTimeHelper.AvailableTimeTable(db.AvailableTime.ToList());
            return View(availableTimeTable);
        }

        public ActionResult FillIn(int id, string name)
        {
            if (!Request.IsAjaxRequest())
            {
                return new HttpStatusCodeResult(403);
            }
            if (manageTable == null)
            {
                return new HttpStatusCodeResult(403);
            }
            manageTable[id].Manager = db.Managers.Where(m => m.Name == name).SingleOrDefault();
            return RedirectToAction("Index");
        }

        public ActionResult Clean()
        {
            manageTable = ManagerTableHelper.GetEmptyManageTable(db.AvailableTime.ToList());
            return RedirectToAction("Index");
        }

        public ActionResult ManageTable()
        {
            if (manageTable == null)
            {
                manageTable = ManagerTableHelper.GetEmptyManageTable(db.AvailableTime.ToList());
            }
            return View(manageTable);
        }
    }
}