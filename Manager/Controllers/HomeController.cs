using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Manager.Models;
using System.DirectoryServices;

namespace Manager.Controllers
{
    public class HomeController : Controller
    {
        BaseDbContext db = new BaseDbContext();

        public ActionResult Index(string id="")
        {
            if (!string.IsNullOrWhiteSpace(id))
            {

            }

            return View();
        }
    }
}