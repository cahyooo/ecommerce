using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            var exist = db.Accounts.Count(a => a.Username.Equals(account.Username)) > 0;
            if (!exist)
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
            else
            {
                ViewBag.error = "Username Exist";
                return View("register", account);
            }
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

        [Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("profile")]
        public IActionResult Profile()
        {
            var user = User.FindFirst(ClaimTypes.Name);
            var customer = db.Accounts.SingleOrDefault(a => a.Username.Equals(user.Value));
            ViewBag.Profile = customer;

            return View("Profile", customer);
        }

        [HttpPost]
        [Route("profile")]
        public IActionResult Profile(Account account)
        {
            var currentCustomer = db.Accounts.Find(account.Id);
            if (!string.IsNullOrEmpty(account.Password))
            {
                currentCustomer.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            }
            currentCustomer.FullName = account.FullName;
            currentCustomer.Email = account.Email;
            currentCustomer.Address = account.Address;
            currentCustomer.Phone = account.Phone;
            var user = User.FindFirst(ClaimTypes.Name);
            db.SaveChanges();
            var customer = db.Accounts.SingleOrDefault(a => a.Username.Equals(user.Value));
            ViewBag.Profile = customer;
            return View("profile", currentCustomer);
        }

        [HttpGet]
        [Route("Dashboard")]
        public IActionResult Dashboard()
        {

            return View("Dashboard");
        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("History")]
        public IActionResult History()
        {
            var user = User.FindFirst(ClaimTypes.Name);
            var customer = db.Accounts.SingleOrDefault(a => a.Username.Equals(user.Value));
            ViewBag.invoices = customer.Invoices.OrderByDescending(i => i.Id).ToList();
            return View("History");
        }

        [Authorize(Roles="Customer")]
        [HttpGet]
        [Route("details/{id}")]
        public IActionResult Details(int id)
        {
            //var user = User.FindFirst(ClaimTypes.Name);
            //var customer = db.Accounts.SingleOrDefault(a => a.Username.Equals(user.Value));
            //ViewBag.invoices = customer.Invoices.OrderByDescending(i => i.Id).ToList();
            ViewBag.invoiceDetails = db.InvoiceDetailses.Where(i => i.InvoiceId == id).ToList();
            return View("Details");
        }

    }
}