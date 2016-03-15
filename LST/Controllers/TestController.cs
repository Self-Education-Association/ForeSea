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
            List<TestViewModel> model;

            if (!user.Applied)
            {
                return View(db.TestMatches.Where(t => t.StartTime <= DateTime.Now && t.EndTime >= DateTime.Now));
            }
            else
            {
                model = new List<TestViewModel>();
                foreach (var item in user.RecordsCollection)
                {
                    model.Add(new TestViewModel
                    {
                        Id = item.Match.Id,
                        Name = item.Match.Name,
                        StartTime = item.Match.StartTime,
                        EndTime = item.Match.EndTime,
                        Enabled = item.Match.Enabled,
                        Score = item.Score
                    });
                }
            }
            return View("MatchInfo", model);
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
                return RedirectToAction("Index", new { message = MessageType.QuitSuccess });
            }
            else
            {
                return RedirectToAction("Index", new { message = MessageType.QuitFailure });
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
