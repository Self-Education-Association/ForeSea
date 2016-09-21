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

namespace LST.Controllers
{
    [Authorize]
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

            return View();
        }

        public ActionResult Records()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).SingleOrDefault();

            if (user == null)
                return new HttpStatusCodeResult(403);

            List<TestRecordsViewModel> model = new List<TestRecordsViewModel>();

            foreach (var item in user.RecordsCollection)
            {
                model.Add(new TestRecordsViewModel
                {
                    Id = item.Match.Id,
                    Name = item.Match.Name,
                    Enabled = user.Enabled,
                    Applied = user.Applied,
                    Canceled = item.Match.Enabled,
                    Score = item.Score
                });
            }
            return View(model);
        }

        public ActionResult Account()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (user == null)
                return new HttpStatusCodeResult(403);

            var model = new TestAccountViewModel { Enabled = user.Enabled, Applied = user.Applied };

            return View(model);
        }

        public ActionResult Matches()
        {
            var matches = db.TestMatches
                .Where(t => t.StartTime <= DateTime.Now && t.EndTime >= DateTime.Now)
                .Where(t => t.Visible == true)
                .OrderBy(r => r.Name)
                .ToList();

            List<TestIndexViewModel> model = new List<TestIndexViewModel>();
            foreach (var item in matches)
            {
                model.Add(new TestIndexViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Count = item.Count,
                    Limit = item.Limit,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                    Enabled = item.Enabled
                });
            }
            model.OrderBy(r => r.Name);

            return View(model);
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
