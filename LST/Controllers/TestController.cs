using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LST.Models;

namespace LST.Controllers
{
    public class TestController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Test
        public ActionResult Index(MessageType message = MessageType.None)
        {
            ViewBag.ErrorInfo =
                message == MessageType.ApplySuccess ? "场次选择成功" :
            message == MessageType.ApplyFailure ? "场次选择失败" :
            message == MessageType.QuitSuccess ? "取消报名成功" :
            message == MessageType.QuitFailure ? "取消报名失败" : "";

            var user = db.Users.Where(u => u.UserName == User.Identity.Name).Single();

            if (!user.Applied)
            {
                return View(db.TestMatches.Where(t => t.StartTime <= DateTime.Now && t.EndTime >= DateTime.Now));
            }
            else
            {
                return View("MatchInfo", user.RecordsCollection);
            }
        }

        public ActionResult Apply(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(400);
            }
            var match = db.TestMatches.Find(id);
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).Single();
            if (match == null || user == null)
            {
                return new HttpStatusCodeResult(404);
            }

            var helper = new TestHelper();
            if (helper.ApplyMatch(match, user))
            {
                return RedirectToAction("Index", new { message = MessageType.ApplySuccess });
            }
            else
            {
                return RedirectToAction("Index", new { message = MessageType.ApplyFailure });
            }
        }

        public ActionResult Quit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(400);
            }
            var match = db.TestMatches.Find(id);
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).Single();
            if (match == null || user == null)
            {
                return new HttpStatusCodeResult(404);
            }

            var helper = new TestHelper();
            if (helper.QuitMatch(match, user))
            {
                return RedirectToAction("Index", new { message = MessageType.ApplySuccess });
            }
            else
            {
                return RedirectToAction("Index", new { message = MessageType.ApplyFailure });
            }
        }

        public enum MessageType
        {
            ApplySuccess,
            ApplyFailure,
            QuitSuccess,
            QuitFailure,
            None
        }
    }
}