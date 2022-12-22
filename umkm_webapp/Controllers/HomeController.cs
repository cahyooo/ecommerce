using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using umkm_webapp.Models;
using X.PagedList;

namespace umkm_webapp.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}


        private DatabaseContext db = new DatabaseContext();

        public HomeController(DatabaseContext _db)
        {
            db = _db;
        }

        [Route("")]
        [Route("index")]
        [Route("~/")]

        public IActionResult Index()
        {
            ViewBag.isHome = true;
            var featuredproduct = db.Products.OrderByDescending(p => p.Id).Where(p => p.Status && p.Featured).ToList();
            ViewBag.FeaturedProducts = featuredproduct;
            ViewBag.CountFeaturedProducts = featuredproduct.Count;
            return View();
        }
      
        [Route("subproduct")]
        public IActionResult subproduct(int? page)
        {
            var pageNumber = page ?? 1;
           
            var product = db.Products.OrderByDescending(p => p.Id).Where(p => p.Status).ToList();
            ViewBag.Products = product;
            ViewBag.CountProducts = product.Count;
            ViewBag.LatestProducts = db.Products.OrderByDescending(p => p.Id).Where(p => p.Status).ToList();
            ViewBag.Products = product.Where(c => c.Status).ToList().ToPagedList(pageNumber, 1);
            return View();
        }

        



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
