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
    [Route("admin/invoice")]
    public class InvoiceController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public InvoiceController(DatabaseContext _db)
        {
            db = _db;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            ViewBag.invoices = db.Invoices.OrderByDescending(i => i.Id).ToList();
            return View();
        }

        [HttpGet]
        [Route("details/{id}")]
        public IActionResult Details(int id)
        {
            //var user = User.FindFirst(ClaimTypes.Name);
            //var customer = db.Accounts.SingleOrDefault(a => a.Username.Equals(user.Value));
            //ViewBag.invoices = customer.Invoices.OrderByDescending(i => i.Id).ToList();
            ViewBag.invoice = db.Invoices.Find(id);
            return View("Details");
        }

        [HttpPost]
        [Route("process")]
        public IActionResult Process(int id)
        {
            var invoice = db.Invoices.Find(id);
            invoice.Status = 2;
            db.SaveChanges();
            return RedirectToAction("Index", "Invoice", new { area = "admin" } );
        }
    }
}
