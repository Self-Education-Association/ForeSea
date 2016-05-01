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
        public ActionResult Login()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
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
                        return View(model);
                    }
                    Session["User"] = model.Account;
                    return RedirectToAction("Index", "Account");
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
            string domain = "uibe.edu";
            try
            {
                DirectoryEntry entry = new DirectoryEntry(string.Format("LDAP://uibe.edu/OU=Users,OU=SEA团队,OU=2014外语自学中心用户,OU=过度,DC=uibe,DC=edu", domain), model.Account, model.Password);
                entry.RefreshCache();
                isLogin = true;
            }
            catch (Exception)
            {
                isLogin = false;
            }
            return isLogin;
        }
        #endregion
    }
}