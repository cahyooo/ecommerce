using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using umkm_webapp.Models;

namespace umkm_webapp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("admin")]
    [Route("admin/customer")]
    public class CustomerController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public CustomerController(DatabaseContext _db)
        {
            db = _db;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            ViewBag.customers = db.Accounts.Where(a => a.RoleAccounts.FirstOrDefault().RoleId == 2).ToList();
            return View();
        }

        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int Id)
        {
            var customers = db.Accounts.Find(Id);
            return View("Edit", customers);

        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int Id, Account account)
        {
            var customers = db.Accounts.Find(Id);

            if (!string.IsNullOrEmpty(account.Password))
            {
                customers.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            }
            customers.FullName = account.FullName;
            customers.Email = account.Email;
            customers.Address = account.Address;
            customers.Status = account.Status;
            customers.Phone = account.Phone;
            db.SaveChanges();

            return RedirectToAction("Index", "Customer", new { area = "admin" });

        }
    }

}