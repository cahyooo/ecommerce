using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace umkm_webapp.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        [Route("details/{id}")]
        public IActionResult Details(int id)
        {
            return View("Details");
        }
    }
}
