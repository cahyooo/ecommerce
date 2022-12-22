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
    [Route("admin/dashboard")]

    public class DashboardController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public DashboardController(DatabaseContext _db)
        {
            db = _db;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            ViewBag.countInvoices = db.Invoices.Count(i => i.Status == 1);
            ViewBag.countProduct = db.Products.Count();
            ViewBag.countCustomer = db.RoleAccounts.Count(ra => ra.RoleId == 2);
            ViewBag.countCategory = db.Categories.Count(ca => ca.ParentId == null);
            return View();
        }
    }
}
