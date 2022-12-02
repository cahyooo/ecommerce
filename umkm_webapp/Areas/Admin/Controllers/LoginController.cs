using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
                return View("index");

            }
        }

        
        private Account processLogin(string username, string password)
        {
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
            return RedirectToAction("index", "login", new { area = "admin" });

        }

        [HttpGet]
        [Route("profile")]
        public IActionResult Profile()
        {
            var user = User.FindFirst(ClaimTypes.Name);
            if (user != null)
            {
                var username = user.Value;
                var account = db.Accounts.SingleOrDefault(a => a.Username.Equals(username));

                return View("profile", account);
            }
            else
            {
                return RedirectToAction("index", "login", new { area = "admin" });
            }

            

        }

        [HttpPost]
        [Route("profile")]
        public IActionResult Profile(Account account)
        {
            var currentAccount = db.Accounts.SingleOrDefault(a => a.Username.Equals
            (account.Username));

            if (!string.IsNullOrEmpty(account.Password))
            {
                currentAccount.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            }
            currentAccount.Username = account.Username;
            currentAccount.Email = account.Email;
            currentAccount.FullName = account.FullName;
            db.SaveChanges();
            return View("profile");

        }

        [Route("accesdenied")]
        public IActionResult AccesDenied()
        {

            return View("AccesDenied");
        }


    }
}
