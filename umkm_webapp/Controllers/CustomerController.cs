using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using umkm_webapp.Models;
using umkm_webapp.Security;

namespace umkm_webapp.Controllers
{
    [Route("customer")]
    public class CustomerController : Controller
    {

        private SecurityManager securityManager = new SecurityManager();
        private DatabaseContext db = new DatabaseContext();

        public CustomerController(DatabaseContext _db)
        {
            db = _db;
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            var account = new Account();
            return View("register", account);
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(Account account)
        {
            account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            account.Status = true;
            db.Accounts.Add(account);
            db.SaveChanges();

            //Add Role 

            var roleAccount = new RoleAccount()
            {
                RoleId = 2,
                AccountId = account.Id,
                Status = true

            };
            db.RoleAccounts.Add(roleAccount);
            db.SaveChanges();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string username, string password)
        {
            var account = processLogin(username, password);
            if (account != null)
            {
                securityManager.SignIn(this.HttpContext, account);
                return RedirectToAction("index", "home");
            }
            else
            {
                ViewBag.error = "Invalid Account";
                return RedirectToAction("index", "home");


            }
        }


        private Account processLogin(string username, string password)
        {
            {
                var account = db.Accounts.SingleOrDefault(a => a.Username.Equals(username) && a.Status == true);

                if (account != null)
                {
                    var roleofAccount = account.RoleAccounts.FirstOrDefault();
                    if (roleofAccount.RoleId == 2 && roleofAccount.Status == true && BCrypt.Net.BCrypt.Verify(password, account.Password))
                    {
                        return account;
                    }
                }
                return null;
            }
        }

        [Route("signout")]
        public IActionResult SignOut()
        {
            securityManager.SignOut(this.HttpContext);
            return RedirectToAction("index", "home");

        }

    }
}