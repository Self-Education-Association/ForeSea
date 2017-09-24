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
using System.DirectoryServices;

namespace Manager.Controllers
{
    [CheckLogin]
    public class AccountController : Controller
    {
        BaseDbContext db = new BaseDbContext();

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
             
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                bool result = TryAuthenticate(model);
                if (result)
                {
                    var manager = db.Managers.Where(m => m.AccountName == model.Account).FirstOrDefault();
                    if (manager == null)
                    {
                        TempData["Alert"] = "AD域验证通过，但数据库无此值班员记录，请与数据库管理员联系！";
                        return RedirectToLocal(returnUrl);
                        return View(model);
                    }
                    Session["User"] = model.Account;
                }
                TempData["Alert"] = "AD域验证失败，请检查用户名和密码是否输入正确！";
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            TempData["Alert"] = "你已退出系统！";
            return RedirectToAction("Login", "Account");
        }

        #region HelpFunctions
        public static bool TryAuthenticate(LoginViewModel model)
        {
            bool isLogin = false;
            try
            {
                DirectoryEntry entry = new DirectoryEntry(string.Format("LDAP://uibe.edu/OU=Users,OU=SEA团队,OU=2014外语自学中心用户,OU=过度,DC=uibe,DC=edu"), model.Account, model.Password);
                entry.RefreshCache();
                isLogin = true;
            }
            catch (Exception)
            {
                isLogin = false;
            }
            return isLogin;
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}