using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using umkm_webapp.Models;

namespace umkm_webapp.ViewComponents
{
    [ViewComponent(Name = "LatestProduct")]
    public class LatestProductViewComponent : ViewComponent
    {
        private DatabaseContext db;
        public LatestProductViewComponent(DatabaseContext _db)
        {
            this.db = _db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Product> products = db.Products.OrderByDescending(p => p.Id).Where(p => p.Status).Take(2).ToList();
            return View("Index", products);
        }
    }
}
