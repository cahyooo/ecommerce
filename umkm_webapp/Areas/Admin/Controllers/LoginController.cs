using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using umkm_webapp.Context;
using umkm_webapp.Models;
using umkm_webapp.Security;


namespace umkm_webapp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/login")]
    public class LoginController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        private SecurityManager securityManager = new SecurityManager();
        public LoginController(DatabaseContext _db)
        {
            db = _db;
        }


        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        [Route("process")]
        public IActionResult Process(string username, string password)
        {
            var account = processLogin(username, password);
            if (account != null)
            {
                securityManager.SignIn(this.HttpContext, account);
                return RedirectToAction("index", "dashboard", new { area = "admin" });
            }
            else
            {
                ViewBag.error = "Invalid Account";
                return View("");

            }
        }


        private Account processLogin(string username, string password)
        {
            //var data = db.RoleAccounts
            //    .Include(x => x.Account)
            //    .Include(x => x.Role)
            //    .FirstOrDefault(x => x.Account.Employee.Email.Equals(login.Email));


            var account = db.Accounts.SingleOrDefault(a => a.Username.Equals(username) && a.Status == true);
            if (account != null)
            {
                var verivy = BCrypt.Net.BCrypt.Verify(password, account.Password);
                if (verivy)
                {
                    return account;
                }
                ViewBag.error = "Invalid Account";

            }
            return null;
        }

        [Route("signout")]
        public IActionResult SignOut()
        {
            securityManager.SignOut(this.HttpContext);
            return RedirectToAction("admin", "login", new { area = "admin" });

        }

        [Route("accesdenied")]
        public IActionResult AccesDenied()
        {

            return View("AccesDenied");
        }


    }
}
